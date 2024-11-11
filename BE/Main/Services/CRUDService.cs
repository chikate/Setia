using Main.Data.DTOs;
using Main.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Main.Services;

public interface ICRUDService<TModel> where TModel : BaseModel
{
    Task<TModel> Add(TModel model);
    Task<bool> Delete(Guid id);
    Task<IEnumerable<TModel>> Get(GetFilterDTO<TModel> filter);
    Task<TModel> Update(TModel model);
}

public class CRUDService<TModel, TContext> : ICRUDService<TModel> where TModel : BaseModel where TContext : DbContext
{
    #region Dependency Injection 
    private readonly TContext _context;
    private readonly ILogger<CRUDService<TModel, TContext>> _logger;
    private readonly IAuditService _audit;
    private readonly IAuthService _auth;
    private readonly DbSet<TModel> _dbTable;

    public CRUDService
    (
        TContext context,
        ILogger<CRUDService<TModel, TContext>> logger,
        IAuditService audit,
        IAuthService auth
    )
    {
        _context = context;
        _dbTable = _context.Set<TModel>();
        _logger = logger;
        _audit = audit;
        _auth = auth;
    }
    #endregion

    public async Task<IEnumerable<TModel>> Get(GetFilterDTO<TModel> filter)
    {
        try
        {
            // Step 1: Start with the base query, excluding "Deleted" tagged. // Soft Delete
            IQueryable<TModel> query = _dbTable.Where(e => !e.Tags.Any(t => t.Contains("Deleted")));

            // Step 2: Include necessary navigation properties based on ForeignKey attributes.
            foreach (PropertyInfo? property in typeof(TModel).GetProperties().Where(p => p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any()))
                query = query.Include(property.Name.EndsWith("Id") ? property.Name.Substring(0, property.Name.Length - 2) + "Data" : property.Name + "Data");

            // Step 3: Apply filters if the filter object is provided.
            if (filter.Items.Count > 0)
                foreach (PropertyInfo property in typeof(TModel).GetProperties())
                {
                    object? filterValue = property.GetValue(filter);
                    if (filterValue != null && !string.IsNullOrEmpty(filterValue.ToString()))
                        query = query.Where(e => property.GetValue(e) != null && property.GetValue(e)!.Equals(filterValue));
                }

            // Step 4: Apply pagination if PageSize is valid.
            if (filter?.PageSize > 0 && filter?.PageNumber > 0) 
                query = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);

            return await query.ToListAsync();
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<TModel> Add(TModel model)
    {
        try
        {
            TModel addedEntity = (await _dbTable.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return addedEntity;
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<TModel> Update(TModel model)
    {
        try
        {
            TModel updatedEntity = _context.Update(model).Entity;
            await _context.SaveChangesAsync();
            return updatedEntity;
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
    public async Task<bool> Delete(Guid id)
    {
        try
        {
            TModel? entity = await _dbTable.FindAsync(id);

            if (entity == null) return false;
            else
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (Exception ex) { _logger.LogError(ex, GetType().FullName, ex.Message); throw; }
    }
}
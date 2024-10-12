using Main.Data.DTOs;
using Main.Data.Models;
using Main.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Main.Services;

public class CRUDService<TModel, TContext> : ICRUD<TModel> where TModel : BaseModel where TContext : DbContext
{
    #region Dependency Injection 
    private readonly TContext _context;
    private readonly DbSet<TModel> _dbTableData;
    private readonly ILogger<CRUDService<TModel, TContext>> _logger;
    private readonly IAudit _audit;
    private readonly IAuth _auth;

    public CRUDService
    (
        TContext context,
        ILogger<CRUDService<TModel, TContext>> logger,
        IAudit audit,
        IAuth auth
    )
    {
        _context = context;
        _dbTableData = context.Set<TModel>();
        _logger = logger;
        _audit = audit;
        _auth = auth;
    }
    #endregion

    public async Task<IEnumerable<TModel>> Get(GetFilterDTO<TModel>? filter)
    {
        try
        {
            // Step 1: Start with the base query, excluding "Deleted" tagged. // Soft Delete
            IQueryable<TModel> query = _dbTableData
                .Where(e => !e.Tags.Any(t => t.Contains("Deleted")))
                .AsNoTracking();

            // Step 2: Include necessary navigation properties based on ForeignKey attributes.
            foreach (PropertyInfo property in typeof(TModel).GetProperties().Where(p => p.GetCustomAttributes(typeof(ForeignKeyAttribute), false).Any()))
                query = query.Include(
                    property.Name.EndsWith("Id")
                        ? property.Name.Substring(0, property.Name.Length - 2) + "Data"
                        : property.Name + "Data");

            #region Tests
            // Step 3: Apply filters if the filter object is provided.
            //if (filter.Item.Count() > 0)
            //    foreach (PropertyInfo property in typeof(TModel).GetProperties())
            //    {
            //        object? filterValue = property.GetValue(filter);
            //        if (filterValue != null && !string.IsNullOrEmpty(filterValue.ToString()))
            //            query = query.Where(e => property.GetValue(e) != null && property.GetValue(e).Equals(filterValue));
            //    }
            #endregion

            return await query.ToListAsync();
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task<TModel> Add(TModel model)
    {
        try
        {
            model.GetType().GetProperty("ExecutionDate")?.SetValue(model, DateTime.Now);
            model.GetType().GetProperty("AuthorId")?.SetValue(model, _auth.GetCurrentUser()?.Id);

            #region Tests
            //PropertyInfo? passwordProperty = model.GetType().GetProperty("Password");
            //passwordProperty?.SetValue(model, _auth.CriptPassword((string)passwordProperty?.GetValue(model)));
            #endregion

            TModel addedEntity = (await _dbTableData.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            await _audit.LogAuditTrail(addedEntity);
            return addedEntity;
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task<TModel> Update(TModel model)
    {
        try
        {
            model.GetType().GetProperty("ExecutionDate")?.SetValue(model, DateTime.Now);
            model.GetType().GetProperty("AuthorId")?.SetValue(model, _auth.GetCurrentUser()?.Id);

            TModel updatedEntity = (_context.Update(model)).Entity;
            await _context.SaveChangesAsync();
            await _audit.LogAuditTrail(updatedEntity, model);
            return updatedEntity;
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
    public async Task Delete(string id)
    {
        try
        {
            TModel removedEntity = (_context.Remove(await _dbTableData.FindAsync(id))).Entity;
            await _context.SaveChangesAsync();
            await _audit.LogAuditTrail(removedEntity);
        }
        catch (Exception ex) { _logger.LogError(ex.Message, GetType().FullName); throw; }
    }
}
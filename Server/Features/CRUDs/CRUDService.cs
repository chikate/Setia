using Main.Data.Context;
using Main.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Main.Features.CRUDs;

public interface ICRUDService<TModel> where TModel : BaseModel
{
    Task<IEnumerable<TModel>> Get(GetFilterDTO<TModel> filter);
    Task<IEnumerable<TModel>> Add(List<TModel> models);
    Task<IEnumerable<TModel>> Update(List<TModel> models);
    Task<IEnumerable<TModel>> Delete(List<Guid> ids);
}

public class CRUDService<TModel>(BaseContext context) : ICRUDService<TModel> where TModel : BaseModel
{
    /// <summary>
    /// Get the list of entities from the database.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TModel>> Get(GetFilterDTO<TModel> filter)
    {
        // Step 1: Start with the base query, excluding "Deleted" tagged. // Soft Delete
        IQueryable<TModel> query = context.Set<TModel>().Where(e => !e.Tags.Any(t => t.Contains("Deleted")));

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

    /// <summary>
    /// Add the entity to the database.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TModel>> Add(List<TModel> models)
    {
        List<TModel> addedEntities = new();
        models.ForEach(async model =>
        {
            addedEntities.Add((await context.AddAsync(model)).Entity);
        });
        await context.SaveChangesAsync();
        return addedEntities;
    }

    /// <summary>
    /// Update the entity in the database.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TModel>> Update(List<TModel> models)
    {
        List<TModel> updatedEntities = new();
        models.ForEach(model =>
        {
            updatedEntities.Add(context.Update(model).Entity);
        });
        await context.SaveChangesAsync();
        return updatedEntities;
    }

    /// <summary>
    /// Delete the entity from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TModel>> Delete(List<Guid> ids)
    {
        List<TModel> deletedEntities = new();
        ids.ForEach(async id =>
        {
            TModel? deletedEntity = await context.Set<TModel>().FindAsync(id);
            if (deletedEntity != null)
            {
                context.Entry(deletedEntity).State = EntityState.Deleted;
                context.Remove(deletedEntity);
            }
        });
        await context.SaveChangesAsync();
        return deletedEntities;
    }
}

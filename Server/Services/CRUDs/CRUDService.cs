using Data.Context;
using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Features.CRUDs;

public interface ICRUDService<TModel> where TModel : BaseModel<Guid>
{
    Task<List<TModel>> Get(GetFilterDTO<TModel> filter);
    Task<List<TModel>> Add(List<TModel> models);
    Task<int> Update(List<TModel> models);
    Task<List<TModel>> Delete(List<Guid> ids);
}

public class CRUDService<TModel>(BaseContext context) : ICRUDService<TModel> where TModel : BaseModel<Guid>
{
    public async Task<List<TModel>> Get(GetFilterDTO<TModel> filter)
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

    public async Task<List<TModel>> Add(List<TModel> models)
    {
        await context.AddRangeAsync(models);
        await context.SaveChangesAsync();
        return models;
    }

    public async Task<int> Update(List<TModel> models)
    {
        models.ForEach(model => context.Entry(model).State = EntityState.Modified);
        return await context.SaveChangesAsync();
    }

    public async Task<List<TModel>> Delete(List<Guid> ids)
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

using Microsoft.AspNetCore.Mvc;
using Setia.Models;

namespace Setia.Services.Interfaces
{
    public interface IAudit
    {
        Task<IEnumerable<AuditModel>> GetAll();
        IEnumerable<AuditModel> GetAllWithFilter(AuditModel filter);
        Task<bool> Add(AuditModel model);
        Task<bool> Update(AuditModel model);
        Task<bool> Delete(int id);
        string CompareObjects<T>(T obj1, T obj2);
    }
}
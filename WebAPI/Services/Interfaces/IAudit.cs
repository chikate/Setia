using Microsoft.AspNetCore.Mvc;
using Setia.Models;

namespace Setia.Services.Interfaces
{
    public interface IAudit
    {
        Task<IEnumerable<AuditModel>> GetAll();
        Task<IEnumerable<AuditModel>> GetAllWithFilter(AuditModel filter);
        Task<string> Add(AuditModel model);
        Task<ActionResult<bool>> Update(AuditModel model);
        Task<ActionResult<bool>> Delete(int id);
    }
}
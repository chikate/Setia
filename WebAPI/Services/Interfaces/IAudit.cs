using Setia.Models;

namespace Setia.Services.Interfaces
{
    public interface IAudit
    {
        Task LogAuditTrail<T>(T model, T oldModel = default);
        Task<IEnumerable<AuditModel>> GetAll();
        IEnumerable<AuditModel> GetAllWithFilter(AuditModel filter);
        Task<bool> Add(AuditModel model);
        Task<bool> Update(AuditModel model);
        Task<bool> Delete(int id);
    }
}
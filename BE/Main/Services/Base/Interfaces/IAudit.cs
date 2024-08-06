namespace Main.Services.Base.Interfaces
{
    public interface IAudit
    {
        Task LogAuditTrail<T>(T model, T? oldModel = default);
    }
}
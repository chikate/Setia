namespace Main.Services.Interfaces;

public interface IAudit
{
    Task LogAuditTrail<T>(T model, T? oldModel = default);
}
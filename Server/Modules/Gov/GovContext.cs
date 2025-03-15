using Main.Modules.Audit;
using Main.Modules.Auth;
using Main.Modules.Gov.Models;
using Microsoft.EntityFrameworkCore;

namespace Main.Modules.Gov;

public partial class GovContext : DbContext
{
    #region Dependency Injection
    private readonly IConfiguration _config;
    private readonly IAuditService _audit;
    private readonly IAuthService _auth;

    public GovContext
    (
        DbContextOptions<GovContext> options,
        IConfiguration config,
        IAuditService audit,
        IAuthService auth
    ) : base(options)
    {
        _config = config;
        _audit = audit;
        _auth = auth;
    }
    #endregion

    #region SQL Tables
    public required DbSet<IntervalModel> Pontaj { get; set; }
    public required DbSet<QuestionModel> Questions { get; set; }
    public required DbSet<QuestionAnswerModel> QuestionAnswers { get; set; }
    public required DbSet<PostModel> Posts { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("gov");

        #region FKs
        modelBuilder.Entity<QuestionAnswerModel>(entity =>
        {
            entity.HasOne(e => e.QuestionData)
                  .WithMany()
                  .HasForeignKey(e => e.QuestionId)
                  .HasConstraintName("FK_QuestionAnswer_Question");
        });

        modelBuilder.Entity<PostModel>(entity =>
        {
            entity.HasOne(e => e.QuestionData)
                  .WithMany()
                  .HasForeignKey(e => e.QuestionId)
                  .HasConstraintName("FK_Post_Question");
        });
        #endregion

        //IEnumerable<PropertyInfo> dbSetProperties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        //foreach (PropertyInfo dbSetProperty in dbSetProperties)
        //{
        //    Type entityType = dbSetProperty.PropertyType.GetGenericArguments()[0];
        //    var entityBuilder = modelBuilder.Entity(entityType);

        //    ApplyCommonConfigurations(entityBuilder, entityType);
        //}
    }

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    foreach (EntityEntry entry in ChangeTracker.Entries()) //.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
    //    {
    //        entry.Property("ExecutionDate").CurrentValue = DateTime.Now;
    //        entry.Property("AuthorId").CurrentValue = (await _auth.GetCurrentUser())?.Id;
    //        await _audit.LogAuditTrail(entry.Entity);
    //    }
    //    return await base.SaveChangesAsync(cancellationToken);
    //}
}

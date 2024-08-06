using Main.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Main.Data.Contexts;

public partial class GovContext(DbContextOptions<GovContext> options) : DbContext(options)
{
    #region SQL Tables
    public DbSet<IntervalModel> Pontaj { get; set; }
    public DbSet<QuestionModel> Questions { get; set; }
    public DbSet<QuestionAnswerModel> QuestionAnswers { get; set; }
    public DbSet<PostModel> Posts { get; set; }
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

    //private void ApplyCommonConfigurations(dynamic entityBuilder, Type entityType)
    //{
    //    ApplyPropertyConfigurations(entityBuilder, entityType, "Author");
    //    ApplyPropertyConfigurations(entityBuilder, entityType, "User");
    //    ApplyPropertyConfigurations(entityBuilder, entityType, "Question");
    //}

    //private void ApplyPropertyConfigurations(dynamic entityBuilder, Type entityType, string properyName)
    //{
    //    PropertyInfo? dynProperty = entityType.GetProperty(properyName);
    //    PropertyInfo? dynDataProperty = entityType.GetProperty(properyName + "Data");
    //    if (dynProperty != null && dynDataProperty != null)
    //    {
    //        entityBuilder.HasOne(dynDataProperty.PropertyType, properyName + "Data")
    //                     .WithMany()
    //                     .HasForeignKey(properyName);
    //    }
    //}
}

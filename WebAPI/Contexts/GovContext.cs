using Microsoft.EntityFrameworkCore;
using Setia.Models.Gov;

namespace Setia.Contexts.Gov;

public partial class GovContext(DbContextOptions<GovContext> options) : DbContext(options)
{
    public DbSet<PontajModel> Pontaj { get; set; }
    public DbSet<QuestionModel> Questions { get; set; }
    public DbSet<QuestionAnswerModel> QuestionAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("gov");
    }
}

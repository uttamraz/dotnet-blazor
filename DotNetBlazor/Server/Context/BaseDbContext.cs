using Microsoft.EntityFrameworkCore;
using DotNetBlazor.Server.Entities;

namespace DotNetBlazor.Server.Context
{
    public partial class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var config = new ConfigurationBuilder()
        //            .AddEnvironmentVariables()
        //            .Build();

        //        optionsBuilder.UseSqlServer(config.GetConnectionString("Connection"),
        //            b => b.MigrationsAssembly(typeof(BaseDbContext).Assembly.FullName));

        //        base.OnConfiguring(optionsBuilder);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PokémonTeambuilder.core.Classes;
using System.Reflection;

namespace PokémonTeambuilder.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BasePokemon> BasePokemons { get; set; }
        public DbSet<Nature> Natures { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasePokemon>()
                .HasOne(b => b.BaseStats)
                .WithMany()
                .HasForeignKey(b => b.BaseStatsId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pokemon>()
                .HasOne(b => b.EVs)
                .WithMany()
                .HasForeignKey(b => b.EVsId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pokemon>()
                .HasOne(b => b.IVs)
                .WithMany()
                .HasForeignKey(b => b.IVsId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=legion5; Database=pokemon_teambuilder; Integrated Security=True; trustServerCertificate=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

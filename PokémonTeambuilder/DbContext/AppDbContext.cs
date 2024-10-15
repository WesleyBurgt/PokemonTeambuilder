using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PokémonTeambuilder.core.Dto;
using System.Reflection;

namespace PokémonTeambuilder.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BasePokemonDto> BasePokemons { get; set; }
        public DbSet<PokemonDto> Pokemons { get; set; }
        public DbSet<TeamDto> Teams { get; set; }
        public DbSet<AbilityDto> Abilities { get; set; }
        public DbSet<MoveDto> Moves { get; set; }
        public DbSet<StatsDto> Stats { get; set; }
        public DbSet<TypingDto> Typings { get; set; }
        public DbSet<ItemDto> Items { get; set; }
        public DbSet<NatureDto> Natures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PokemonDto>()
                .HasOne(p => p.EVs)
                .WithMany()
                .HasForeignKey(p => p.EVsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PokemonDto>()
                .HasOne(p => p.IVs)
                .WithMany()
                .HasForeignKey(p => p.IVsId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationship between BasePokemon and Typing
            modelBuilder.Entity<BasePokemonDto>()
                .HasMany(b => b.Typings)
                .WithMany(t => t.BasePokemons)
                .UsingEntity(j => j.ToTable("BasePokemonTypings"));

            // Many-to-many relationship between BasePokemon and Ability
            modelBuilder.Entity<BasePokemonDto>()
                .HasMany(b => b.Abilities)
                .WithMany(a => a.BasePokemons)
                .UsingEntity(j => j.ToTable("BasePokemonAbilities"));

            // Many-to-many relationship between BasePokemon and Move
            modelBuilder.Entity<BasePokemonDto>()
                .HasMany(b => b.Moves)
                .WithMany(m => m.BasePokemons)
                .UsingEntity(j => j.ToTable("BasePokemonMoves"));

            // Many-to-many relationship between Pokemon and Move (SelectedMoves)
            modelBuilder.Entity<PokemonDto>()
                .HasMany(p => p.SelectedMoves)
                .WithMany(m => m.Pokemons)
                .UsingEntity(j => j.ToTable("PokemonSelectedMoves"));

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

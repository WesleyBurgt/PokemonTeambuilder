using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PokémonTeambuilder.core.Dto;
using PokémonTeambuilder.dal;
using System.Reflection;

namespace PokémonTeambuilder.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext, IAppDbContext
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

            modelBuilder.Entity<BasePokemonDto>()
                .Property(b => b.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<AbilityDto>()
                .Property(b => b.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<ItemDto>()
                .Property(b => b.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<MoveDto>()
                .Property(b => b.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<NatureDto>()
                .Property(b => b.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<TypingDto>()
                .Property(b => b.Id)
                .ValueGeneratedNever();

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

            // Weaknesses relationship (many-to-many)
            modelBuilder.Entity<TypingDto>()
                .HasMany(t => t.Weaknesses)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "TypingWeakness",
                    j => j.HasOne<TypingDto>().WithMany().HasForeignKey("WeaknessId").OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<TypingDto>().WithMany().HasForeignKey("TypingId").OnDelete(DeleteBehavior.Restrict)
                );

            // Resistances relationship (many-to-many)
            modelBuilder.Entity<TypingDto>()
                .HasMany(t => t.Resistances)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "TypingResistance",
                    j => j.HasOne<TypingDto>().WithMany().HasForeignKey("ResistanceId").OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<TypingDto>().WithMany().HasForeignKey("TypingId").OnDelete(DeleteBehavior.Restrict)
                );

            // Immunities relationship (many-to-many)
            modelBuilder.Entity<TypingDto>()
                .HasMany(t => t.Immunities)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "TypingImmunity",
                    j => j.HasOne<TypingDto>().WithMany().HasForeignKey("ImmunityId").OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<TypingDto>().WithMany().HasForeignKey("TypingId").OnDelete(DeleteBehavior.Restrict)
                );

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

using Microsoft.EntityFrameworkCore;
using PokémonTeambuilder.core.Models;

namespace PokémonTeambuilder.dal.DbContext
{
    public class PokemonTeambuilderDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PokemonTeambuilderDbContext(DbContextOptions<PokemonTeambuilderDbContext> options) : base(options)
        {
        }

        public DbSet<BasePokemon> BasePokemons { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ability> Abilties { get; set; }
        public DbSet<BasePokemonTyping> BasePokemonTypings { get; set; }
        public DbSet<BasePokemonAbility> BasePokemonAbilities { get; set; }
        public DbSet<Typing> Typings { get; set; }
        public DbSet<TypingRelationship> TypingRelationships { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<SelectedMove> SelectedMoves { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Nature> Natures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Prevent Ef from making own Ids when gotten from api
            modelBuilder.Entity<BasePokemon>()
                .Property(bp => bp.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Ability>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Item>()
                .Property(i => i.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Move>()
                .Property(m => m.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Nature>()
                .Property(n => n.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Typing>()
                .Property(t => t.Id)
                .ValueGeneratedNever();

            //Pokemon EVs one-to-many
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.EVs)
                .WithMany()
                .HasForeignKey(p => p.EVsId)
                .OnDelete(DeleteBehavior.Restrict);

            //Pokemon IVs one-to-many
            modelBuilder.Entity<Pokemon>()
                .HasOne(p => p.IVs)
                .WithMany()
                .HasForeignKey(p => p.IVsId)
                .OnDelete(DeleteBehavior.Restrict);

            //TypingRelationShip many-to-many
            modelBuilder.Entity<TypingRelationship>()
                .HasKey(tr => new { tr.TypingId, tr.RelatedTypingId });

            modelBuilder.Entity<TypingRelationship>()
                .HasOne(tr => tr.Typing)
                .WithMany(t => t.Relationships)
                .HasForeignKey(tr => tr.TypingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TypingRelationship>()
                .HasOne(tr => tr.RelatedTyping)
                .WithMany()
                .HasForeignKey(tr => tr.RelatedTypingId)
                .OnDelete(DeleteBehavior.Restrict);

            //BasePokemonTyping many-to-many
            modelBuilder.Entity<BasePokemonTyping>()
                .HasKey(bpt => new { bpt.BasePokemonId, bpt.TypingId });

            modelBuilder.Entity<BasePokemonTyping>()
                .HasOne(bpt => bpt.BasePokemon)
                .WithMany(bp => bp.Typings)
                .HasForeignKey(bpt => bpt.BasePokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BasePokemonTyping>()
                .HasOne(bpt => bpt.Typing)
                .WithMany()
                .HasForeignKey(bpt => bpt.TypingId)
                .OnDelete(DeleteBehavior.Restrict);


            //BasePokemonAbility many-to-many
            modelBuilder.Entity<BasePokemonAbility>()
                .HasKey(bpa => new { bpa.BasePokemonId, bpa.AbilityId });

            modelBuilder.Entity<BasePokemonAbility>()
                .HasOne(bpa => bpa.BasePokemon)
                .WithMany(bp => bp.Abilities)
                .HasForeignKey(bpa => bpa.BasePokemonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BasePokemonAbility>()
                .HasOne(bpa => bpa.Ability)
                .WithMany()
                .HasForeignKey(bpa => bpa.AbilityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

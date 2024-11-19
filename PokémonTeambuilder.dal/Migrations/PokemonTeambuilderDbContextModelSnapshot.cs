﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokémonTeambuilder.dal.DbContext;

#nullable disable

namespace PokémonTeambuilder.dal.Migrations
{
    [DbContext(typeof(PokemonTeambuilderDbContext))]
    partial class PokemonTeambuilderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BasePokemonMove", b =>
                {
                    b.Property<int>("BasePokemonsId")
                        .HasColumnType("int");

                    b.Property<int>("MovesId")
                        .HasColumnType("int");

                    b.HasKey("BasePokemonsId", "MovesId");

                    b.HasIndex("MovesId");

                    b.ToTable("BasePokemonMove");
                });

            modelBuilder.Entity("MovePokemon", b =>
                {
                    b.Property<int>("PokemonsId")
                        .HasColumnType("int");

                    b.Property<int>("SelectedMovesId")
                        .HasColumnType("int");

                    b.HasKey("PokemonsId", "SelectedMovesId");

                    b.HasIndex("SelectedMovesId");

                    b.ToTable("MovePokemon");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Ability", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abilties");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.BasePokemon", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("BaseStatsId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sprite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BaseStatsId");

                    b.ToTable("BasePokemons");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.BasePokemonAbility", b =>
                {
                    b.Property<int>("BasePokemonId")
                        .HasColumnType("int");

                    b.Property<int>("AbilityId")
                        .HasColumnType("int");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.HasKey("BasePokemonId", "AbilityId");

                    b.HasIndex("AbilityId");

                    b.ToTable("BasePokemonAbilities");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.BasePokemonTyping", b =>
                {
                    b.Property<int>("BasePokemonId")
                        .HasColumnType("int");

                    b.Property<int>("TypingId")
                        .HasColumnType("int");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.HasKey("BasePokemonId", "TypingId");

                    b.HasIndex("TypingId");

                    b.ToTable("BasePokemonTypings");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("Accuracy")
                        .HasColumnType("int");

                    b.Property<int?>("BasePower")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PP")
                        .HasColumnType("int");

                    b.Property<int>("TypingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypingId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Nature", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("Down")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Up")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Natures");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbilityBasePokemonId")
                        .HasColumnType("int");

                    b.Property<int>("AbilityId")
                        .HasColumnType("int");

                    b.Property<int>("BasePokemonId")
                        .HasColumnType("int");

                    b.Property<int>("EVsId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("IVsId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("NatureId")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BasePokemonId");

                    b.HasIndex("EVsId");

                    b.HasIndex("IVsId");

                    b.HasIndex("ItemId");

                    b.HasIndex("NatureId");

                    b.HasIndex("TeamId");

                    b.HasIndex("AbilityBasePokemonId", "AbilityId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Stats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<int>("SpecialAttack")
                        .HasColumnType("int");

                    b.Property<int>("SpecialDefense")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Typing", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Typings");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.TypingRelationship", b =>
                {
                    b.Property<int>("TypingId")
                        .HasColumnType("int");

                    b.Property<int>("RelatedTypingId")
                        .HasColumnType("int");

                    b.Property<int>("Relation")
                        .HasColumnType("int");

                    b.HasKey("TypingId", "RelatedTypingId");

                    b.HasIndex("RelatedTypingId");

                    b.ToTable("TypingRelationships");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BasePokemonMove", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.BasePokemon", null)
                        .WithMany()
                        .HasForeignKey("BasePokemonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Move", null)
                        .WithMany()
                        .HasForeignKey("MovesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovePokemon", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.Pokemon", null)
                        .WithMany()
                        .HasForeignKey("PokemonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Move", null)
                        .WithMany()
                        .HasForeignKey("SelectedMovesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.BasePokemon", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.Stats", "BaseStats")
                        .WithMany()
                        .HasForeignKey("BaseStatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseStats");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.BasePokemonAbility", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.Ability", "Ability")
                        .WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.BasePokemon", "BasePokemon")
                        .WithMany("Abilities")
                        .HasForeignKey("BasePokemonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ability");

                    b.Navigation("BasePokemon");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.BasePokemonTyping", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.BasePokemon", "BasePokemon")
                        .WithMany("Typings")
                        .HasForeignKey("BasePokemonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Typing", "Typing")
                        .WithMany()
                        .HasForeignKey("TypingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BasePokemon");

                    b.Navigation("Typing");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Move", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.Typing", "Typing")
                        .WithMany()
                        .HasForeignKey("TypingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Typing");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Pokemon", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.BasePokemon", "BasePokemon")
                        .WithMany()
                        .HasForeignKey("BasePokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Stats", "EVs")
                        .WithMany()
                        .HasForeignKey("EVsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Stats", "IVs")
                        .WithMany()
                        .HasForeignKey("IVsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Item", "Item")
                        .WithMany("Pokemons")
                        .HasForeignKey("ItemId");

                    b.HasOne("PokémonTeambuilder.core.Models.Nature", "Nature")
                        .WithMany("Pokemons")
                        .HasForeignKey("NatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Team", null)
                        .WithMany("Pokemons")
                        .HasForeignKey("TeamId");

                    b.HasOne("PokémonTeambuilder.core.Models.BasePokemonAbility", "Ability")
                        .WithMany()
                        .HasForeignKey("AbilityBasePokemonId", "AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ability");

                    b.Navigation("BasePokemon");

                    b.Navigation("EVs");

                    b.Navigation("IVs");

                    b.Navigation("Item");

                    b.Navigation("Nature");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Team", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.User", null)
                        .WithMany("Teams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.TypingRelationship", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Models.Typing", "RelatedTyping")
                        .WithMany()
                        .HasForeignKey("RelatedTypingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Models.Typing", "Typing")
                        .WithMany("Relationships")
                        .HasForeignKey("TypingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RelatedTyping");

                    b.Navigation("Typing");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.BasePokemon", b =>
                {
                    b.Navigation("Abilities");

                    b.Navigation("Typings");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Item", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Nature", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Team", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.Typing", b =>
                {
                    b.Navigation("Relationships");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Models.User", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}

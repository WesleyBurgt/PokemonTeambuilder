﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokémonTeambuilder.DbContext;

#nullable disable

namespace PokémonTeambuilder.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241015111207_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AbilityDtoBasePokemonDto", b =>
                {
                    b.Property<int>("AbilitiesId")
                        .HasColumnType("int");

                    b.Property<int>("BasePokemonsId")
                        .HasColumnType("int");

                    b.HasKey("AbilitiesId", "BasePokemonsId");

                    b.HasIndex("BasePokemonsId");

                    b.ToTable("BasePokemonAbilities", (string)null);
                });

            modelBuilder.Entity("BasePokemonDtoMoveDto", b =>
                {
                    b.Property<int>("BasePokemonsId")
                        .HasColumnType("int");

                    b.Property<int>("MovesId")
                        .HasColumnType("int");

                    b.HasKey("BasePokemonsId", "MovesId");

                    b.HasIndex("MovesId");

                    b.ToTable("BasePokemonMoves", (string)null);
                });

            modelBuilder.Entity("BasePokemonDtoTypingDto", b =>
                {
                    b.Property<int>("BasePokemonsId")
                        .HasColumnType("int");

                    b.Property<int>("TypingsId")
                        .HasColumnType("int");

                    b.HasKey("BasePokemonsId", "TypingsId");

                    b.HasIndex("TypingsId");

                    b.ToTable("BasePokemonTypings", (string)null);
                });

            modelBuilder.Entity("MoveDtoPokemonDto", b =>
                {
                    b.Property<int>("PokemonsPokemonDtoId")
                        .HasColumnType("int");

                    b.Property<int>("SelectedMovesId")
                        .HasColumnType("int");

                    b.HasKey("PokemonsPokemonDtoId", "SelectedMovesId");

                    b.HasIndex("SelectedMovesId");

                    b.ToTable("PokemonSelectedMoves", (string)null);
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.AbilityDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.BasePokemonDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.ItemDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.MoveDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Accuracy")
                        .HasColumnType("int");

                    b.Property<int>("BasePower")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypingId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.NatureDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.PokemonDto", b =>
                {
                    b.Property<int>("PokemonDtoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PokemonDtoId"));

                    b.Property<int>("AbilityId")
                        .HasColumnType("int");

                    b.Property<int>("BasePokemonId")
                        .HasColumnType("int");

                    b.Property<int>("EVsId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IVsId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("NatureId")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamDtoId")
                        .HasColumnType("int");

                    b.HasKey("PokemonDtoId");

                    b.HasIndex("AbilityId");

                    b.HasIndex("BasePokemonId");

                    b.HasIndex("EVsId");

                    b.HasIndex("IVsId");

                    b.HasIndex("ItemId");

                    b.HasIndex("NatureId");

                    b.HasIndex("TeamDtoId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.StatsDto", b =>
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

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.TeamDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.TypingDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Typings");
                });

            modelBuilder.Entity("AbilityDtoBasePokemonDto", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Dto.AbilityDto", null)
                        .WithMany()
                        .HasForeignKey("AbilitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.BasePokemonDto", null)
                        .WithMany()
                        .HasForeignKey("BasePokemonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BasePokemonDtoMoveDto", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Dto.BasePokemonDto", null)
                        .WithMany()
                        .HasForeignKey("BasePokemonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.MoveDto", null)
                        .WithMany()
                        .HasForeignKey("MovesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BasePokemonDtoTypingDto", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Dto.BasePokemonDto", null)
                        .WithMany()
                        .HasForeignKey("BasePokemonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.TypingDto", null)
                        .WithMany()
                        .HasForeignKey("TypingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoveDtoPokemonDto", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Dto.PokemonDto", null)
                        .WithMany()
                        .HasForeignKey("PokemonsPokemonDtoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.MoveDto", null)
                        .WithMany()
                        .HasForeignKey("SelectedMovesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.BasePokemonDto", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Dto.StatsDto", "BaseStats")
                        .WithMany()
                        .HasForeignKey("BaseStatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseStats");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.MoveDto", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Dto.TypingDto", "Typing")
                        .WithMany()
                        .HasForeignKey("TypingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Typing");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.PokemonDto", b =>
                {
                    b.HasOne("PokémonTeambuilder.core.Dto.AbilityDto", "Ability")
                        .WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.BasePokemonDto", "BasePokemon")
                        .WithMany()
                        .HasForeignKey("BasePokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.StatsDto", "EVs")
                        .WithMany()
                        .HasForeignKey("EVsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.StatsDto", "IVs")
                        .WithMany()
                        .HasForeignKey("IVsId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.ItemDto", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.NatureDto", "Nature")
                        .WithMany()
                        .HasForeignKey("NatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokémonTeambuilder.core.Dto.TeamDto", null)
                        .WithMany("Pokemons")
                        .HasForeignKey("TeamDtoId");

                    b.Navigation("Ability");

                    b.Navigation("BasePokemon");

                    b.Navigation("EVs");

                    b.Navigation("IVs");

                    b.Navigation("Item");

                    b.Navigation("Nature");
                });

            modelBuilder.Entity("PokémonTeambuilder.core.Dto.TeamDto", b =>
                {
                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}

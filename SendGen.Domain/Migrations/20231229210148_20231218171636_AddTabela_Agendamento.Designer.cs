﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SendGen.Domain.SendGenDomains.Data;

#nullable disable

namespace SendGen.Domain.Migrations
{
    [DbContext(typeof(SendGenContexto))]
    [Migration("20231229210148_20231218171636_AddTabela_Agendamento")]
    partial class _20231218171636_AddTabela_Agendamento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SendGen.Domain.SendGenDomains.Data.Agendamento", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CanalID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condicao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiltroID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IntervaloExecucao")
                        .HasColumnType("int");

                    b.Property<string>("TemplateID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UltimaExecucao")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Agendamento");
                });

            modelBuilder.Entity("SendGen.Domain.SendGenDomains.Data.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TraCod")
                        .HasColumnType("int");

                    b.HasKey("ClienteId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("SendGen.Domain.SendGenDomains.Data.FiltroDB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Condicao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("FiltroDB");
                });
#pragma warning restore 612, 618
        }
    }
}

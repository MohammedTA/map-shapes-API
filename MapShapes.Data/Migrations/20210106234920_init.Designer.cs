﻿// <auto-generated />
using MapShapes.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MapShapes.Data.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    [Migration("20210106234920_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("MapShapes.Data.Entities.OverlayShape", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Properties")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShapeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShapeTypeId");

                    b.ToTable("OverlayShapes");
                });

            modelBuilder.Entity("MapShapes.Data.Entities.ShapeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShapeTypes");
                });

            modelBuilder.Entity("MapShapes.Data.Entities.OverlayShape", b =>
                {
                    b.HasOne("MapShapes.Data.Entities.ShapeType", "ShapeType")
                        .WithMany("OverlayShapes")
                        .HasForeignKey("ShapeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShapeType");
                });

            modelBuilder.Entity("MapShapes.Data.Entities.ShapeType", b =>
                {
                    b.Navigation("OverlayShapes");
                });
#pragma warning restore 612, 618
        }
    }
}

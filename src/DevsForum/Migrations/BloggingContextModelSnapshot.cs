using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DevsForum.Entities;

namespace DevsForum.Migrations
{
    [DbContext(typeof(BloggingContext))]
    partial class BloggingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DevsForum.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DevsForum.Entities.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("DevsForum.Entities.Post", b =>
                {
                    b.HasOne("DevsForum.Entities.Topic", "Topic")
                        .WithMany("Posts")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

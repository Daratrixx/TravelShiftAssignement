using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MagazineConnector.Contexts
{
    public class BlogContext : DbContext
    {
        public static readonly string DatabasePath = @".\Blog.sqlite";
        public BlogContext()
        {
        }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            if (!File.Exists(DatabasePath))
            {
                SQLiteConnection.CreateFile(DatabasePath);
            }
            optionbuilder.UseSqlite(@$"Data Source={DatabasePath};");
        }

        public DbSet<Model.Article> Articles { get; set; }
        public DbSet<Model.Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Article>(entity =>
            {
                entity.HasIndex(e => e.id)
                    .IsUnique();

                entity.Property(e => e.title)
                    .IsRequired();

                entity.Property(e => e.content)
                    .IsRequired();

                entity.Property(e => e.category)
                    .IsRequired();

                entity.Property(e => e.dateCreated)
                    .IsRequired();
            });

            modelBuilder.Entity<Model.Author>(entity =>
            {
                entity.HasIndex(e => e.id)
                    .IsUnique();

                entity.Property(e => e.name)
                    .IsRequired();
            });
        }
    }
}

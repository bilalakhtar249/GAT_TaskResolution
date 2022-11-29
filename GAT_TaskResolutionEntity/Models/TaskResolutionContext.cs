using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GAT_TaskResolutionEntity.Models
{
    public class TaskResolutionContext : DbContext
    {
        public TaskResolutionContext() : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasMany(s => s.Subjects);
            modelBuilder.Entity<Student>().HasMany(s => s.Subjects);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.ID)
                .HasIndex(s => s.Number).IsUnique();

            

            modelBuilder.Entity<Subject>()
                .HasKey(s => s.ID)
                .HasIndex(s => s.Code).IsUnique();

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
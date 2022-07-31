using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Овърайд на OnConfiguring за създаване на Връзка с Базите
        {
            //!!!! Важна проверка
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer
                    (@"Server=.;Database=StudentSystem;Integrated Security=true;TrustServerCertificate=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Оварайд на OnModelCreating за Fluent API настройки на базата
        {
            modelBuilder.Entity<StudentCourse>(x =>
                {
                    x.HasKey(x => new { x.CourseId, x.StudentId });
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}

using EL_Saher.Shared;
using EL_Saher.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EL_Saher.Server.DataBase
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Course>().Property(i => i.StudentId).IsRequired(false);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<CourseAttendance>()
                .HasOne(att => att.Student)
                .WithMany(s => s.Attendances)
                .HasForeignKey(att => att.StudentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();


            modelBuilder.Entity<MonthFee>()
                .HasOne(att => att.Student)
                .WithMany(s => s.MonthFees)
                .HasForeignKey(att => att.StudentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Student)
                .WithMany(s=>s.Exams)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
		}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseAttendance> CourseAttendances { get; set; }
        public DbSet<User> Users { get; set; }

		public DbSet<MonthFee> MonthFees { get; set; }


    }
}

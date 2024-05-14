using Microsoft.EntityFrameworkCore;
using universityApiBackend.Models.DataModels;


namespace universityApiBackend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options) {

        }

        // Add DbSets (tables of ours Data Base)
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Student>? Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Categories)
                .WithMany(c => c.Courses)
                .UsingEntity(j => j.ToTable("CourseCategory"));
            modelBuilder.Entity<Course>()
                .HasMany(c => c.students)
                .WithMany(c => c.Courses)
                .UsingEntity(j => j.ToTable("CourseStudent"));
        }


    }
}

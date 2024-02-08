using Microsoft.EntityFrameworkCore;

namespace daw_en_2324.Models
{
    public class PSAContext : DbContext
    {
        public PSAContext()
        {
        }

        public PSAContext(DbContextOptions<PSAContext> options) : base(options)
        {
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CurricularProgram> CurricularPrograms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Uc> Ucs { get; set; }
    }
}

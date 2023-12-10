using HospitalManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementAPI.DBContext
{
    public class HospitalDBContext : DbContext
    {
        public HospitalDBContext() { }
        public HospitalDBContext(DbContextOptions<HospitalDBContext> dbContextOptions) : base(dbContextOptions) { }
      
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<ProgressNote> ProgressNotes { get; set; }

    }
}

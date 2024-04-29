using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using test_app.Models;

namespace test_app;

public class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }
    
        public DbSet<Diagnose> Diagnose { get; set; } = null!;
        public DbSet<Doctor> Doctor { get; set; } = null!;
        public DbSet<Patient> Patient { get; set; } = null!;
        public DbSet<Procedure> Procedure { get; set; } = null!;
        public DbSet<DocDiagnose> DocDiagnose { get; set; } = null!;
        public DbSet<DocProcedure> DocProcedure { get; set; } = null!;
        public DbSet<PatDiagnose> PatDiagnose { get; set; } = null!;
        public DbSet<PatProcedure> PatProcedure { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=172.17.0.3;Database=HospitalForms;Username=postgres;Password=123");
    }
}
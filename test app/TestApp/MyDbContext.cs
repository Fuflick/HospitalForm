using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using test_app.Models;

namespace test_app;

public class MyDbContext : DbContext
{

    // Какие таблицы есть в базе данных
    public DbSet<Diagnose> Diagnose { get; set; } = null!;
    public DbSet<Doctor> Doctor { get; set; } = null!;
    public DbSet<Patient> Patient { get; set; } = null!;
    public DbSet<Procedure> Procedure { get; set; } = null!;
    public DbSet<DocDiagnose> DocDiagnose { get; set; } = null!;
    public DbSet<DocProcedure> DocProcedure { get; set; } = null!;
    public DbSet<PatDiagnose> PatDiagnose { get; set; } = null!;
    public DbSet<PatProcedure> PatProcedure { get; set; } = null!;

    // Метод, реализующий подключение к базе данных
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Строка подключения
        optionsBuilder.UseNpgsql("Host=172.17.0.1;Database=HospitalForms;Username=postgres;Password=123");
    }
}
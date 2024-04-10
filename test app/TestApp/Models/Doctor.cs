using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class Doctor
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public Specialization[]? Specializations { get; set; }
    
}
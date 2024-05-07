using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class Doctor
{
    [Key] //атрибут указывающий на то, что Id primary key
    public int Id { get; set; }

    // Имя
    public string? Name { get; set; }

    // Список Специализаций врача 
    public Specialization[]? Specializations { get; set; }
    
}
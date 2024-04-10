using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class Patient
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }

    public Gender? Gender { get; set; }
}
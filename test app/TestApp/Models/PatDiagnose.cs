using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class PatDiagnose
{
    [Key] public int Id { get; set; }
    
    public int? PatId { get; set; }

    public int? DiagId { get; set; }
}
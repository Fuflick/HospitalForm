using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class DocDiagnose
{
    [Key] public int Id { get; set; }

    public int? DocId { get; set; }

    public int? DiagId { get; set; }
}
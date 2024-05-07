using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class DocDiagnose
{
    // primary key Id
    [Key] public int Id { get; set; }

    // связь с врачом
    public int? DocId { get; set; }
    
    // связь с диагнозом
    public int? DiagId { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class DocProcedure
{
    [Key] public int Id { get; set; }

    public int? DocId { get; set; }

    public int? ProcId { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace test_app.Models;

public class PatProcedure
{
    [Key] public int Id { get; set; }

    public int? PatId { get; set; }

    public int? Procid { get; set; }
}
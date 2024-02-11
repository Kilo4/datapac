using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datapac_interview.Models;

public class Book : IBaseEntity
{
    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Title { get; set; }
    public string? Author { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }

}
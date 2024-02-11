using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datapac_interview.Models;

public class Order: IBaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OrderDate { get; set; }

    public DateTime LastDate { get; set; }
    public bool Completed { get; set; }
    public string UserMailAdress { get; set; }

    [Column(TypeName = "jsonb")]
    public List<Book> Books { get; set; }
}
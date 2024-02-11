using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace datapac_interview.Dto.Book.request;

public class UpdateOrderedBookRequest 
{
    [Required]
    [DefaultValue("b226d810-bf4d-4892-a957-7b8356ece6a9")]
    public Guid Id { get; set; }
    
    [Required]
    [DefaultValue(false)]
    public bool Available { get; set; }
}
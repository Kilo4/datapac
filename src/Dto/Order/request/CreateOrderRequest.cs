using System.ComponentModel.DataAnnotations;
using datapac_interview.Dto.Book.request;

namespace datapac_interview.Dto.Order.request;

public class CreateOrderRequest
{
    [Required]
    [EmailAddress]
    public string UserMailAdress { get; set; }
    
    public List<UpdateOrderedBookRequest>? Books { get; set; }
}
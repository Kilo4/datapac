using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace datapac_interview.Dto.Book.request;

public class UpdateBookRequest
{
    [StringLength(40, MinimumLength = 5, ErrorMessage = "Length of name of the Title 5 a 40 char.")]
    [DefaultValue("Harry Potter and the Philosopher's Stone")]
    public string? Title { get; set; }
    [StringLength(40, MinimumLength = 5, ErrorMessage = "Length of name of the author 5 a 40 char.")]
    [DefaultValue("J. K. Rowling")]
    public string? Author { get; set; }
    
    [Range(0.01, 100.00, ErrorMessage = "Value must be in the range of 0.01 a 100.00.")]
    [DefaultValue("54.99")]
    public decimal? Price { get; set; }
    
    [DefaultValue(false)]
    public bool? Available { get; set; }
}
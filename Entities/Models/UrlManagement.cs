using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("url_management")]   

    public class UrlManagement
    {
        // 7 characters in base62 will generate roughly ~3500 Billion URLs. 62 to the power of 7 base 62 are [0–9][a-z][A-Z]
        [Key]
        [Required]
        [MaxLength(7, ErrorMessage = "Maximum length for the short url is 7 characters.")]
        [MinLength(7, ErrorMessage = "Minimum length for the short url is 7 characters.")]
        public string? ShortUrl { get; set; }

        [Required(ErrorMessage = "Long URL is a required field.")]
        public string? OriginalUrl { get; set; }

        
        
    }
}

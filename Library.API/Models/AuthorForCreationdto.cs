using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name.")]
        [MaxLength(50)]
        public string Name { get; set; }
        
    }
}

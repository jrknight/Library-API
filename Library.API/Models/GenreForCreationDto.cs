using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class GenreForCreationDto
    {
        [Required(ErrorMessage = "A name value is required.")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}

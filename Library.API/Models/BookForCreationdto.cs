using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "You should provide a title value.")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "You should provide an author value.")]
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Summary { get; set; }
        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; }
        [Required]
        public IEnumerable<int> genreIds { get; set; }
    }
}

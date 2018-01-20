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
        [MaxLength(1000)]
        public string Summary { get; set; }
        [MaxLength(20)]
        public string ISBN { get; set; }
        public string Genre { get; set; }
    }
}

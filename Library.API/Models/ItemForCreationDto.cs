using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class ItemForCreationDto
    {
        [Required(ErrorMessage = "You should provide a title value.")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "You should provide an author value.")]
        public string OwnerEmail { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhotoUrl { get; set; }
    }
}

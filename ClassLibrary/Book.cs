using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    [Table("tblBook")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string ISBN { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
        public Author Author { get; set; }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    [Table("tblBookGenre")]
    public class BookGenre
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public Genre Genre { get; set; }
    }
}

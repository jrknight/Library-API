using Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities
{
    [Table("tblBookRequest")]
    public class BookRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public Student Student { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public bool Fufilled { get; set; }

    }
}

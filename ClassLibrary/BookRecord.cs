using Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities
{
    [Table("tblBookRecord")]
    public class BookRecord
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public Student Student { get; set; }
        
    }
}

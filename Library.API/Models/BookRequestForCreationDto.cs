using Entities;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class BookRequestForCreationDto
    {
        [Required]
        public Book Book { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public LibraryUser User { get; set; }
        [Required]
        public bool Fufilled { get; set; }
    }
}

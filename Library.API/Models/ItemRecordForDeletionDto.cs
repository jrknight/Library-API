using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class ItemRecordForDeletionDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

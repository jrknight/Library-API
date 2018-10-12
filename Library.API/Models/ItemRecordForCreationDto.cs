using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class ItemRecordForCreationDto
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}

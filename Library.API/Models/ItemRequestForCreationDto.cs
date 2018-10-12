using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class ItemRequestForCreationDto
    {
        public int BookId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}

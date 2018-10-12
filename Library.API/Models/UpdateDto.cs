using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class UpdateDto
    {
        public bool IsUpdate { get; set; }
        public string Url { get; set; }
        public string UpdateMessage { get; set; }
    }
}

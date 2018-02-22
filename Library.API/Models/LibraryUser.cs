using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities
{
    public class LibraryUser : IdentityUser
    {
        public string RoleClaim { get; set; }
        public string Name { get; set; }
    }
}

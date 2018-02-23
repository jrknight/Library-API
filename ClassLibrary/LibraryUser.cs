using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class LibraryUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

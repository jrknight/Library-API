using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class CredentialModel : IdentityUser
    {
        public string Password { get; set; }
        public string RoleClaim { get; set; }
    }
}

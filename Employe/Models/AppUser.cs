﻿using Microsoft.AspNetCore.Identity;

namespace Employe.Models
{
    public class AppUser:IdentityUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}

﻿using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
   
        public class ApplicationUser : IdentityUser
        {
            public string City { get; set; }
        }
    
}

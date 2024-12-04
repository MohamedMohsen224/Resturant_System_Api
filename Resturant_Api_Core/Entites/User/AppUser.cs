﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Entites.User
{
    public class AppUser : IdentityUser
    {
        public Address Address { get; set; }
    }
}

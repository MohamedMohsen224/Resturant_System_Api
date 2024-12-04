using Microsoft.AspNetCore.Identity;
using Resturant_Api_Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Core.Services.AuthServices
{
    public interface IAuthServices
    {
        Task<String> CreateToken(AppUser user, UserManager<AppUser> userManager);

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant_Api.Dtos.AppUserDTO;
using Resturant_Api.HandleErrors;
using Resturant_Api_Core.Entites.User;
using Resturant_Api_Core.Services.AuthServices;

namespace Resturant_Api.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthServices authServices;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public UserController(IAuthServices authServices,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.authServices = authServices;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiErrorResponse(401,"This Email is not Found"));
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(result.Succeeded is false)
                return Unauthorized(new ApiErrorResponse(401,"Email Or Password is InCorrect"));
            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await authServices.CreateToken(user,userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false || model.Password != model.ConfirmPassword)
                return BadRequest(new ApiErrorResponse(400));

            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await authServices.CreateToken(user, userManager)
            });
        }

    }
}

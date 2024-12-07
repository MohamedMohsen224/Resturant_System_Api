using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender emailSender;

        public UserController(IAuthServices authServices,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager , IEmailSender emailSender)
        {
            this.authServices = authServices;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.emailSender = emailSender;
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

        [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                return RedirectToAction("ForgotPassword", new { message = "Password reset instructions have been sent (if the email address exists)." });
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action(
                "ResetPassword",
                "Account",
                new { userId = user.Id, code = token },
                Request.Scheme);

            var emailMessage = $"Please click the link below to reset your password for {user.Email}:\n{callbackUrl}";

            await emailSender.SendEmailAsync(user.Email, "Reset Your Password", emailMessage);

            return RedirectToAction("ForgotPassword", new { message = "Password reset instructions have been sent to your email." });
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            var result = await userManager.ChangePasswordAsync(user, resetPasswordDto.NewPassword, resetPasswordDto.ConfirmPassword);
            if (result.Succeeded is true && resetPasswordDto.NewPassword == resetPasswordDto.ConfirmPassword)
                return Ok(new UserDto()
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = await authServices.CreateToken(user, userManager)

                });
            else
                return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(UserDto userDto)
        {
            var user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                return BadRequest(new ApiErrorResponse(400));
            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await authServices.CreateToken(user, userManager)
            });
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UserDto>> UpdateUser(UserDto userDto)
        {
            var user = await userManager.FindByEmailAsync(userDto.Email);
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded is false)
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

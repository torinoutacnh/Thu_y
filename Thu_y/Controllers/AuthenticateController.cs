using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Modules.UserModule.Ports;

namespace Thu_y.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public ActionResult<ResponseLoginModel> Authenticate(UserDtoModel model)
        {
            var response = _userService.Authenticate(model);
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("validate-reset-token")]
        public IActionResult ValidateResetToken(ValidateResetTokenRequest model)
        {
            _userService.ValidateResetToken(model);
            return Ok(new { message = "Token is valid" });
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        [HttpPost("refresh-token")]
        public ActionResult<ResponseLoginModel> RefreshToken(RefreshTokenModel model)
        {
            var refreshToken = model.Token ?? Request.Cookies["refreshToken"];
            var response = _userService.RefreshToken(refreshToken);
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("register")]

        public IActionResult Register(RegisterModel model)
        {
            _userService.Register(model);
            return Ok(new { message = "Registration successful, please check your email for verification instructions" });
        }

        [HttpPost("verify-email")]
        public IActionResult VerifyEmail(VerifyEmailRequestModel model)
        {
            _userService.VerifyEmail(model);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequestModel model)
        {
            _userService.ForgotPassword(model);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordRequest model)
        {
            _userService.ResetPassword(model);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [HttpPost("resend-email")]
        public IActionResult ResendEmail(ResendEmailModel model)
        {
            _userService.ReSendEmail(model);
            return Ok(new { message = "Resend Email successful" });
        }
    }
}

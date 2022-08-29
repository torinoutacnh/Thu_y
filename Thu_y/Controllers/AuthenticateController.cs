using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AuthenticateController : BaseController
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
            return Ok(new { message = "Đăng ký tài khoản thành công, vui lòng vào email để kích hoạt tài khoản" });
        }

        [HttpPost("verify-email")]
        public IActionResult VerifyEmail(VerifyEmailRequestModel model)
        {
            _userService.VerifyEmail(model);
            return Ok(new { message = "Kích hoạt tài khoản thành công" });
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordRequestModel model)
        {
            _userService.ForgotPassword(model);
            return Ok(new { message = "Yêu cầu cấp lại mật khẩu thành công, vui lòng kiểm tra email" });
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("change-password")]
        public IActionResult ChangePassword(ChangePasswordRequest model)
        {
            _userService.ChangePassword(model, UserEntity.Role);
            return Ok(new { message = "Thay đổi mật khẩu thành công" });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordRequest model)
        {
            _userService.ResetPassword(model);
            return Ok(new { message = "Đặt lại mật khẩu thành công" });
        }

        [HttpPost("resend-email")]
        public IActionResult ResendEmail(ResendEmailModel model)
        {
            _userService.ReSendEmail(model);
            return Ok(new { message = "Resend Email successful" });
        }

    }
}

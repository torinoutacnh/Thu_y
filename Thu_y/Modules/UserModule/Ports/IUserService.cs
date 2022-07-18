using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Model;

namespace Thu_y.Modules.UserModule.Ports
{
    public interface IUserService
    {
        bool CreateUser(UserModel model);
        bool UpdateUser(UserModel model);
        bool DeleteUser(string id);
        UserEntity GetUserByAccount(string username);
        List<UserEntity> GetAccount(int PageIndex, int PageNumber);
        string CreateJWTToken(UserEntity loggedUser);
        ResponseLoginModel Authenticate(UserDtoModel model);
        UserGetListModel GetUserById(string id);
        ResponseLoginModel RefreshToken(string token);
        void Register(RegisterModel model);
        void VerifyEmail(VerifyEmailRequestModel model);
        void ForgotPassword(ForgotPasswordRequestModel model);
        void ResetPassword(ResetPasswordRequest model);
        void ReSendEmail(ResendEmailModel model);
        void ValidateResetToken(ValidateResetTokenRequest model);
    }
}

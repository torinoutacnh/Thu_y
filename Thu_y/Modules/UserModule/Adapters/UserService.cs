using AutoMapper;
using Thu_y.Infrastructure.UOF;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Thu_y.Utils.Infrastructure.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Thu_y.Infrastructure.Utils.Constant;
using System.Security.Cryptography;
using Thu_y.Utils.Infrastructure.Application;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserScheduleRepository _userScheduleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IServiceProvider serviceProvider)
        {
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _userScheduleRepository = serviceProvider.GetRequiredService<IUserScheduleRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        #region Authenticate
        public ResponseLoginModel Authenticate(UserDtoModel model)
        {
            var account = _userRepository.GetSingle(_=>_.Account == model.UserName);
            var token = CreateJWTToken(account);
            var refreshToken = GenerateRefreshToken();
            account.RefreshTokens.Add(refreshToken);

            // xóa mấy refreshtoken củ đi
            RemoveOldRefreshTokens(account);
            try
            {
                _userRepository.Update(account);
                _unitOfWork.SaveChange();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var response = _mapper.Map<ResponseLoginModel>(account);
            response.RefreshToken = refreshToken.Token;
            response.Token = token;

            return response;
        }
        #endregion Authenticate

        #region Create User
        public bool CreateUser(UserModel model)
        {
            var user = _mapper.Map(model, new UserEntity());
            user.Role = RoleType.Employee;
            _userRepository.Add(user);
            _unitOfWork.SaveChange();

            return true;
        }
        #endregion Create User

        #region Update User
        public bool UpdateUser(UserModel model)
        {
            var user = _userRepository.Get(x => x.Id.Equals(model.Id)).FirstOrDefault();
            if (user == null) throw new Exception("No user found!") { HResult = 400 };

            var updated = _mapper.Map(model, user);
            _userRepository.Update(updated);
            _unitOfWork.SaveChange();

            return true;
        }
        #endregion Update User

        #region Delete User
        public bool DeleteUser(string id)
        {
            var user = _userRepository.Get(x => x.Id.Equals(id)).FirstOrDefault();
            if (user == null) throw new Exception("No user found!") { HResult = 400 };

            _userRepository.Delete(user);
            _unitOfWork.SaveChange();

            return true;
        }
        #endregion Delete User

        #region Get BY Account
        public UserEntity GetByAccount(string username)
        {
            return _userRepository.Get(_ => _.Account == username && _.DateDeleted == null).FirstOrDefault();
        }
        #endregion Get BY Account

        public List<UserEntity> GetAccount(int PageIndex, int PageNumber)
        {
            return _userRepository.Get(_ => _.Status == 0).Skip((PageIndex - 1) * PageNumber).Take(PageNumber).ToList();
        }

        #region Create Token
        public string CreateJWTToken(UserEntity loggedUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(JWTSettingModel.Instance.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("account", loggedUser.Account),
                    new Claim(ClaimTypes.Name, loggedUser.Name),
                    new Claim(ClaimTypes.Email, loggedUser.Email),
                    new Claim(ClaimTypes.Role, loggedUser.Role.ToString())
                }),
                IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion Create Token

        #region GenerateRefreshToken
        public static RefreshToken GenerateRefreshToken()
        {
            var randomByte = new byte[64];
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(randomByte);
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomByte),
                Expires = SystemHelper.SystemTimeNow.AddDays(1),
            };
            return refreshToken;
        }
        #endregion Generate Refresh Token

        private static void RemoveOldRefreshTokens(UserEntity userEntity)
        {
            foreach (var item in userEntity.RefreshTokens)
            {
                if (!item.IsActive &&
                    item.DateCreated.AddDays(2) <= SystemHelper.SystemTimeNow)
                {
                    userEntity.RefreshTokens.Remove(item);
                }
            }
        }

    }
}

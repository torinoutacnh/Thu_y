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
using Thu_y.Infrastructure.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Thu_y.Modules.UserModule.Adapters
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserScheduleRepository _userScheduleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserService(IServiceProvider serviceProvider)
        {
            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _userScheduleRepository = serviceProvider.GetRequiredService<IUserScheduleRepository>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _refreshTokenRepository = serviceProvider.GetRequiredService<IRefreshTokenRepository>();
        }

        #region Authenticate
        public ResponseLoginModel Authenticate(UserDtoModel model)
        {
            var account = _userRepository.Get(_=>_.Account == model.UserName, false, _=>_.RefreshTokens).FirstOrDefault();
            var token = CreateJWTToken(account);
            var refreshToken = GenerateRefreshToken();
            refreshToken.UserId = account.Id;
            _refreshTokenRepository.Add(refreshToken);

            // xóa mấy refreshtoken củ đi
            RemoveOldRefreshTokens(account);
            try
            {
                _refreshTokenRepository.Add(refreshToken);
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

        #region Refresh Token
        public ResponseLoginModel RefreshToken(string token)
        {
            var (refreshToken, user) = GetRefreshToken(token);
            var newRefreshToken = GenerateRefreshToken();

            newRefreshToken.UserId = user.Id;
            _refreshTokenRepository.Add(newRefreshToken);

            // xóa mấy refreshtoken củ đi
            RemoveOldRefreshTokens(user);
            try
            {
                _refreshTokenRepository.Add(refreshToken);
                _unitOfWork.SaveChange();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var jwtToken = CreateJWTToken(user);
            var response = _mapper.Map<ResponseLoginModel>(user);
            response.RefreshToken = refreshToken.Token;
            response.Token = jwtToken;

            return response;
        }
        #endregion Refresh Token

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
            var user = _userRepository.GetSingle(x => x.Id == id);
            if (user == null) throw new AppException("No user found!");

            _userRepository.Delete(user);
            _unitOfWork.SaveChange();

            return true;
        }
        #endregion Delete User

        #region Get By Account
        public UserEntity GetUserByAccount(string username)
        {
            return _userRepository.GetSingle(_ => _.Account == username && _.DateDeleted == null);
        }
        #endregion Get By Account

        #region Get By Id
        public UserGetListModel GetUserById(string id)
        {

            var user = _userRepository.GetSingle(_ => _.Id == id);
            return _mapper.Map<UserGetListModel>(user);
        }
        #endregion Get By Id

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
        private static RefreshToken GenerateRefreshToken()
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

        private void RemoveOldRefreshTokens(UserEntity userEntity)
        {
            foreach (var item in userEntity.RefreshTokens)
            {
                if (!item.IsActive &&
                    item.DateCreated.AddDays(2) <= SystemHelper.SystemTimeNow)
                {
                    //userEntity.RefreshTokens.Remove(item);
                    _refreshTokenRepository.Delete(item);
                }
            }
        }

        private (RefreshToken, UserEntity) GetRefreshToken(string token)
        {
            var account = _userRepository.Get()
                                         .Include(_=>_.RefreshTokens)
                                         .Where(y => y.RefreshTokens.Any( t => t.Token == token))
                                         .FirstOrDefault();
            if (account == null) throw new AppException("Invalid token");
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive) throw new AppException("Invalid token");
            return (refreshToken, account);
        }
    }
}

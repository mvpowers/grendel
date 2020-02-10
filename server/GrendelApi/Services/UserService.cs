using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GrendelApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(long phone, string password);
        Task<User> CreateUserPasswordResetToken(long phone);
        Task<User> UpdateUserPassword(string passwordResetToken, string password);
        Task<User> GetUserFromResetToken(string passwordResetToken);
        Task<User> GetUserFromAuthHeader(string authHeader);
    }
    public class UserService : IUserService
    {
        private readonly IAppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<User> Authenticate(long phone, string password)
        {
            var user = await _userRepository.GetUserFromPhone(phone);
            if (user == null) return null;
            
            // verify password
            var hasher = new PasswordHasher<string>();
            var userVerified = hasher.VerifyHashedPassword(user.Name, user.Password, password);
            if (userVerified == PasswordVerificationResult.Failed) return null;

            // generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            await _userRepository.UpdateUser(user);

            return user;
        }

        public async Task<User> CreateUserPasswordResetToken(long phone)
        {
            var user = await _userRepository.GetUserFromPhone(phone);

            if (user == null) return null;

            var token = Guid.NewGuid();
            user.PasswordResetToken = token.ToString();
            user.Token = null;

            await _userRepository.UpdateUser(user);

            return user;
        }
        
        public async Task<User> UpdateUserPassword(string passwordResetToken, string password)
        {
            var user = await _userRepository.GetUserFromPasswordResetToken(passwordResetToken);

            if (user == null) return null;
            
            var hasher = new PasswordHasher<string>();
            var hashedPassword = hasher.HashPassword(user.Name, password);
            
            user.Password = hashedPassword;
            user.PasswordResetToken = null;
            user.Token = null;
            
            await _userRepository.UpdateUser(user);
            return user;
        }

        public async Task<User> GetUserFromResetToken(string passwordResetToken)
        {
            var user = await _userRepository.GetUserFromPasswordResetToken(passwordResetToken);

            return user;
        }

        public async Task<User> GetUserFromAuthHeader(string authHeader)
        {
            var user = await _userRepository.GetUserFromAuthHeader(authHeader);

            return user;
        }
    }
}
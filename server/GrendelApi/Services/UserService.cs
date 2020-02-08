using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Users;
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
            var user = await _userRepository.GetUserFromPhonePassword(phone, password);

            if (user == null) return null;

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

            // generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.PasswordResetToken = tokenHandler.WriteToken(token);
            user.Token = null;

            await _userRepository.UpdateUser(user);

            return user;
        }

        private SecurityToken ValidateJwtToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenValidatorParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, tokenValidatorParameters, out var validatedToken);
            return validatedToken;
        }
        
        public async Task<User> UpdateUserPassword(string passwordResetToken, string password)
        {
            ValidateJwtToken(passwordResetToken);
            
            var user = await _userRepository.GetUserFromPasswordResetToken(passwordResetToken);

            if (user == null) return null;
            
            user.Password = password;
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
    }
}
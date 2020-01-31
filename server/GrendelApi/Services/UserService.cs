using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GrendelApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(long phone, string password);
        Task<int> GetUserIdFromAuthHeader(string authHeader);
    }
    public class UserService : IUserService
    {
        private readonly IAppSettings _appSettings;
        private readonly ILogger<UserService> _logger;
        private readonly GrendelContext _context;

        public UserService(IOptions<AppSettings> appSettings, ILogger<UserService> logger, GrendelContext context)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _context = context;
        }

        public async Task<User> Authenticate(long phone, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Phone == phone && x.Password == password);

            if (user == null) return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                // Expires = DateTime.UtcNow.AddDays(7),
                Expires = DateTime.UtcNow.AddSeconds(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<int> GetUserIdFromAuthHeader(string authHeader)
        {
            var token = authHeader.Replace("Bearer ", "");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Token == token) ?? throw new ArgumentNullException(nameof(token));
            
            return user.Id;
        }
    }
}
using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.Users
{
    public interface IUserRepository
    {
        Task<User> UpdateUser(User user);
        Task<int> GetUserIdFromJwt(string jwt);
        Task<User> GetUserFromPhonePassword(long phone, string password);
    }
    
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<User> _logger;
        private readonly GrendelContext _context;

        public UserRepository(ILogger<User> logger, GrendelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<User> UpdateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            
            return user;
        }

        public async Task<int> GetUserIdFromJwt(string jwt)
        {
            if (jwt == null) throw new ArgumentNullException(nameof(jwt));
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Token == jwt);
                if (user == null) throw new NoNullAllowedException(nameof(user));
                return user.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return 0;
        }

        public async Task<User> GetUserFromPhonePassword(long phone, string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (phone <= 0) throw new ArgumentOutOfRangeException(nameof(phone));

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Phone == phone && x.Password == password);
                
                if (user == null) throw new NoNullAllowedException(nameof(user));

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
    }
}
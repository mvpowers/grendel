using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.Users
{
    public interface IUserRepository
    {
        Task<User> UpdateUser(User user);
        Task<User> GetUserFromAuthHeader(string authHeader);
        Task<User> GetUserFromPhonePassword(long phone, string password);
        Task<User> GetUserFromPhone(long phone);
        Task<User> GetUserFromPasswordResetToken(string passwordResetToken);
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

        public async Task<User> GetUserFromAuthHeader(string authHeader)
        {
            if (authHeader == null) throw new ArgumentNullException(nameof(authHeader));
            
            try
            {
                var token = authHeader.Replace("Bearer ", "");
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Token == token);
                if (user == null) throw new NoNullAllowedException(nameof(user));
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
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
        
        public async Task<User> GetUserFromPhone(long phone)
        {
            if (phone <= 0) throw new ArgumentOutOfRangeException(nameof(phone));

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Phone == phone);
                
                if (user == null) throw new NoNullAllowedException(nameof(user));

                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }
        
        public async Task<User> GetUserFromPasswordResetToken(string passwordResetToken)
        {
            if (passwordResetToken == null) throw new ArgumentNullException(nameof(passwordResetToken));

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.PasswordResetToken == passwordResetToken);
                
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrendelData.Users
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> GetUserFromAuthHeader(string authHeader);
        Task<User> GetUserFromPhone(long phone);
        Task<User> GetUserFromPasswordResetToken(string passwordResetToken);
        Task<List<UserView>> GetActiveUsers();
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

        public async Task<List<UserView>> GetActiveUsers()
        {
            try
            {
                var users = await _context.Users
                    .Where(x => x.IsActive)
                    .ToListAsync();
                
                if (users == null) throw new NoNullAllowedException(nameof(users));

                return users.ToUserView();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return null;
        }

        public async Task<User> CreateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

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
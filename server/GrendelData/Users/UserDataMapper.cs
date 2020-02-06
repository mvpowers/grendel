using System;

namespace GrendelData.Users
{
    public static class UserDataMapper
    {
        public static UserView ToUserView(this User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            return new UserView()
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Token = user.Token,
                IsAdmin = user.IsAdmin,
                PasswordResetToken = user.PasswordResetToken
            };
        }
    }
}
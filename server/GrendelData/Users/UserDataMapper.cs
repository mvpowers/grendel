using System;
using System.Collections.Generic;

namespace GrendelData.Users
{
    public static class UserDataMapper
    {
        public static User ToUser(this UserCreateRequest userCreateRequest, string passwordResetToken)
        {
            if (userCreateRequest == null) throw new ArgumentNullException(nameof(userCreateRequest));
            if (string.IsNullOrEmpty(passwordResetToken)) throw new ArgumentNullException(nameof(passwordResetToken));

            return new User()
            {
                Name = userCreateRequest.Name,
                Phone = userCreateRequest.Phone,
                PasswordResetToken = passwordResetToken,
                IsAdmin = false,
                IsActive = true
            };
        }
        
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

        public static List<UserView> ToUserView(this List<User> users)
        {
            if (users == null) throw new ArgumentNullException(nameof(users));

            return users.ConvertAll(x => x.ToUserView());
        }

        public static UserSessionView ToUserSessionView(this User user, bool hasActiveSession, bool hasActiveVote)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            return new UserSessionView()
            {
                Id = user.Id,
                HasActiveSession = hasActiveSession,
                HasActiveVote = hasActiveVote
            };
        }
    }
}
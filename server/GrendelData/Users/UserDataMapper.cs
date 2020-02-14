using System;
using System.Collections.Generic;

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

        public static List<UserView> ToUserView(this List<User> users)
        {
            if (users == null) throw new ArgumentNullException(nameof(users));

            return users.ConvertAll(x => x.ToUserView());
        }

        public static UserSessionView ToUserSessionView(this User user, bool hasVotingExpired, bool hasActiveVote)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            return new UserSessionView()
            {
                Id = user.Id,
                HasVotingExpired = hasVotingExpired,
                HasActiveVote = hasActiveVote
            };
        }
    }
}
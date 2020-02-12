using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrendelData;
using GrendelData.Questions;
using GrendelData.Users;
using Microsoft.Extensions.Options;

namespace GrendelApi.Services
{
    public interface ILikeService
    {
    }
    
    public class LikeService : ILikeService
    {
        private readonly IUserRepository _userRepository;

        public LikeService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
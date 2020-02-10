using System;

namespace GrendelApi.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public override string Message { get; }

        public UserNotFoundException()
        {
            Message = $"UserNotFoundException: Unable to find user";
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
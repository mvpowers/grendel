using System;

namespace GrendelApi.Exceptions
{
    [Serializable]
    public class ReadEntityException : Exception
    {
        public override string Message { get; }

        public ReadEntityException(Type t)
        {
            Message = $"ReadEntityException: Unable to read {t}";
        }

        public ReadEntityException(string message) : base(message)
        {
        }

        public ReadEntityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
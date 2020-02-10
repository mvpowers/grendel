using System;

namespace GrendelApi.Exceptions
{
    [Serializable]
    public class CreateEntityException : Exception
    {
        public override string Message { get; }

        public CreateEntityException(Type t)
        {
            Message = $"CreateEntityException: Unable to create {t}";
        }

        public CreateEntityException(Type t, string message) : base(message)
        {
        }

        public CreateEntityException(Type t, string message, Exception inner) : base(message, inner)
        {
        }
    }
}
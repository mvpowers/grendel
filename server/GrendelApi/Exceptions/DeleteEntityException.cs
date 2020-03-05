using System;

namespace GrendelApi.Exceptions
{
    [Serializable]
    public class DeleteEntityException : Exception
    {
        public override string Message { get; }

        public DeleteEntityException(Type t)
        {
            Message = $"DeleteEntityException: Unable to delete {t}";
        }

        public DeleteEntityException(string message) : base(message)
        {
        }

        public DeleteEntityException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
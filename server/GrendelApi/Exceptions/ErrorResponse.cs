using System.Runtime.CompilerServices;

namespace GrendelApi.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }
        
        public string Message { get; set; }
    }
}
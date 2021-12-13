using MediatR;

namespace Presentation.ParametrObjects.Aspects
{
    public class ExceptionNotification : INotification
    {
        public ulong Id { get; init; }
        public string Message { get; init; }

        public ExceptionNotification(string message, ulong id)
        {
            Message = message;
            Id = id;
        }
    }
}

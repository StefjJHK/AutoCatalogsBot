namespace BusinessLogic.Services.Requests
{
    public interface IRequestService<Command, Response> 
        where Command : class
    {
        Response Execute(Command request);
    }
}

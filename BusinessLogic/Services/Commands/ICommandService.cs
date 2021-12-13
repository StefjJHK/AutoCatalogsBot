namespace BusinessLogic.Services.Commands
{
    public interface ICommandService<Command> 
        where Command : class
    {
        void Execute(Command command);
    }
}

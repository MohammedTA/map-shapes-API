namespace MapShapes.Domain.Commands
{
    using MediatR;

    public class CommandBase<T> : IRequest<T> where T : class
    {
    }
}
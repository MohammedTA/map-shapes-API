namespace MapShapes.Domain.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public abstract class HandlerBaseAsync<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
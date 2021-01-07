namespace MapShapes.Core.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IMediator mediator;

        public ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException();
        }

        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await this.mediator.Send(command);
        }

        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            return await this.mediator.Send(query);
        }

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null)
            {
                return this.NotFound();
            }

            return this.Ok(data);
        }
    }
}
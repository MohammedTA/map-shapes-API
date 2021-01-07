namespace MapShapes.Core.Api.Controllers
{
    using System.Threading.Tasks;
    using MapShapes.Domain.Commands.OverlayShapeCommands;
    using MapShapes.Domain.Models;
    using MapShapes.Domain.Queries.OverlayShapeQueries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class OverlayShapesController : ApiControllerBase
    {
        public OverlayShapesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpDelete("overlay-shapes/{id}")]
        public async Task Delete(int id)
        {
            await this.CommandAsync(new DeleteOverlayShapeCommand(id));
        }

        [HttpGet("overlay-shapes")]
        public async Task<PaginatedResultModel<OverlayShapeModel>> Get([FromQuery] GetOverlayShapesQuery request)
        {
            return await this.QueryAsync(request);
        }

        [HttpGet("overlay-shapes/{id}")]
        public async Task<OverlayShapeModel> Get(int id)
        {
            return await this.QueryAsync(new GetOverlayShapeQuery(id));
        }

        [HttpPost("overlay-shapes")]
        public async Task Post([FromBody] AddOverlayShapeCommand request)
        {
            await this.CommandAsync(request);
        }

        [HttpPut("overlay-shapes/{id}")]
        public async Task Put(int id, [FromBody] EditOverlayShapeCommand request)
        {
            request.Id = id;
            await this.CommandAsync(request);
        }
    }
}
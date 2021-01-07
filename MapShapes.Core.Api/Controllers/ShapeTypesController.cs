namespace MapShapes.Core.Api.Controllers
{
    using System.Threading.Tasks;
    using MapShapes.Domain.Commands.ShapeTypeCommands;
    using MapShapes.Domain.Models;
    using MapShapes.Domain.Queries.ShapeTypeQueries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class ShapeTypesController : ApiControllerBase
    {
        public ShapeTypesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpDelete("shape-types/{id}")]
        public async Task Delete(int id)
        {
            await this.CommandAsync(new DeleteShapeTypeCommand(id));
        }

        [HttpGet("shape-types")]
        public async Task<PaginatedResultModel<ShapeTypeModel>> Get([FromQuery] GetShapeTypesQuery request)
        {
            return await this.QueryAsync(request);
        }

        [HttpGet("shape-types/{id}")]
        public async Task<ShapeTypeModel> Get(int id)
        {
            return await this.QueryAsync(new GetShapeTypeQuery(id));
        }

        [HttpPost("shape-types")]
        public async Task Post([FromBody] AddShapeTypeCommand request)
        {
            await this.CommandAsync(request);
        }

        [HttpPut("shape-types/{id}")]
        public async Task Put(int id, [FromBody] EditShapeTypeCommand request)
        {
            request.Id = id;
            await this.CommandAsync(request);
        }
    }
}
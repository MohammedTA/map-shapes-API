namespace MapShapes.Domain.Queries
{
    using MapShapes.Domain.Models;
    using MediatR;

    public abstract class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
    }

    public abstract class PaginatedQueryBase<TResult> : IRequest<PaginatedResultModel<TResult>>
    {
        public bool Ascending { get; set; }
        public string OrderBy { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
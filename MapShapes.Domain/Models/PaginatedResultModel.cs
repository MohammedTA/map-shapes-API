namespace MapShapes.Domain.Models
{
    using System.Collections.Generic;

    public class PaginatedResultModel<T>
    {
        public int? MaxPageSize { get; set; }

        public IEnumerable<T> Results { get; set; }

        public int TotalCount { get; set; }
    }
}
namespace MapShapes.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using MapShapes.Domain.Models;
    using MapShapes.Domain.Queries;
    using Microsoft.EntityFrameworkCore;

    public static class Extensions
    {
        private const int DefaultPageSize = 1000;

        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    {
                        // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            var list = items.ToList();

            foreach (var item in list)
            {
                action(item);
            }

            return list;
        }

        /// <summary>
        ///     Sorts the elements of a sequence according to a key and the sort order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="query" />.</typeparam>
        /// <param name="query">A sequence of values to order.</param>
        /// <param name="key">Name of the property of <see cref="TSource" /> by which to sort the elements.</param>
        /// <param name="ascending">True for ascending order, false for descending order.</param>
        /// <returns>
        ///     An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted according to a key and sort
        ///     order.
        /// </returns>
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string key, bool ascending = true)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return query;
            }

            var lambda = (dynamic)CreateExpression(typeof(TSource), key);

            return ascending
                ? Queryable.OrderBy(query, lambda)
                : Queryable.OrderByDescending(query, lambda);
        }

        public static PaginatedResultModel<TSource> Paginate<TSource, TResult>(
            this IQueryable<TResult> query,
            Func<TResult, TSource> transform,
            PaginatedQueryBase<TSource> paginationParams,
            int? maxPageSize = null)
        {
            return query.PaginateAsync(
                    transform,
                    paginationParams.PageIndex,
                    paginationParams.PageSize,
                    paginationParams.OrderBy,
                    paginationParams.Ascending,
                    maxPageSize)
                .Result;
        }

        public static async Task<PaginatedResultModel<TSource>> PaginateAsync<TSource, TResult>(
            this IQueryable<TResult> query,
            Func<TResult, TSource> transform,
            PaginatedQueryBase<TSource> paginationParams,
            int? maxPageSize = null,
            CancellationToken cancellationToken = default)
        {
            return await query.PaginateAsync(
                transform,
                paginationParams.PageIndex,
                paginationParams.PageSize,
                paginationParams.OrderBy,
                paginationParams.Ascending,
                maxPageSize,
                cancellationToken);
        }

        public static async Task<PaginatedResultModel<TSource>> PaginateAsync<TSource, TResult>(
            this IQueryable<TResult> query,
            Func<TResult, TSource> transform,
            int pageNum,
            int pageSize,
            string orderBy = null,
            bool ascending = true,
            int? maxPageSize = null,
            CancellationToken cancellationToken = default)
        {
            //Total result count
            var rowsCount = query.Count();

            var queryResult = await query.GetResults(
                orderBy,
                ascending,
                pageSize,
                rowsCount,
                pageNum,
                cancellationToken);

            return new PaginatedResultModel<TSource>
            {
                Results = queryResult.Select(transform),
                TotalCount = rowsCount,
                MaxPageSize = maxPageSize
            };
        }

        public static async Task<T> SingleOrExceptionAsync<T>(
            this IQueryable<T> queryable,
            Expression<Func<T, bool>> where = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var item = where != null
                ? await queryable.SingleOrDefaultAsync(where, cancellationToken)
                : await queryable.SingleOrDefaultAsync(cancellationToken);

            if (item == null)
            {
                throw new BusinessException("The item is not registered in the system");
            }

            return item;
        }

        private static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");

            var body = propertyName.Split('.')
                .Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return Expression.Lambda(body, param);
        }

        private static async Task<IList<TResult>> GetResults<TResult>(
            this IQueryable<TResult> query,
            string orderBy,
            bool ascending,
            int pageSize,
            int rowsCount,
            int pageNum,
            CancellationToken cancellationToken = default)
        {
            if (pageSize <= 0)
            {
                pageSize = DefaultPageSize;
            }

            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0)
            {
                pageNum = 1;
            }

            //Calculate number of rows to skip on page size
            var excludedRows = (pageNum - 1) * pageSize;

            return await query
                .OrderBy(orderBy, ascending)
                .Skip(excludedRows)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
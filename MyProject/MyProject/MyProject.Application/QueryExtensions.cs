using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace MyProject
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Should apply sorting if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        public static IQueryable<TEntity> SortAndPaging<TEntity>(this IQueryable<TEntity> q, PagedAndSortedResultRequestDto input)
        {
            IQueryable<TEntity> query = q;

            //Try to sort query if available
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }
            else
            {
                query = query.OrderBy("ID Desc");
            }

            //Try to use paging if available
            query = query.PageBy(input);
            //Try to limit query result if available
            query = query.Take(input.MaxResultCount);

            return query;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
        {
            string[] propertySplit = propertyName.Split(' ');

            return OrderSort<T>(query, propertySplit[0], propertySplit[1].ToLower());
        }

        public static IQueryable<T> OrderSort<T>(IQueryable<T> Sour, string SortExpression, string Direction)
        {
            string SortDirection = string.Empty;
            if (Direction == "asc")
                SortDirection = "OrderBy";
            else if (Direction == "desc")
                SortDirection = "OrderByDescending";
            ParameterExpression pe = Expression.Parameter(typeof(T), SortExpression);
            PropertyInfo pi = typeof(T).GetProperty(SortExpression);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), SortDirection, types, Sour.Expression, Expression.Lambda(Expression.Property(pe, SortExpression), pe));
            IQueryable<T> query = Sour.Provider.CreateQuery<T>(expr);
            return query;
        }
    }
}

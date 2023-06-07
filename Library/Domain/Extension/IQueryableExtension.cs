using System.Linq.Expressions;

namespace Library.Domain.Extension
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> FilterDynamic<T>(this IQueryable<T> query,
            IQueryable<T> realQury, string fieldName, ICollection<string> values)
        {
            var param = Expression.Parameter(typeof(T), "e");
            // if (param.IsByRef != false) {
            var prop = Expression.PropertyOrField(param, fieldName);
            var body = Expression.Call(typeof(Enumerable), "Contains", new[] { typeof(string) },
                Expression.Constant(values), prop);
            var predicate = Expression.Lambda<Func<T, bool>>(body, param);
            return realQury.Where(predicate);
            // }
            // return query;

        }
    }
}

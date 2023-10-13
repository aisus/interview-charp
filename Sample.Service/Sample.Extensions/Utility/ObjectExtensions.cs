using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Sample.Extensions.Utility
{
    public static class ObjectExtensions
    {
        private static readonly ConcurrentDictionary<Type, Func<object, bool>> DefaultComparers =
            new ConcurrentDictionary<Type, Func<object, bool>>();

        private static Func<object, bool> CreateDefaultComparer(Type type)
        {
            var nullable = Nullable.GetUnderlyingType(type);
            if (nullable != null)
                type = nullable;

            var parameter = Expression.Parameter(typeof(object));
            var expression = Expression.Equal(
                Expression.Convert(parameter, type),
                Expression.Default(type));
            return Expression.Lambda<Func<object, bool>>(expression, parameter).Compile();
        }


        public static bool IsDefaultValue(this object value)
        {
            if (value == null)
                return true;
            var type = value.GetType();
            if (!DefaultComparers.TryGetValue(type, out var comparer))
            {
                if (!type.IsValueType && Nullable.GetUnderlyingType(type) == null)
                    return false;

                comparer = DefaultComparers.GetOrAdd(type, CreateDefaultComparer);
            }

            return comparer(value);
        }
    }
}
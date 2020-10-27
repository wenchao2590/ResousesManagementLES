using System;

namespace Infrustructure.Extensions
{
    public static class ConvertionExtensions
    {
        public static T ConvertTo<T>(this IConvertible convertibleValue)
        {
            if (null == convertibleValue || convertibleValue.Equals(string.Empty))
            {
                return default(T);
            }
            if (!typeof(T).IsGenericType)
            {
                return (T)Convert.ChangeType(convertibleValue, typeof(T));
            }
            else
            {
                Type genericTypeDefinition = typeof(T).GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return (T)Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(typeof(T)));
                }
            }
            throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", convertibleValue.GetType().FullName, typeof(T).FullName));

        }
    }
}

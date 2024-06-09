using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HF.Extensions
{
    public static class ClassExtensions
    {
        /// <summary>
        /// Property Setter
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property">target property</param>
        /// <param name="propertyValue">new property value</param>
        /// <typeparam name="T"></typeparam>
        public static void GiveAValue<T>(this T obj, Expression<Func<T, object>> property, object propertyValue)
            where T : class, new()
        {
            PropertyInfo propertyInfo = null;

            if (property.Body is MemberExpression expression)
                propertyInfo = expression?.Member as PropertyInfo;
            else
                propertyInfo = (((UnaryExpression)property.Body).Operand as MemberExpression)?.Member as PropertyInfo;

            if (propertyInfo.IsNotNull())
                propertyInfo.SetValue(obj, propertyValue);
        }

        /// <summary>
        /// Converts base class to derived class.
        /// </summary>
        /// <param name="obj">Class to convert</param>
        /// <param name="value">Target class</param>
        /// <typeparam name="T">Base class type</typeparam>
        /// <typeparam name="V">Derived class type</typeparam>
        /// <returns></returns>
        public static V ConvertToDerivedClass<T, V>(this T obj, V value) where V : T
        {
            return (V)Convert.ChangeType(obj, typeof(V));
        }
    }
}

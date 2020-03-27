namespace Olympus.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public static class XmlConverter
    {
        public static string Serialize<T>(IEnumerable<T> source)
        {
            var lambda = GetElement<T>(typeof(T));

            var elements = source.Select(lambda).ToArray();
            return new XElement("Root", elements).ToString();
        }

        private static XName GetElementName(Type type)
        {
            var attribute = type.GetCustomAttribute<XmlNameAttribute>();
            return attribute == null ? type.Name : attribute.Name;
        }

        private static XName GetPropertyName(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<XmlNameAttribute>();
            return attribute == null ? propertyInfo.Name : attribute.Name;
        }

        private static Func<T, XElement> GetElement<T>(Type type)
        {
            Expression<Func<XName, object, XAttribute>> attributeSetter = (name, @object) => new XAttribute(name, @object == null ? string.Empty : @object.ToString().Trim());
            var elementName = GetElementName(type);
            var parameter = Expression.Parameter(type);

            var args = GetExpressions(type, parameter, attributeSetter);

            var constructor = typeof(XElement).GetConstructor(new[] { typeof(XName), typeof(object[]) });
            var body = Expression.New(constructor, Expression.Constant(elementName), Expression.NewArrayInit(typeof(object), args));

            return Expression.Lambda<Func<T, XElement>>(body, parameter).Compile();
        }

        private static Expression[] GetExpressions(Type type, Expression expression, Expression lambda)
        {
            var result = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(p => p.GetCustomAttribute<XmlIgnoreAttribute>() == null)
                            .Select(p => GetExpressions(p, expression, lambda))
                            .ToArray();
            return result;
        }

        private static Expression GetExpressions(PropertyInfo propertyInfo, Expression expression, Expression lambda)
        {
            var property = Expression.Property(expression, propertyInfo);
            var name = GetPropertyName(propertyInfo);
            return Expression.Invoke(lambda, Expression.Constant(name), Expression.Convert(property, typeof(object)));
        }
    }
}
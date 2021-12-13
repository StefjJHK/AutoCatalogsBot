using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Presentation.Aspects.Utility
{
    public static class LoggingStatics
    {
        public static string GetMetadata<T>(T request)
        {
            var type = request.GetType();
            var properties = new List<PropertyInfo>(type.GetProperties());

            var properiesValues = properties
                .Select(property => $"{property.Name}: {property.GetValue(request)}");

            return string.Join("; ", properiesValues);
        }
    }
}

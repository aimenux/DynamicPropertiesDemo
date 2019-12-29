using System.Collections.Generic;

namespace Lib.Models.V1
{
    public class UserProfile : Dictionary<string, object>, IDynamicProperties
    {
        public string FirstName => GetPropertyValue<string>("first_name");

        public string LastName => GetPropertyValue<string>("last_name");

        public T GetPropertyValue<T>(string propertyName)
        {
            if (TryGetValue(propertyName, out var propertyValue))
            {
                return (T) propertyValue;
            }

            return default;
        }
    }
}

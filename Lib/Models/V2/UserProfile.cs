using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lib.Models.V2
{
    public class UserProfile : IDynamicProperties
    {
        public string FirstName => GetPropertyValue<string>("first_name");

        public string LastName => GetPropertyValue<string>("last_name");

        [JsonExtensionData] 
        public IDictionary<string, object> UnknownProperties { get; set; }

        public object this[string propertyName]
        {
            get => UnknownProperties[propertyName];
            set => UnknownProperties[propertyName] = value;
        }

        public T GetPropertyValue<T>(string propertyName)
        {
            if (UnknownProperties.TryGetValue(propertyName, out var propertyValue))
            {
                return (T) propertyValue;
            }

            return default;
        }
    }
}

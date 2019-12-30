namespace Lib.Models.V3
{
    public class UserProfile : CustomDynamicObject, IDynamicProperties
    {
        public string FirstName => GetPropertyValue<string>("first_name");

        public string LastName => GetPropertyValue<string>("last_name");

        public T GetPropertyValue<T>(string propertyName)
        {
            return (T) this[propertyName];
        }
    }
}

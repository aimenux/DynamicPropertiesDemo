namespace Lib.Models
{
    public interface IUnknownProperties
    {
        object this[string propertyName]  { get; }
        T GetPropertyValue<T>(string propertyName);
    }
}
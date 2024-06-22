namespace SellersAPI.Tests
{
    using Newtonsoft.Json;

    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}

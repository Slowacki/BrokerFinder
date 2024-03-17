using System.Text;
using Newtonsoft.Json;

namespace BrokerFinder.Cache.Helpers;

public static class SerializationHelper
{
    public static byte[] Serialize<T>(T? obj)
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
    }
    
    public static T? Deserialize<T>(byte[] bytes)
    {
        return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(bytes));
    }
}
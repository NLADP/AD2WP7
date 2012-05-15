using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AD2
{
    public static class Serializer
    {
        public static T Get<T>(string content)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream m = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                return (T) ser.ReadObject(m);
            }
        }
    }
}

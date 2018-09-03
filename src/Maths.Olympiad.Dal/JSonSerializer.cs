using Maths.Olympiad.Dal.Interfaces;
using Newtonsoft.Json;

namespace Maths.Olympiad.Dal
{
    public class JSonSerializer : ISerializer
    {
        public string Serialize<TData>(TData data)
        {
            var output = JsonConvert.SerializeObject(data);

            return output;
        }

        public TData Deserialize<TData>(string data)
        {
            var output = JsonConvert.DeserializeObject<TData>(data);

            return output;
        }
    }
}
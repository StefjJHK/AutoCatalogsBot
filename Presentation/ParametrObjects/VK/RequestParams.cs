using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Presentation.ParametrObjects
{
    [Serializable]
    public class RequestParams
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("group_id")]
        public long Id { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("object")]
        public JObject Object { get; set; }
    }
}

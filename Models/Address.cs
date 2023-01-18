using Newtonsoft.Json;

namespace FBAuthDemoAPI.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}

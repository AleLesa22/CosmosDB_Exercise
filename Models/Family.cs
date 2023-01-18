using Newtonsoft.Json;

namespace FBAuthDemoAPI.Models
{
    public class Family
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<Children> Children { get; set; }

        [JsonProperty(PropertyName = "parents")]
        public List<Parents> Parents { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "isRegistered")]
        public bool IsRegistered { get; set; }

    }
}

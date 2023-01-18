using Newtonsoft.Json;

namespace FBAuthDemoAPI.Models
{
    public class Pets
    {
        [JsonProperty(PropertyName = "givenName")]
        public string GivenName { get; set; }
    }
}

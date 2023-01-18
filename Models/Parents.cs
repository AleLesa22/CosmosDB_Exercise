using Newtonsoft.Json;

namespace FBAuthDemoAPI.Models
{
    public class Parents
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
    }
}

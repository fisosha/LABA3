using Newtonsoft.Json;

namespace Laba3.Models
{
    public class Student
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }
    }
}
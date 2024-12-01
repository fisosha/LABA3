using Newtonsoft.Json;

namespace Laba3.Models
{
    public class Lecture
    {
        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("lecturer")]
        public string Lecturer { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("students")]
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
using Newtonsoft.Json;

namespace Laba3.Models
{
    public class Schedule
    {
        [JsonProperty("lectures")]
        public List<Lecture> Lectures { get; set; } = new List<Lecture>();
    }
}

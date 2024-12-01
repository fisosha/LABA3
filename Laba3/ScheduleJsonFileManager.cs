using Laba3.Models;
using Newtonsoft.Json;

namespace Laba3
{
    public class JsonFileManager
    {
        public static Schedule LoadSchedule(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Шлях до файлу не може бути порожнім.");

            string jsonContent = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<Schedule>(jsonContent)
                   ?? new Schedule();
        }
        public static void SaveSchedule(string filePath, Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule), "Розклад не може бути null.");

            string jsonContent = JsonConvert.SerializeObject(schedule);
            File.WriteAllText(filePath, jsonContent);
        }
    }
}

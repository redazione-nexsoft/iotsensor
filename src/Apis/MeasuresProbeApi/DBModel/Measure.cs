namespace SensorsApi.DBModel
{
    public class Measure
    {
        public string sensorid { get; set; }
        public string type { get; set; }
        public double value { get; set; }
        public DateTime timestamp { get; set; }

    }
}
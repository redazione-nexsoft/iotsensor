namespace MeasuresProbeApi.DeviceModel
{
	public class DeviceProbe
	{
		public string sensorid { get; set; }

		public SensorProbe[] measures { get; set; }

	}
}
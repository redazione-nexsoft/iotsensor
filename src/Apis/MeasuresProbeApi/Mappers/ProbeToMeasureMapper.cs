using MeasuresProbeApi.DeviceModel;
using SensorsApi.DBModel;

namespace SensorsApi.Mappers
{
	public static class ProbeToMeasureMapper 
	{
		 
		public static Measure Map(DeviceProbe probe, SensorProbe m)
		{
			return new Measure { sensorid = probe.sensorid, timestamp = DateTime.Now, type = m.type, value = m.value };
		}
		 
	}
}
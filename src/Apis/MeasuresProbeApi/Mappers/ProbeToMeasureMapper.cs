using MeasuresProbeApi.DeviceModel;
using MeasuresProbeApi.DomainModel;

namespace SensorsApi.Mappers
{
    public static class ProbeToMeasureMapper 
	{
		 
		public static DBMeasure Map(DeviceProbe probe, SensorProbe m)
		{
			return new DBMeasure { sensorid = probe.sensorid, timestamp = DateTime.Now, type = m.type, value = m.value };
		}
		 
	}
}
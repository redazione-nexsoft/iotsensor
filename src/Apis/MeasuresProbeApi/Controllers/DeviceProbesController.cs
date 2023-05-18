using MeasuresProbeApi.DeviceModel;
using Microsoft.AspNetCore.Mvc;
using SensorsApi.Mappers;
using SensorsApi.Repositories;

namespace SensorsApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DeviceProbesController : ControllerBase
	{
		private readonly ILogger<DeviceProbesController> logger;
		private readonly IMeasureRepository repoMeasures;
		
		public DeviceProbesController(IMeasureRepository repoMeasures, ILogger<DeviceProbesController> logger)
		{
			this.logger = logger;
			this.repoMeasures = repoMeasures;
		}

		[HttpPost(Name = "NotifyProbe")]
		public async Task NotifyProbeAsync([FromBody] DeviceProbe probe)
		{
			logger.Log(LogLevel.Information, $"Probe from sensor: {probe.sensorid}");
			var tasks = probe.measures.Select(async m =>
			{
				var dbMeasure = ProbeToMeasureMapper.Map(probe, m);
				await repoMeasures.AddMeasure(dbMeasure);
			});
			await Task.WhenAll(tasks);
		}
	}
}
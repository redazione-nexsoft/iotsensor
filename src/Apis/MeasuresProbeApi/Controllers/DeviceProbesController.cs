using MeasuresProbeApi.DeviceModel;
using Microsoft.AspNetCore.Mvc;
using SensorsApi.Mappers;

namespace SensorsApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DeviceProbesController : ControllerBase
	{
		private readonly ILogger<DeviceProbesController> logger;
		private readonly HttpClient httpClient;
		
		public DeviceProbesController(IHttpClientFactory httpClientFactory, ILogger<DeviceProbesController> logger)
		{
			this.logger = logger;
			this.httpClient = httpClientFactory.CreateClient("MeasuresRepositoryApi");
		}

		[HttpPost(Name = "NotifyProbe")]
		public async Task NotifyProbeAsync([FromBody] DeviceProbe probe)
		{
			logger.Log(LogLevel.Information, $"Probe from sensor: {probe.sensorid}");
			var tasks = probe.measures.Select(async m =>
			{
				var dbRecord = ProbeToMeasureMapper.Map(probe, m);
				var req = await httpClient.PostAsync("/measures", JsonContent.Create(dbRecord));
				req.EnsureSuccessStatusCode();
			});
			await Task.WhenAll(tasks);
		}
	}
}
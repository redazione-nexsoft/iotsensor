using MeasuresRepositoryApi.Model;
using MeasuresRepositoryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MeasuresRepositoryApi.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class MeasuresController : ControllerBase
	{
		private readonly ILogger<MeasuresController> logger;
		private readonly IMeasureRepository repoMeasures;

		public MeasuresController(IMeasureRepository repoMeasures, ILogger<MeasuresController> logger)
		{
			this.logger = logger;
			this.repoMeasures = repoMeasures;
		}

		[HttpPost(Name = "Insert")]
		public async Task InsertAsync([FromBody] Measure measure)
		{
			await repoMeasures.AddMeasure(measure);
		}

		[HttpGet(Name = "GetAll")]
		public async Task<IEnumerable<Measure>> GetAll()
		{
			return await repoMeasures.GetMeasures();
		}
	}
}
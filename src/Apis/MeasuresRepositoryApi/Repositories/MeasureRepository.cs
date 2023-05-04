using MeasuresRepositoryApi.Model;

namespace MeasuresRepositoryApi.Repositories
{
    public class MeasureRepository : IMeasureRepository
	{
        public async Task<IEnumerable<Measure>> GetMeasures()
		{
			return await Task.FromResult(FakeMeasuresTable.Enumerate());
		}

		public async Task<Measure> GetMeasure(string sensorid, DateTime timestamp, string measureType)
		{
			return await Task.FromResult(FakeMeasuresTable.Read(sensorid, timestamp, measureType));
		}

		public async Task<Measure> AddMeasure(Measure measure)
		{
			return await Task.FromResult(FakeMeasuresTable.Insert(measure));
		}

		public async Task<Measure> UpdateMeasure(Measure measure)
		{
			return await Task.FromResult(FakeMeasuresTable.Update(measure));
		}

		public async Task<Measure> DeleteMeasure(string sensorid, DateTime timestamp, string measureType)
		{
			return await Task.FromResult(FakeMeasuresTable.Drop(new Measure { sensorid = sensorid, timestamp = timestamp, type = measureType }));
		}

    }
}
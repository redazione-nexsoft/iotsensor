using SensorsApi.DBModel;
 
namespace SensorsApi.Repositories
{
    public interface IMeasureRepository
    {
        Task<IEnumerable<Measure>> GetMeasures();
        Task<Measure> GetMeasure(string sensorid, DateTime timestamp, string measureType);
        Task<Measure> AddMeasure(Measure measure);
        Task<Measure> UpdateMeasure(Measure measure);
        Task<Measure> DeleteMeasure(string sensorid, DateTime timestamp, string measureType);
    }
}
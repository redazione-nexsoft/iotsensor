using System.Collections.Concurrent;

namespace SensorsApi.DBModel
{
    public static class FakeMeasuresTable
    {
        private const int MAX_INMEMORYITEMS = 50;
        private static ConcurrentDictionary<string, Measure> measures = new ConcurrentDictionary<string, Measure>();

        public static Measure Insert(Measure measure)
        {
            var key = GetKey(measure);
            while (measures.Count >= MAX_INMEMORYITEMS)
            {
                measures.TryRemove(measures.First());
            }
            if (!measures.TryAdd(key, measure))
            {
                throw new System.Data.DuplicateNameException();
            }
            return measure;
        }

        private static string GetKey(Measure measure)
        {
            return $"{measure.sensorid}_{measure.timestamp:yyyyMMddHHmmssfff}_{measure.type}";
        }

        public static Measure Read(string sensorid, DateTime timestamp, string type)
        {
            var key = GetKey(new Measure { sensorid = sensorid, timestamp = timestamp, type = type, });
            measures.TryGetValue(key, out Measure ret);
            return ret;
        }

        public static Measure Update(Measure measure)
        {
            var key = GetKey(measure);
            measures[key] = measure;
            return measures[key];
		}

        public static Measure Drop(Measure measure)
        {
            var key = GetKey(measure);
            measures.Remove(key, out Measure ret);
            return ret;
        }

        public static IEnumerable<Measure> Enumerate()
        {
            return measures.Values;
        }
    }
}
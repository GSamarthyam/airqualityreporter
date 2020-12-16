using System;
using System.Collections.Generic;
using System.Linq;

namespace AirQualityReport.After
{
    public class PollutantWiseFormatter : IFieldFormatter
    {
        // string Pollutant Heading is the key
        // LastUpdatedTime is the common value 
        // values are the tuple of Average, Max, Min, City

        //TODO: Not thread safe
        private static readonly Dictionary<string, List<Tuple<int, int, int, string>>> PollutantWiseReadings =
            new Dictionary<string, List<Tuple<int, int, int, string>>>();

        private readonly string _lastUpdatedTime = string.Empty;

        public PollutantWiseFormatter(IEnumerable<PollutionDataEntry> entries)
        {
            foreach (var entry in entries)
            {
                string heading = $"{entry.Pollutant} - {entry.State}";
                _lastUpdatedTime = entry.LastUpdatedTime;
                var pollutantEntry = new Tuple<int, int, int, string>(entry.Average, entry.Max, entry.Min, entry.City);
                if (!PollutantWiseReadings.ContainsKey(heading))
                {
                    PollutantWiseReadings[heading] = new List<Tuple<int, int, int, string>>();
                }
                PollutantWiseReadings[heading].Add(pollutantEntry);
            }
        }

        public IEnumerable<KeyValuePair<string, List<Tuple<int, int, int, string>>>> 
            Values => PollutantWiseReadings.AsEnumerable();

        public string LastUpdatedTime => _lastUpdatedTime;

        public string[] Headers => new string[] { "Average", "Max", "Min", "City" };
    }
}
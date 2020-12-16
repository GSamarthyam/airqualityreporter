using System;
using System.Collections.Generic;
using System.Linq;

namespace AirQualityReport.After
{
    public class CityWiseFormatter : IFieldFormatter
    {
        // string CityHeading is the key
        // LastUpdatedTime is the common value 
        // values are the tuple of Average, Max, Min, Pollutants 

        private static readonly Dictionary<string, List<Tuple<int, int, int, string>>> CityWiseReadings =
            new Dictionary<string, List<Tuple<int, int, int, string>>>();

        private readonly string _lastUpdatedTime = string.Empty;

        public CityWiseFormatter(List<PollutionDataEntry> entries)
        {
            foreach (var entry in entries)
            {
                string heading = $"{entry.Country} - {entry.State} - {entry.City} - {entry.Place}";
                _lastUpdatedTime = entry.LastUpdatedTime;

                var cityEntry = new Tuple<int, int, int, string>(entry.Average, entry.Max, entry.Min, entry.Pollutant);
                if (!CityWiseReadings.ContainsKey(heading))
                {
                    CityWiseReadings[heading] = new List<Tuple<int, int, int, string>>();
                }
                CityWiseReadings[heading].Add(cityEntry);
            }
        }

        public IEnumerable<KeyValuePair<string, List<Tuple<int, int, int, string>>>>
        Values => CityWiseReadings.AsEnumerable();

        public string LastUpdatedTime => _lastUpdatedTime;

        public string[] Headers => new string[] { "Average", "Max", "Min", "Pollutant" };

    }
}
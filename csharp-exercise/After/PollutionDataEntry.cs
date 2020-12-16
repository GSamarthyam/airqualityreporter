namespace AirQualityReport.After
{
    public class PollutionDataEntry
    {
        internal string Country { get; set; }
        internal string State { get; set; }
        internal string City { get; set; }
        internal string Place { get; set; }
        internal string LastUpdatedTime { get; set; }
        internal int Average { get; set; }
        internal int Max { get; set; }
        internal int Min { get; set; }
        internal string Pollutant { get; set; }

        private PollutionDataEntry()
        {
        } 

        private PollutionDataEntry(string country, string state, string city, string place, string lastUpdatedTime, int average,
            int max, int min, string pollutant)
        {
            Country = country;
            State = state;
            City = city;
            Place = place;
            LastUpdatedTime = lastUpdatedTime;
            Average = average;
            Max = max;
            Min = min;
            Pollutant = pollutant;
        }
        
        public class Builder
        {
            private PollutionDataEntry _pollutionDataEntry;

            public Builder()
            {
                _pollutionDataEntry = new PollutionDataEntry();
            }

            public Builder WithCountry(string country)
            {
                _pollutionDataEntry.Country = country;
                return this; 
            }
            
            public Builder WithState(string state)
            {
                _pollutionDataEntry.State = state;
                return this; 
            }
            
            public Builder WithCity(string city)
            {
                _pollutionDataEntry.City = city;
                return this; 
            }
            
            public Builder WithPlace(string place)
            {
                _pollutionDataEntry.Place = place;
                return this; 
            }
            
            public Builder WithLastUpdatedTime(string lastUpdatedTime)
            {
                _pollutionDataEntry.LastUpdatedTime = lastUpdatedTime;
                return this; 
            }
            
            public Builder OfAverage(int average)
            {
                _pollutionDataEntry.Average = average;
                return this; 
            }
            
            public Builder OfMax(int max)
            {
                _pollutionDataEntry.Max = max;
                return this; 
            }
            
            public Builder OfMin(int min)
            {
                _pollutionDataEntry.Min = min;
                return this; 
            }
            
            public Builder WithPollutant(string pollutant)
            {
                _pollutionDataEntry.Pollutant = pollutant;
                return this; 
            }

            public PollutionDataEntry Make()
            {
                return _pollutionDataEntry;
            }
        }
    }
    
}
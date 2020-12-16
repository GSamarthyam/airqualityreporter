using System;
using System.Collections.Generic;
using Aspose.Cells;

namespace AirQualityReport.After
{
    public class ExcelDataReader
    {
        private string Path { get; }

        public ExcelDataReader(string pathToMyExcel)
        {
            Path = pathToMyExcel; 
        }

        // helper function to convert the given string (in object type) to int
        // if cannot parse, then set the return value to defaultVal 
        static private int ConvertToInt(object val, int defaultVal)
        {
            int intVal;
            if(!Int32.TryParse((string) val, out intVal))
            {
                intVal = defaultVal; 
            }

            return intVal; 
        }
        private PollutionDataEntry GetPollutionDataEntryRow(Cells cells, int row)
        {
            var country = Convert.ToString(cells[row, 0].Value);
            var state = Convert.ToString(cells[row, 1].Value);
            var city = Convert.ToString(cells[row, 2].Value);
            var place = Convert.ToString(cells[row, 3].Value);
            var lastUpdatedTime = Convert.ToString(cells[row, 4].Value);

            var average = ConvertToInt(cells[row, 5].Value, -1);
            var max = ConvertToInt(cells[row, 6].Value, -1);
            int min = ConvertToInt(cells[row, 7].Value, -1);

            var pollutant = Convert.ToString(cells[row, 8].Value);

            var pollutionDataEntryBuilder = new PollutionDataEntry.Builder();

            var pollutionDataEntry = pollutionDataEntryBuilder.WithCountry(country)
                .WithCity(city)
                .WithState(state)
                .WithPlace(place)
                .WithLastUpdatedTime(lastUpdatedTime)
                .OfAverage(average)
                .OfMax(max)
                .OfMin(min)
                .WithPollutant(pollutant)
                .Make();
            return pollutionDataEntry;
        }
        public List<PollutionDataEntry> ReadWorksheet()
        {
            // open the workbook 
            using (var wb = new Workbook(Path))
            {
                // open the first worksheet from the workbook 
                var worksheet = wb.Worksheets[0];
                // Get the cells inside the worksheet 
                var cells = worksheet.Cells;
                // Get the row count 
                int rowCount = cells.MaxDataRow;

                var entries = new List<PollutionDataEntry>();
                // for every entry in the row - skip first one
                for (int row = 1; row <= rowCount; row++)
                {
                    var pollutionDataEntry = GetPollutionDataEntryRow(cells, row);
                    entries.Add(pollutionDataEntry);
                }
                return entries;
            }
        }
    }
}
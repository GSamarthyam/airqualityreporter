// This program takes the real-time generated air-quality index in different cities in India 
// and generates as the output a City-Wise PDF report with the air quality details

// This code does not adhere to SOLID principles and has numerous smells 
// Axis of change: the report may have to be generated Pollutant-wise in addition to City-wise reports 

// For excel reading, use Aspose library 
// For the PDF generation, using the iTextSharp library 
// Source for the dataset: https://www.kaggle.com/venky73/airquality 

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Cells;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace AirQualityReport.Before
{
    internal class AirQualityReporter
    {
        private static void ConvertEntriesToCitywiseEntries(List<Entry> entries)
        {
            foreach (var entry in entries)
                CityWiseEntry.AddToCityWiseEntry(entry.Country, entry.State, entry.City, entry.Place,
                    entry.LastUpdatedTime, entry.Average, entry.Max, entry.Min, entry.Pollutant);
        }

//        public static void Main(string[] args)
//        {
//            var path = @"./Assets/CityWiseReport.pdf";
//
//            // first read the file 
//            var entries = ReadXLS(@"./Assets/AirQuality-India-Realtime.xlsx");
//
//            // now transform into the desired class structure for generating the city-wise entries 
//            // the values are now in CityWiseEntry class 
//            ConvertEntriesToCitywiseEntries(entries);
//
//            // also transform into the desired class structure for pollutant-wise reports
//
//            // now generate the report for each city 
//            GenerateCityWiseReadingPDF(path, "India Pollution Report");
//
//            Console.WriteLine($"Report has been generated at {path}");
//
//            Console.ReadLine();
//
//        }

        public static List<Entry> ReadXLS(string PathToMyExcel)
        {
            // open the workbook 
            var wb = new Workbook(PathToMyExcel);

            // open the first worksheet from the workbook 
            var worksheet = wb.Worksheets[0];

            // Get the cells inside the worksheet 
            var cells = worksheet.Cells;

            // Get the row and column count
            var rowCount = cells.MaxDataRow;
            var columnCount = cells.MaxDataColumn;

            // Current cell value
            var strCell = "";

            var entries = new List<Entry>();
            // for every entry in the row - skip first one
            try
            {
                for (var row = 1; row <= rowCount; row++)
                {
                    var column1 = Convert.ToString(cells[row, 0].Value); // Country
                    var column2 = Convert.ToString(cells[row, 1].Value); // State 
                    var column3 = Convert.ToString(cells[row, 2].Value); // City  
                    var column4 = Convert.ToString(cells[row, 3].Value); // Place 
                    var column5 = Convert.ToString(cells[row, 4].Value); // Last Updated Time

                    var column6 = -1; // Average - could be "NA" instead of a int, so the ToInt32 will fail for that
                    try
                    {
                        column6 = Convert.ToInt32(cells[row, 5].Value);
                    }
                    catch (FormatException fe)
                    {
                    }

                    var column7 = -1; // Max -- could be "NA" instead of a int, so the ToInt32 will fail for that
                    try
                    {
                        column7 = Convert.ToInt32(cells[row, 6].Value);
                    }
                    catch (FormatException fe)
                    {
                    }

                    var column8 = -1; // Min - could be "NA" instead of a int, so the ToInt32 will fail for that
                    try
                    {
                        column8 = Convert.ToInt32(cells[row, 7].Value);
                    }
                    catch (FormatException fe)
                    {
                    }

                    var column9 = Convert.ToString(cells[row, 8].Value); // Pollutant  

                    var entry = new Entry(column1, column2, column3, column4, column5, column6, column7, column8,
                        column9);

                    entries.Add(entry);
                }
            }
            catch (Exception fe)
            {
                // doesn't occur for this .csv when I tested, so ignoring any exceptions  
            }

            return entries;
        }

        public static void GenerateCityWiseReadingPDF(string path, string Heading)
        {
            // instantiate a new document
            var document = new Document(PageSize.LETTER, 15, 15, 15, 15);

            // instantiate the writer that will listen to the document
            var writer =
                PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));

            // open the document
            document.Open();

            var html = string.Empty;
            // build the document
            var heading =
                $"<h1> {Heading} </h1> <br> <br> <h2> Generated on: {CityWiseEntry.LastUpdatedTime} </h2> <br> ";
            var bodystart = "<body>";
            var tableheading =
                @"<br><table border=""1""> <tr> <th>Average</th> <th>Max</th><th>Min</th><th>Pollutant</th>";
            html += heading + bodystart;

            foreach (var cityWiseReading in CityWiseEntry.CityWiseReadings)
            {
                var cityheading = $"<h2> {cityWiseReading.Key} </h2>";
                html += cityheading;
                html += tableheading;
                foreach (var reading in cityWiseReading.Value)
                {
                    var tablecontent =
                        $"<tr> <td>{reading.Item1}</td> <td>{reading.Item2}</td><td>{reading.Item3}</td><td>{reading.Item4}</td></tr>";
                    html += tablecontent;
                }

                var tableending = @"</table><br>";
                html += tableending;
            }

            var bodyend = "</body>";
            html += bodyend;
            var htmlend = "</html>";
            html += htmlend;
            var elements =
                HTMLWorker.ParseToList(new StringReader(html), null);

            // add the collection to the document
            foreach (var element in elements) document.Add(element);

            //close the document
            document.Close();
        }

        public class Entry
        {
            public Entry(string Country, string State, string City, string Place, string LastUpdatedTime, int Average,
                int Max, int Min, string Pollutant)
            {
                this.Country = Country;
                this.State = State;
                this.City = City;
                this.Place = Place;
                this.LastUpdatedTime = LastUpdatedTime;
                this.Average = Average;
                this.Max = Max;
                this.Min = Min;
                this.Pollutant = Pollutant;
            }

            public string Country { get; }
            public string State { get; }
            public string City { get; }
            public string Place { get; }
            public string LastUpdatedTime { get; }
            public int Average { get; }
            public int Max { get; }
            public int Min { get; }
            public string Pollutant { get; }

            public override string ToString()
            {
                return $"{Country}:{State}:{City}:{Place}:{LastUpdatedTime}:{Average}:{Max}:{Min}:{Pollutant}";
            }
        }

        public class CityWiseEntry : Entry
        {
            // string CityHeading is the key
            // LastUpdatedTime is the common value 
            // values are the tuple of Average, Max, Min, Pollutants 

            public static Dictionary<string, List<Tuple<int, int, int, string>>> CityWiseReadings =
                new Dictionary<string, List<Tuple<int, int, int, string>>>();

            public static string LastUpdatedTime = string.Empty;

            // Don't call CityWiseEntry constructor - rather, call the AddToCityWiseEntry to add a new entry 
            public CityWiseEntry() : base(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0,
                string.Empty)
            {
            }

            public static void AddToCityWiseEntry(string Country, string State, string City, string Place,
                string UpdatedTime, int Average, int Max, int Min, string Pollutant)
            {
                var heading = $"{Country} - {State} - {City} - {Place}";
                LastUpdatedTime = UpdatedTime;
                var cityEntry = new Tuple<int, int, int, string>(Average, Max, Min, Pollutant);
                if (!CityWiseReadings.ContainsKey(heading))
                    CityWiseReadings[heading] = new List<Tuple<int, int, int, string>>();
                CityWiseReadings[heading].Add(cityEntry);
            }
        }
    }
}
// This program takes the real-time generated air-quality index in different cities in India 
// and generates as the output a City-Wise PDF report with the air quality details

// For excel reading, use Aspose library 
// For the PDF generation, using the iTextSharp library 
// Source for the dataset: https://www.kaggle.com/venky73/airquality 

using System;
using System.Collections.Generic;
using System.Configuration;

namespace AirQualityReport.After
{
    class AirQualityReporter
    {
        static void Main()
        {
            var entries = ReadPlPollutionDataEntries(out var airQualityDataPath, 
                out var excelDataReader);

            var reportGenerator = new ReportGenerator();
            
            GenerateCityWiseReport(entries, reportGenerator);

            // finally, generate report based on pollutants 
            GeneratePollutantReport(entries, reportGenerator);
        }

        private static void GeneratePollutantReport(List<PollutionDataEntry> entries, ReportGenerator reportGenerator)
        {
            var pollutantReportPath = ConfigurationManager.AppSettings["PollutantReportPath"];
            var pollutantFormatter = new PollutantWiseFormatter(entries);

            reportGenerator.Generate(pollutantFormatter, pollutantReportPath,
                "India Pollution Report - Pollutant - wise");
            Console.WriteLine($"PollutantWise Report generated at {pollutantReportPath}.");
        }

        private static void GenerateCityWiseReport(List<PollutionDataEntry> entries, ReportGenerator reportGenerator)
        {
            // now transform into the desired class structure for generating the city-wise entries
            // and generate the report 
            var citywiseReportPath = ConfigurationManager.AppSettings["CitywiseReportPath"];
            var cityFormatter = new CityWiseFormatter(entries);
            reportGenerator.Generate(cityFormatter, citywiseReportPath,
                "India Pollution Report - City-wise");
            Console.WriteLine($"CityWise Report generated at {citywiseReportPath}.");
        }

        private static List<PollutionDataEntry> ReadPlPollutionDataEntries(out string airQualityDataPath, out ExcelDataReader excelDataReader)
        {
            airQualityDataPath = ConfigurationManager.AppSettings["AirQualityDataPath"];

            excelDataReader = new ExcelDataReader(airQualityDataPath);
            var entries = excelDataReader.ReadWorksheet();
            return entries;
        }
    }
}

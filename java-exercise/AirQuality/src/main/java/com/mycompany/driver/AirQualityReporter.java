package com.mycompany.driver;

import com.mycompany.pollutantdata.FeedFactory;
import com.mycompany.pollutantdata.PollutantEntry;
import com.mycompany.reporting.CitywiseReportingStrategy;
import com.mycompany.reporting.PollutantwiseReportingStrategy;
import com.mycompany.reporting.Report;

import java.util.List;

/* The airquality reporter class is the driver class (i.e., with the main method)
   It reads the input from the AirQuality-India-Realtime.csv file and generates to
   kinds of pollutant reports - Citywise pollutant report & pollutant-wise report
* */
public class AirQualityReporter {
    public static void main(String[] args) throws Exception {
        String currentDir = System.getProperty("user.dir");
        String filePath =  currentDir + "/AirQuality/resources/AirQuality-India-Realtime.csv";

        FeedFactory feedFactory = new FeedFactory(filePath);
        List<PollutantEntry> pollutantEntries = feedFactory.getAirQualityDataFeed();

        Report citywiseReport = new Report("India Pollution Report - City-wise", pollutantEntries);
        citywiseReport.generate(new CitywiseReportingStrategy(), "CitywiseReport.pdf");

        Report pollutantwiseReport = new Report("India Pollution Report - Pollutant-wise", pollutantEntries);
        pollutantwiseReport.generate(new PollutantwiseReportingStrategy(), "PollutantwiseReport.pdf");
    }
}

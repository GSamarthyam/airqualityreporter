package com.mycompany.reporting;

import com.mycompany.pollutantdata.PollutantEntry;

import java.time.LocalDateTime;
import java.util.Arrays;
import java.util.List;
import java.util.Map;

/* Report class abstracts the details of the report & its kind - it has a heading to print,
   the time of report creation, and the table data values to be printed as part of the report
   along with the table headings.
 */
public class Report {
    private String heading;
    private LocalDateTime dateTime;

    private List<PollutantEntry> pollutantEntries;

    public Report(String heading, List<PollutantEntry> pollutantEntries) {
        this.heading = heading;
        this.pollutantEntries = pollutantEntries;
        this.dateTime = pollutantEntries.get(0).getLastUpdate();
    }

    public void generate(ReportingStrategy reportingStrategy, String fileName) throws Exception {
        Map<String, List<String>> entries =
                reportingStrategy.getReportDataFromPollutantEntries(pollutantEntries);
        List<String> columnHeaders = Arrays.asList("Average", "Max", "Min", "Pollutant");
        PDFReport pdfReport = new PDFReport(heading, dateTime);
        pdfReport.generate(fileName, entries, columnHeaders);
    }

}


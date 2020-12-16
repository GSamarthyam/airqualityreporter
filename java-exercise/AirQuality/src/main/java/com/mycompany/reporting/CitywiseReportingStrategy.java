package com.mycompany.reporting;

import com.mycompany.pollutantdata.PollutantEntry;
import com.mycompany.pollutantdata.PollutionData;

import java.util.Arrays;
import java.util.List;
import java.util.Map;

/*
Strategy implementation  to add relevant pollutant entries into a map for the report
for the key location string, the values are average, max, min and pollutant entries
*/
public class CitywiseReportingStrategy extends ReportingStrategy {
    @Override
    public void addSelectReportEntriesToMap(Map<String, List<String>> reportEntries,
                                            PollutantEntry pollutantEntry) {
        String locationString = pollutantEntry.getLocation().toString();
        PollutionData pollutionData = pollutantEntry.getPollutionData();
        List<String> tableDataEntries = Arrays.asList(pollutionData.getAvg(),
                pollutionData.getMax(), pollutionData.getMin(), pollutionData.getPollutant());
        appendMapEntries(reportEntries, locationString, tableDataEntries);
    }
}

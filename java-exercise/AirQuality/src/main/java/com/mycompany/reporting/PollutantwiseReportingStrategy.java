package com.mycompany.reporting;

import com.mycompany.pollutantdata.PollutantEntry;
import com.mycompany.pollutantdata.PollutionData;

import java.util.Arrays;
import java.util.List;
import java.util.Map;

/*
Strategy implementation  to add relevant pollutant entries into a map for the report
for the key pollutant string, the values are average, max, min and city entries
*/
public class PollutantwiseReportingStrategy extends ReportingStrategy {
    // Helper function to add relevant pollutant entries into a map for the report
    //  the key pollutant string consists of pollutant name and state,
    //
    @Override
    public void addSelectReportEntriesToMap(Map<String, List<String>> reportEntries,
                                            PollutantEntry pollutantEntry) {
        String pollutantString = pollutantEntry.getPollutionData().getPollutant()
                + " - " + pollutantEntry.getLocation().getState();
        PollutionData pollutionData = pollutantEntry.getPollutionData();
        List<String> tableDataEntries = Arrays.asList(pollutionData.getAvg(),
                pollutionData.getMax(), pollutionData.getMin(), pollutantEntry.getLocation().getCity());
        appendMapEntries(reportEntries, pollutantString, tableDataEntries);
    }
}

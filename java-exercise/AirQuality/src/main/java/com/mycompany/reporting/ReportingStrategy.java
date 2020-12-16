package com.mycompany.reporting;

import com.mycompany.pollutantdata.PollutantEntry;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

/* ReportingStrategy lets the client choose which approach to choose for generating the reports:
   examples: is it pollutant-based on city based; constructs a map of key (the table headings)
   and values (the corresponding pollutant values)
 */
public abstract class ReportingStrategy {
    Map<String, List<String>> getReportDataFromPollutantEntries(List<PollutantEntry> pollutantEntries) {
        Map<String, List<String>> reportEntries = new TreeMap<>();
        pollutantEntries.forEach(pollutantEntry -> {
            addSelectReportEntriesToMap(reportEntries, pollutantEntry);
        });
        return reportEntries;
    }

    protected abstract void addSelectReportEntriesToMap(Map<String, List<String>> reportEntries,
                                                        PollutantEntry pollutantEntry);

    // Helper function to append values if not already present in the Map
    protected void appendMapEntries(Map<String, List<String>> map, String key, List<String> values) {
        values.forEach(value -> map.computeIfAbsent(key, k -> new ArrayList<>()).add(value));
    }
}


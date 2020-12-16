package com.mycompany.pollutantdata;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

/* The FeedFactory is responsible for reading the entries from the pollutant data feed
   and creating corresponding PollutantEntry objects.
 */
public class FeedFactory {
    private String filePath;

    public FeedFactory(String path) {
        filePath = path;
    }

    public List<PollutantEntry> getAirQualityDataFeed() throws IOException {
        List<String> lines = readRawLinesFromFile();
        removeFirstEntryHeader(lines);
        return getPollutantEntries(lines);
    }

    private List<String> readRawLinesFromFile() throws IOException {
        return Files.lines(Paths.get(filePath)).collect(Collectors.toList());
    }

    private void removeFirstEntryHeader(List<String> lines) {
        // remove the header line (at index 0 of the List) before processing
        lines.remove(0);
    }

    private List<PollutantEntry> getPollutantEntries(List<String> lines) {
        List<PollutantEntry> pollutantEntries = new ArrayList<>();
        lines.forEach(line -> processPollutantEntry(line, pollutantEntries));
        return pollutantEntries;
    }

    private void processPollutantEntry(String feedEntry, List<PollutantEntry> pollutantEntries) {
        // the format of the feed entries is:
        // Country, State, City, Place, Lastupdate, Avg, Max, Min, Pollutants
        // String, String, String, String, LocalDateTime, float, float, float, String
        String[] csvEntries = feedEntry.split(",");

        // the first four entries are Country, State, City, Place
        Location location = new Location(csvEntries[0], csvEntries[1], csvEntries[2], csvEntries[3]);

        LocalDateTime time = getPollutantDataFeedDateTime(csvEntries[4]);

        // the last four entries are the pollution data
        PollutionData pollutionData = new PollutionData(csvEntries[5],
                                            csvEntries[6], csvEntries[7], csvEntries[8]);

        PollutantEntry.Builder builder = new PollutantEntry.Builder();
        builder.location(location);
        builder.lastUpdate(time);
        builder.pollutionData(pollutionData);
        PollutantEntry pollutantEntry = builder.build();

        pollutantEntries.add(pollutantEntry);
    }

    private LocalDateTime getPollutantDataFeedDateTime(String csvEntry) {
        // format like: 21-12-2018 03:00:00
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm:ss");
        // the next entry is local time
        return LocalDateTime.parse(csvEntry.trim(), formatter);
    }
}

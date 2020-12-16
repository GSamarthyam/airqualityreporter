package com.mycompany.pollutantdata;

import java.time.LocalDateTime;

/* The pollutant entry corresponds to the abstractions in the "AirQuality-India-Realtime.csv file
   The abstractions are Location, date/time of generating the feed, and the actual pollution readings
 */
public class PollutantEntry {
    private Location location;
    private LocalDateTime lastUpdate;
    private PollutionData pollutionData;

    public LocalDateTime getLastUpdate() {
        return lastUpdate;
    }

    public void setLastUpdate(LocalDateTime lastUpdate) {
        this.lastUpdate = lastUpdate;
    }

    public Location getLocation() {
        return location;
    }

    public void setLocation(Location location) {
        this.location = location;
    }

    public PollutionData getPollutionData() {
        return pollutionData;
    }

    public void setPollutionData(PollutionData pollutionData) {
        this.pollutionData = pollutionData;
    }

    /* The Builder class corresponds to the PollutantEntry - it helps create a Pollutant entry
       object given the location object, time of reading, and the actual pollution reading
     */
    public static class Builder {
        PollutantEntry pollutantEntry = new PollutantEntry();

        public Builder() {
        }

        public Builder location(Location location) {
            pollutantEntry.location = location;
            return this;
        }

        public Builder lastUpdate(LocalDateTime lastUpdate) {
            pollutantEntry.lastUpdate = lastUpdate;
            return this;
        }

        public Builder pollutionData(PollutionData pollutionData) {
            pollutantEntry.pollutionData = pollutionData;
            return this;
        }

        public PollutantEntry build() {
            return pollutantEntry;
        }
    }
}

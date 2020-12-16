package com.mycompany.pollutantdata;

/* Class that abstracts the pollution data values, i.e., average, max, min and the pollutant name
 */
public class PollutionData {
    private String avg;
    private String max;
    private String min;
    private String pollutant;

    public PollutionData(String avg, String max, String min, String pollutant) {
        this.avg = avg;
        this.min = min;
        this.max = max;
        this.pollutant = pollutant;
    }

    public String getPollutant() {
        return pollutant;
    }

    public void setPollutant(String pollutant) {
        this.pollutant = pollutant;
    }

    public String getAvg() {
        return avg;
    }

    public void setAvg(String avg) {
        this.avg = avg;
    }

    public String getMax() {
        return max;
    }

    public void setMax(String max) {
        this.max = max;
    }

    public String getMin() {
        return min;
    }

    public void setMin(String min) {
        this.min = min;
    }

    @Override
    public String toString() {
        return "PollutionData{" +
                "avg='" + avg + '\'' +
                ", max='" + max + '\'' +
                ", min='" + min + '\'' +
                ", pollutant='" + pollutant + '\'' +
                '}';
    }
}

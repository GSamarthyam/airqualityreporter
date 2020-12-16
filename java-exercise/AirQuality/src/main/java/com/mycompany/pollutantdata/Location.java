package com.mycompany.pollutantdata;

/* Location represents the place from which the pollutant reading was performed */
public class Location {
    private String country;
    private String state;
    private String city;
    private String place;
    public Location(String country, String state, String city, String place) {
        this.country = country;
        this.state = state;
        this.city = city;
        this.place = place;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }

    public String getState() {
        return state;
    }

    public void setState(String state) {
        this.state = state;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getPlace() {
        return place;
    }

    public void setPlace(String place) {
        this.place = place;
    }

    @Override
    public String toString() {
        return country + " - " + state + " - " + city + " - " + place;
    }
}

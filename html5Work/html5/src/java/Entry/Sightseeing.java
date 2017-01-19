/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Entry;

/**
 *
 * @author sanshou
 */
public class Sightseeing {
 
    private String name;
    private String location;
    private String duration;
    private String detail;
    private String photo;
    private String price;
    
    public Sightseeing() {
        this.name = "";
        this.location = "";
        this.duration = "";
        this.detail = "";
        this.photo = "";
        this.price = "";
    }

    public Sightseeing(String name, String location, String duration,String detail,String photo,String price) {
        
        this.name = name;
        this.location = location;
        this.duration = duration;
        this.detail = detail;
        this.photo = photo;
        this.price = price;
    }

   
    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }
    public String getDuration() {
        return duration;
    }

    public void setDuration(String duration) {
        this.duration = duration;
    }
    public String getDetail() {
        return detail;
    }

    public void setDetail(String detail) {
        this.detail = detail;
    }
    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }
    public String getPrice() {
        return price;
    }

    public void setPrice(String price) {
        this.price = price;
    }
}

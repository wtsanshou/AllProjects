/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package DB;

import Entry.Sightseeing;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;

/**
 *
 * @author sanshou
 */
public class DbBean {
    
    public DbBean(){
    
    }
     public static void main(String []args){
      DbBean db = new DbBean();
//      db.Add("Cliffs of Moher", "Limerick, Ireland", "14 hours", 
//              "Explore Ireland\'\'s west coast highlights on a coach and rail day trip from Dublin to the wild Cliffs of Moher, the Burren and picturesque Galway Bay.",
//              "ireland-logo1.jpg", "€ 109");
      
      ArrayList<Sightseeing> result = db.findByName("Giant's Causeway");
      System.out.println(result.listIterator().next().getName());
     }
     
     public ArrayList<Sightseeing> getAllSightings(){
         ConnectionPool pool=ConnectionPool.getInstance();
        Connection con = pool.getConnection();
        PreparedStatement pstmt = null;
        ResultSet rs = null;
        String SQL = "SELECT * FROM sightseeings";
         System.out.println(SQL);
        ArrayList<Sightseeing> entries = new ArrayList<Sightseeing>();
        try{
            pstmt = con.prepareStatement(SQL);
            rs = pstmt.executeQuery();
            while(rs.next()){
                Sightseeing entry = new Sightseeing(rs.getString("NAME"),rs.getString("location"), rs.getString("duration"),
                        rs.getString("detail"),rs.getString("photo"),rs.getString("price"));
                entries.add(entry);
            }
            rs.close();
            pstmt.close();
        } catch (Exception e){
            e.printStackTrace();
        }
         finally
        {
            try
            {
                pstmt.close();
                con.close();
            }
            catch(Exception e)
            {
                e.printStackTrace();
            }
        }
        return entries;
     }
     
     public void Add(String name, String location, String duration,String detail,String photo,String price){
         ConnectionPool pool=ConnectionPool.getInstance();
         Connection con=pool.getConnection();
         Statement stmt = null;
          String SQL = "INSERT INTO sightseeings ( name, location,duration,detail,photo,price) "
                  + "VALUES (\'"+name+"\',\'"+location+"\',\'"+duration+"\',\'"+detail+"\',\'"+photo+"\',\'"+price+"\')";
          System.out.println(SQL);
         try{
            stmt = con.createStatement();
            if(stmt.executeUpdate(SQL)==1)
            {
                
                System.out.println("insert success.....");
            }
            
            stmt.close();
            
        } catch (Exception e){
            e.printStackTrace();
            
        }
        finally
        {
            try
            {
                stmt.close();
                con.close();
            }
            catch(Exception e)
            {
                e.printStackTrace();
            }
        }
     
     }
     
     public ArrayList<Sightseeing> findByName(String name){
         ConnectionPool pool=ConnectionPool.getInstance();
        Connection con = pool.getConnection();
        PreparedStatement pstmt = null;
        ResultSet rs = null;
        String SQL = "SELECT * FROM sightseeings where name =\'"+name+"\'";
         System.out.println(SQL);
        ArrayList<Sightseeing> entries = new ArrayList<Sightseeing>();
        try{
            pstmt = con.prepareStatement(SQL);
            rs = pstmt.executeQuery();
            while(rs.next()){
                Sightseeing entry = new Sightseeing(rs.getString("NAME"),rs.getString("location"), rs.getString("duration"),
                        rs.getString("detail"),rs.getString("photo"),rs.getString("price"));
                entries.add(entry);
            }
            rs.close();
            pstmt.close();
        } catch (Exception e){
            e.printStackTrace();
        }
         finally
        {
            try
            {
                pstmt.close();
                con.close();
            }
            catch(Exception e)
            {
                e.printStackTrace();
            }
        }
        return entries;
     }
}

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package DB;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

/**
 *
 * @author sanshou
 */
public class ConnectionPool {
    
    private static ConnectionPool pool;
    String dbURL = "jdbc:mysql://localhost:3306/html5";
    String dbDriver = "com.mysql.jdbc.Driver";  
    String dbName = "root";
    String dbPassword = "admin";

    private ConnectionPool()
    {
        try
        {
            Class.forName(dbDriver);//Loading database management classes(Through this class can connect this database

        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    public static ConnectionPool getInstance()
    {
        if(pool==null)
            pool=new ConnectionPool();
        return pool;
    }

    public Connection getConnection()//get an connection to database
    {
        try
        {
            return DriverManager.getConnection(dbURL, dbName, dbPassword);
        }
        catch (SQLException ex)
        {
            return null;
        }
    }
}


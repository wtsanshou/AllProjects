<%@ page language="Java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>

<input type="text" id="forMap"  onblur="codeAddress()"/>
          <div id="pagination" >
             
                
                
           
            <%@ page language="Java" import="java.util.ArrayList" %>
            <%@ page language="Java" import="Entry.Sightseeing" %>

            <jsp:useBean id="db" scope="request" class="DB.DbBean" />

            <jsp:setProperty name="db" property="*" />
            
            <% 
                ArrayList<Sightseeing> result = db.getAllSightings();
                int i = result.size();
            %>
            
                
                    
                
                <%
                for(int j = 0; j<i;j++) { 
                %>
                <a href="#">
                  <table class="sight" >  
                    <tr>
                    <td rowspan="3"><img src="images/<%= result.get(j).getPhoto()%>" /></td>
                    <td>Name:</td>
                    <td class="name"><%= result.get(j).getName() %></td>
                    </tr>
                    <tr>
                    <td>Duration:</td>
                    <td><%= result.get(j).getDuration() %></td>
                    </tr>
                    <tr>
                    <td>Price:</td>
                    <td><%= result.get(j).getPrice() %>
                    <input type="text" class="location" value="<%= result.get(j).getLocation() %>"/>
                    </td>
                    </tr>
                  </table>
                    
                </a>
                <%
                           }
                %>
              
                <a href="#"><button id ="up"></button></a>
                <a href="#"><button id="down"></button></a>
             
                
          </div>
          <div id="map"></div>
          <div id="popupBack">
              </div>
             <div id="popupContact">
                 
            </div> 
          
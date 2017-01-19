

<%@ page language="Java" contentType="text/html; charset=UTF-8"
    pageEncoding="UTF-8"%>

<%@ page language="Java" import="java.util.ArrayList" %>
            <%@ page language="Java" import="Entry.Sightseeing" %>

            <jsp:useBean id="db" scope="request" class="DB.DbBean" />

            <jsp:setProperty name="db" property="*" />
            <%
                String name = request.getParameter("name");

                ArrayList<Sightseeing> detail = db.findByName(name);
                
            %>


<div id="closeBack"><button id="popupContactClose" style="width: 50px;height:25px;">X</button>  </div>
                    
                     <table id="popupDetail">
                      <tr>
                        <td id="row1">Name:</td>
                        <td id="row2"><%= detail.listIterator().next().getName() %></td>
                       
                      </tr>
                      <tr>
                        <td>Location:</td>
                        <td><%= detail.listIterator().next().getLocation() %></td>
                      </tr>
                      <tr>
                        <td>Duration:</td>
                        <td><%= detail.listIterator().next().getDuration() %></td>
                      </tr>
                      <tr>
                        <td>Price:</td>
                        <td><%= detail.listIterator().next().getPrice() %></td>
                      </tr>
                      <tr>
                        <td colspan="2" id="det"><%= detail.listIterator().next().getDetail() %></td>
                      </tr>
                    </table>
<img src="images/<%= detail.listIterator().next().getPhoto() %>"/>
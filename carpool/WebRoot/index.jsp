<%@ page language="java" import="java.util.*" pageEncoding="GBK"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
  	<meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />

<title>��ҳ</title>

<script type="text/javascript" src="js/jquery.js"></script>
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />

    <script src="http://maps.google.com/maps/api/js?sensor=true" type="text/javascript"></script>
    <script src="http://google-maps-utility-library-v3.googlecode.com/svn/trunk/routeboxer/src/RouteBoxer.js" type="text/javascript"></script>
     <script type="text/javascript" src="js/map.js"></script>
     <link type="text/css" rel="stylesheet" href="css/index.css" />
     <script type="text/javascript">
   	function search(){
		with(document.getElementById("searchForm")){
				method = "post";
				action = "form/unLoginSearch.do";
				submit();
			}	
	}
	function login(){
		with(document.getElementById("searchForm")){
				method = "post";
				action = "form/login.do";
				submit();
			}	
	}
</script>

<style type="text/css">
	
	
</style> 

	
  </head>
  
  <body onLoad="initialize()" >
<form id="searchForm" name="form1" >
<div style="display:none">
	  <input type="text" id="scord" name="scord"/>
		<input type="text" id="ecord" name="ecord"/>
		<button id="undo" style="margin:auto" onClick="undo()">Undo</button>
	  
</div>
<center>
	
	<div>
	  <table width="100%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	    
	      <td width="55%">&nbsp;</td>
	      <td width="7%" style="color:white;font-size:15px;" align="right">�û���:</td>
	      <td width="10%"><input name="username" type="text" size="15"></td>
	      <td width="5%" style="color:white;font-size:15px" align="right">����:</td>
	      <td width="10%"><input name="password" type="password" size="15"></td>
	      <td width="8%" style=";font-size:15px"><a href="#"   onclick="login()">��¼ |</a></td>
	      <td width="5%" style=";font-size:15px"><a href="form/regist.jsp">ע��</a></td>
	      
	    </tr>
	
	  </table>
	</div>
<div style="padding-top:20px" >
	<div style="margin:0px;align:center">
		<a   style="cursor:hand" ><img style="width:18%" src="images/logo1.jpg" /></a>
	</div>
	<div >
	<table cellspacing="20">
		
		<tr></tr>
		<tr>
			<td style="font-size:20px;color:#0A5CA7;font-family:��Բ;font-weight:bold">���:</td>
			<td><input type="text" name="cp_inf_start" id="start" style="font-size:20px" size="20"  onBlur="showstart()"/> </td>
			<td style="font-size:20px;color:#0A5CA7;font-family:��Բ;font-weight:bold">�յ�:</td>
			<td><input type="text" name="textfield2" id="end" style="font-size:20px" size="20"  onBlur="showend()"/></td>
			
		</tr>
		<tr></tr>
		<tr >
			<td width="10%"></td>
			
				<td colspan="3" align="center">
				<div style="width:130px;">
					<p id="test" onClick="search();">��&nbsp;&nbsp;&nbsp;&nbsp; ��</p>
				</div>
				</td>
				<td width="5%"> <input type="button" value="�鿴��·" onClick="route();" style="display:none"/></td>
			<td><input type="button" name="button" id="button" value="��   ��" onClick="search();" style="display:none"/></td>
				
		</tr>
	</table>
	</div>	
</div>
<div style="clear:both">	
	
  
  <div style="display:none">
	<div id="map"  ></div>      
	<div id="directionsPanel"  >	
	</div>
  </div>  	
</div>

  
  <p>&nbsp;</p>
  
</center>
</form>
</body>
</html>

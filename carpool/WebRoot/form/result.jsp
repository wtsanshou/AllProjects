<%@ page language="java" contentType="text/html; charset=GBK"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <title>搜索结果</title>
    
	<meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />

<script type="text/javascript" src="../js/jquery.js"></script>
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />

    <script src="http://maps.google.com/maps/api/js?sensor=true" type="text/javascript"></script>
    <script src="http://google-maps-utility-library-v3.googlecode.com/svn/trunk/routeboxer/src/RouteBoxer.js" type="text/javascript"></script>
     <link type="text/css" rel="stylesheet" href="../css/main.css" />
     <script type="text/javascript" src="../js/map.js"></script>
     <script type="text/javascript">
     	
     
     	$(function(){
     		$(".light").mouseover(function(){
     			$("#hidden").css("display","block");
     		});
     		$(".light").hover(function(){
     			$(this).css("background-color","#0A5CA7");
     			$(this).css("color","#FFFFFF");
     			$(this).css("cursor","hand");
     			$("#start").val($(this).find(".start").text());
     			$("#end").val($(this).find(".end").text());
     			route();
     		},function(){
     			$(this).css("background-color","#EEEEEE");
     			$(this).css("color","#000000");
     		});
     		
     		$(".light").click(function(){
     			with(this){
     			method = "post";
				action = "route.do";
				submit();
				}	
			});
     	});
     	
     	function exit(){
     		with(document.getElementById("routeForm")){
     				method = "post";
					action = "exit.do";
					submit();
     			}
     	}
</script>
<style type="text/css">
	
	
</style> 
     
  </head>
  
  <body onLoad="initialize()">
<div>
	<div style="float:left;margin:0px;width:10%">
		<a id="logo" style="cursor:hand" ><img src="../images/logo.jpg" /></a>
	</div>
	
	<div>
	  <table width="86%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	    
	      <td width="40%">&nbsp;</td>
	     
	      <td width="18%" style="color:white;font-size:15px">${user}欢迎您！ |</td>
	     
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/postInfo.jsp">信息发布 |</a></td>
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/success.jsp">返回主页 |</a></td>
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/personal.jsp">个人主页 |</a></td>
	      <td width="5%" style=";font-size:15px;color:white"><a href="#" onClick="exit()">退出</a></td>
	    </tr>
	
	  </table>
	</div>
<div style="padding-top:0px;padding-left:50px" >
	<center>
		<h1>搜寻结果</h1>
	</center>
	<table >
		<tr>
			
			<td><input type="text" name="cp_inf_start" id="start" style="font-size:20px" size="25"  style="display:none"/> </td>
			
			<td><input type="text" name="textfield2" id="end" style="font-size:20px" size="25"  style="display:none"/></td>
			
			
			<td> <input type="button" value="查看线路" onClick="route();" style="display:none"/></td>
			<td><input type="button" name="button" id="button" value="搜   索" onClick="search();" style="display:none"/></td>
			
		</tr>
	
	</table>	   	
		
	</div>
</div>	
<div style="clear:both">
  <div style="float:left;background-color:#FFFFFF;height:600px;width:30%">
  	
  	<table width="300" style="border:none;" >
   		<tr>
   			<td>
   				<h1 style="color:#0A5CA7">${emptyList}</h1>
   			</td>
   		</tr>
	  <c:forEach var = "info" items = "${infoList}" >
	  <form name="routeForm" class="light" id="routeForm" >
	  	<table style="font-size:12px" >
	  		<tr>
    <th width="67"  scope="col">起点:</th>
    
    <th width="159"  scope="col" class="start" >${info.cpInfStart}</th>
    <th width="84"  scope="col">终点:</th>
    <th width="156" scope="col" class="end"><c:out value="${info.cpInfEnd}" /></th>
  </tr>
  <tr>
    <td >人数限制:</td>
    <td ><c:out value="${info.cpInfPeopNumb}" /></td>
    <td >已报名人数:</td>
    <td ><c:out value="${info.cpInfAvilableNum}" /></td>
  </tr>
  <tr>
    <td >拼车周期:</td>
    <td ><c:out value="${info.cpInfFrequency}" /></td>
    <td >性别要求:</td>
    <td ><c:out value="${info.cpInfSex}" /></td>
  </tr>
  <tr>
    <td >出发时间:</td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
    <td >&nbsp;</td>
  </tr>
  <tr>
  			<td style="display:none">
  				<input name="routeID" value="${info.cpInfId}" />
  			</td>	
  				
  </tr>
</table>
	  	
	</form>   	
	    
	  </c:forEach>	
	
	  </table>
	
	</div>
  
   <div  style="background-color:#EEEEEE;height:44px;width:100%"></div>
	<div id="map" style="width: 100%; height: 600px;" ></div>    
	    
	<div id="directionsPanel"  style="position:absolute; right:0px; top:87px; width:200px;background-color:#CEEDFF">
		
	</div>
	<div style="position:absolute; right:10px; top:105px;">
		<a href="#" ><img title="隐藏面板" style="display:none;border:0px" id="hidden" src="../images/mark1.jpg"  /></a>
		
	</div>
	<div style="position:absolute; right:10px; top:105px;">
		
		<a href="#" ><img title="显示面板" style="display:none;border:0px" id="show" src="../images/mark2.jpg" /></a>
	</div>
 </div>
 <button id="undo" style="display:none;margin:auto" onClick="undo()">Undo          </button>
  </body>
</html>

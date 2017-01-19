<%@ page language="java" import="java.util.*" pageEncoding="GBK"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
  	<meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />

<title>主页</title>

<script type="text/javascript" src="../js/jquery.js"></script>
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />

    <script src="http://maps.google.com/maps/api/js?sensor=true" type="text/javascript"></script>
    <script src="http://google-maps-utility-library-v3.googlecode.com/svn/trunk/routeboxer/src/RouteBoxer.js" type="text/javascript"></script>
     <script type="text/javascript" src="../js/map.js"></script>
     <link type="text/css" rel="stylesheet" href="../css/main.css" />
     <script type="text/javascript">
    
    
		//var a = $(".t2").attr("value");
		//alert(a);
		//if(a == "3"){
		//	alert(a);
		//	$(".t1").css("display","block");
		//}
   
	function search(){
		with(document.getElementById("searchForm")){
				method = "post";
				action = "search.do";
				submit();
			}	
	}
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
     			$(this).css("background-color","#FFFFFF");
     			$(this).css("color","#000000");
     		});
     		
     		$(".light").click(function(){
     			with(this){
     			method = "post";
				action = "route.do";
				submit();
				}	
			});
			$("#exit").click(function(){
     			with(document.getElementById("searchForm")){
     				method = "post";
					action = "exit.do";
					submit();
     			}
     		});
     		$(".page").hover(function(){
     			$(this).css("font-size","30px");
     		},function(){
     			$(this).css("font-size","15px");
     		});
     		
     		$(".page").click(function(){
     			var a = $(this).text();
     			
     			//alert(a);
     			if(a == "1"){
     				$("#p1").css("display","block");
     				$("#p2").css("display","none");
     				$("#p3").css("display","none");
     				$("#p4").css("display","none");
     			}else if(a == "2"){
     				$("#p1").css("display","none");
     				$("#p2").css("display","block");
     				$("#p3").css("display","none");
     				$("#p4").css("display","none");
     			}else if(a == "3"){
     				$("#p1").css("display","none");
     				$("#p2").css("display","none");
     				$("#p3").css("display","block");
     				$("#p4").css("display","none");
     			}else if(a == "4"){
     				$("#p1").css("display","none");
     				$("#p2").css("display","none");
     				$("#p3").css("display","none");
     				$("#p4").css("display","block");
     			}
     		});
     	});
</script>

<style type="text/css">
	.tab {
		margin:0px;
		font-size:12px;
		
	}
	.tab td{
		width: 120px;
		border: 0px;
	}
</style>  

	
  </head>
  
  <body onLoad="initialize()">
<form id="searchForm" name="form1" >
<div style="display:none">
	  <input type="text" id="scord" name="scord"/>
		<input type="text" id="ecord" name="ecord"/>
		<button id="undo" style="margin:auto" onClick="undo()">Undo</button>
	  
</div>
<div>
	<div style="float:left;margin:0px;width:10%">
		<a id="logo"  style="cursor:hand" ><img src="../images/logo.jpg" /></a>
	</div>
	
	<div>
	  <table width="86%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	    
	      <td width="40%">&nbsp;</td>
	     
	      <td width="15%" style="color:white;font-size:15px">${user}欢迎您！ |</td>
	     
	      <td width="8%" style=";font-size:15px"><a href="../form/postInfo.jsp">信息发布 |</a></td>
	      
	      <td width="8%" style=";font-size:15px"><a href="../form/personal.jsp">个人主页 |</a></td>
	      <td width="5%" style=";font-size:15px"><a href="#" id="exit">退出</a></td>
	    </tr>
	
	  </table>
	</div>
<div style="padding-top:20px" align="center">
	
	<table >
		<tr>
			<td style="font-size:20px;color:#0A5CA7;font-family:幼圆;font-weight:bold">起点:</td>
			<td><input type="text" name="cp_inf_start" id="start" style="font-size:20px" size="20"  onBlur="showstart()"/> </td>
			<td style="font-size:20px;color:#0A5CA7;font-family:幼圆;font-weight:bold">终点:</td>
			<td><input type="text" name="textfield2" id="end" style="font-size:20px" size="20"  onChange="showend()"/></td>
			<td>
				<div>
					<p id="test" onClick="search();">搜   索</p>
				</div>
			</td>
			<td> <input type="button" value="查看线路" onClick="route();" style="display:none"/></td>
			<td><input type="button" name="button" id="button" value="搜   索" onClick="search();" style="display:none"/></td>
			
		</tr>
	
	</table>	   	
		
	</div>
</div>	

</form>
  
<div style="clear:both; margin-top:0px">
	<div style="float:left;background-color:#FFFFFF;height:650px;width:28%">
  		<table  width="100%" style="border-bottom:2px;color:#0A5CA7" >
        <tr>
        <td height="45" colspan="4">
        	<table>
        		<tr>
        		<c:forEach var = "paging" items = "${len}" >
        		<td width="10%">
        		<a href="#" class="page"><c:out value="${paging}"/></a>
        		</td>
        		</c:forEach>
        		</tr>
        	</table>
        </td>
        	
        </tr>
        <tr>
        	<td colspan="4" align="center"><h2>最新发布</h2></td>
        </tr>
        <tr> 
        	<th width="25%" align="center"><h5>起点</h5></th>
        	<th width="25%" align="center"><h5>终点</h5></th>
        	<th width="25%" align="center"><h5>出发时间</h5></th>
        	<th width="25%" align="center"><h5>发布时间</h5></th>
        </tr>
        </table>
        	<hr>
        	<div id="p1">
      		<c:forEach var = "info" items = "${t1}" >
      		
		      	<form class="light">
			      	<table   class="tab">
				      	<tr>
				      		<td align="center" class="start"><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" class="end"><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="right"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      		<td style="display:none">
				  				<input name="routeID" value="${info.cpInfId}" />
				  			</td>
				      	</tr>
			      	</table>
			      	<hr>
		      	</form> 
      		</c:forEach>
      		</div >
      		<div id="p2" style="display:none">
      		<c:forEach var = "info" items = "${t2}" >
      		
		      	<form class="light">
			      	<table   class="tab">
				      	<tr>
				      		<td align="center" class="start"><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" class="end"><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="right"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      		<td style="display:none">
				  				<input name="routeID" value="${info.cpInfId}" />
				  			</td>
				      	</tr>
			      	</table>
			      	<hr>
		      	</form> 
      		</c:forEach>
      		</div>
      		<div id="p3" style="display:none">
      		<c:forEach var = "info" items = "${t3}" >
      		
		      	<form class="light">
			      	<table   class="tab">
				      	<tr>
				      		<td align="center" class="start"><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" class="end"><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="right"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      		<td style="display:none">
				  				<input name="routeID" value="${info.cpInfId}" />
				  			</td>
				      	</tr>
			      	</table>
			      	<hr>
		      	</form> 
      		</c:forEach>
      		</div>
      		<div id="p4" style="display:none">
      		<c:forEach var = "info" items = "${t4}" >
      		
		      	<form class="light">
			      	<table   class="tab">
				      	<tr>
				      		<td align="center" class="start"><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" class="end"><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="right"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      		<td style="display:none">
				  				<input name="routeID" value="${info.cpInfId}" />
				  			</td>
				      	</tr>
			      	</table>
			      	<hr>
		      	</form> 
      		</c:forEach>
      		</div>
		</div>	
	<div  style="background-color:#EEEEEE;height:44px;"></div>
	<div id="map" style="width: 100%; height: 500px;" ></div>    
	    
	<div id="directionsPanel"  style="position:absolute; right:0px; top:87px; width:200px;background-color:#CEEDFF">
		
	</div>
	<div style="position:absolute; right:10px; top:105px;">
		<a href="#" ><img title="隐藏面板" style="display:none;border:0px" id="hidden" src="../images/mark1.jpg"  /></a>
		
	</div>
	<div style="position:absolute; right:10px; top:105px;">
		
		<a href="#" ><img title="显示面板" style="display:none;border:0px" id="show" src="../images/mark2.jpg" /></a>
	</div>
	

  <p>&nbsp;</p>
  

</body>
</html>

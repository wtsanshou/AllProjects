<%@ page language="java" contentType="text/html; charset=GBK"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html>
  <head>
  	<meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />

<title>个人主页</title>

<script type="text/javascript" src="../js/jquery.js"></script>
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />

    <script src="http://maps.google.com/maps/api/js?sensor=true" type="text/javascript"></script>
    <script src="http://google-maps-utility-library-v3.googlecode.com/svn/trunk/routeboxer/src/RouteBoxer.js" type="text/javascript"></script>
     <script type="text/javascript" src="../js/map.js"></script>
     <link type="text/css" rel="stylesheet" href="../css/index.css" />
  
   <script type="text/javascript">
  
   		$(function(){
     		$(".light").hover(function(){
     			$(this).css("background-color","#0A5CA7");
     			$(this).css("color","#FFFFFF");
     			$(this).css("cursor","hand");
     		},function(){
     			$(this).css("background-color","#EEEEEE");
     			$(this).css("color","#000000");
     		});
     		$(".light").click(function(){
     			var pp = $(this).find("#pp").attr("value");
     			var aa = $(this).find("#aa").attr("value");
     			if(pp == "" || aa == ""){
     				alert("当前信息为空！");
     			}else {
     				with(this){
	     				method = "post";
						action = "route.do";
						submit();
					}	
     			}
			});
			$("#exit").click(function(){
     			with(document.getElementById("routeForm")){
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
     		
     		$("#n").click(function(){
     			$("#news").css("display","block");
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
	}
	.ui{
		line-height:40px;
		border-color:#0A5CA7;
		font-size: 12px;
		width: 100%;
	}
	.ui td {
		border:0px;
	}
</style>  

	
  </head>
  
<body onLoad="initialize()">

<div>
	<div style="float:left;width:10%;margin-left:100px" >
		<a id="logo"  style="cursor:hand" ><img src="../images/logo.jpg" /></a>
	</div>
	
	<div style="width:100%">
	  <table width="90%" border="0" style="background-color:#0A5CA7;" id="head">
	    <tr bordercolor="#FFFFFF" >
	    <td width="20%" style="display:block" ><a id="n" href="#">${news}</a></td>
	      <td width="5%">&nbsp;</td>
	     
	      <td width="29%" style="color:white;font-size:15px;" align="right" >${user}欢迎您！ |</td>
	     <td width="11%" style=";font-size:15px"><a href="../form/change.jsp">修改信息 |</a></td>
	      <td width="11%" style=";font-size:15px"><a href="../form/postInfo.jsp">信息发布 |</a></td>
	      
	      <td width="11%" style=";font-size:15px"><a href="../form/success.jsp">返回主页 |</a></td>
	      <td width="5%" style=";font-size:15px"><a href="#" id="exit">退出</a></td>
	    </tr>
	
	  </table>
	</div>
	<div align="center">
		<h1>${userinfo.userNickname}的空间  </h1>
	</div>
	<div id="news" style="position:absolute;top:40px;right:80px;display:none">
		<table border="1" style="border-color:#0A5CA7">
			<tr>
				<td width="200px" style="font-size:12px;border:0px;font-color:#FFFF00">${message.test2}</td>
			</tr>
		</table>
		
	</div>
</div>	
<div style="padding-top:25px;padding-left:400px;display:none;" >
	
	<table >
		<tr>
			<td style="font-size:21px;color:#0A5CA7;font-family:幼圆;font-weight:bold">起点:</td>
			<td><input type="text" name="cp_inf_start" id="start" style="font-size:20px" size="15"  onBlur="showstart()"/> </td>
			<td style="font-size:21px;color:#0A5CA7;font-family:幼圆;font-weight:bold">终点:</td>
			<td><input type="text" name="textfield2" id="end" style="font-size:20px" size="15"  onChange="showend()"/></td>
			<td>
				<div >
					<p id="test" onClick="search();">搜   索</p>
				</div>
			</td>
			<td> <input type="button" value="查看线路" onClick="route();" style="display:none"/></td>
			<td><input type="button" name="button" id="button" value="搜   索" onClick="search();" style="display:none"/></td>			
		</tr>
	</table>
	</div>
	<p>&nbsp;</p>
<div style="width:100%">
		<div style="float:left;width:20%;margin-left:105px;height:600px">
			<table class="ui" border="1.5" style="height:70%">
				<tr>
				<td ><img style="width:100px" src="${userinfo.userProfile}"/></td><td>
						<table>
						  <tr><td>${userinfo.userNickname}</td></tr>
						  <tr>
						    <td>等级:</td><td>${userinfo.userLevel}</td>
						  </tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>个性签名:</td><td>${userinfo.userIndr}</td>
				</tr>
				
				<tr>
					<td>性别:</td><td>${userinfo.userSex}</td>
				</tr>
				<tr>
					<td>拼车成功次数:</td><td>${userinfo.userSuccessTimes}</td>
				</tr>
				<tr>
					<td>拼车失败次数:</td><td>${userinfo.userBadTimes}</td>
				</tr>
			</table>
		
		</div>
		
	<div style="width:40%;margin-left:2px;float:left;">
		<table class="ui" border="1" >
		<tr>
		<td>
			<table  width="100%" style="border-bottom:2px;border:none;color:#0A5CA7" >
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
	        	<th width="25%">起点</th>
	        	<th width="25%">终点</th>
	        	<th width="25%">出发时间</th>
	        	<th width="25%">发布时间</th>
	        </tr>
	     	</table>	
	    	<div id="p1" >
	      		<c:forEach var = "info" items = "${t1}" >
		      	<form name="routeForm" class="light" id="routeForm" >
			      	<table  class="tab">
			      	
				      	<tr>
				      		<td align="center" ><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" ><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="center"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      	</tr>
				      	 <tr>
				  			<td style="display:none"><input name="routeID" value="${info.cpInfId}" />
				  			<br></td>	
  				
  						</tr>
			      	</table>
		      	</form> 
      			</c:forEach>
      		</div>
      		<div id="p2" style="display:none">
	      		<c:forEach var = "info" items = "${t2}" >
		      	<form name="routeForm" class="light" id="routeForm" >
			      	<table  class="tab">
			      	
				      	<tr>
				      		<td align="center" ><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" ><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="center"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      	</tr>
				      	 <tr>
				  			<td style="display:none"><input name="routeID" value="${info.cpInfId}" />
				  			<br></td>	
  				
  						</tr>
			      	</table>
		      	</form> 
      			</c:forEach>
      		</div>
      		<div id="p3" style="display:none">
	      		<c:forEach var = "info" items = "${t3}" >
		      	<form name="routeForm" class="light" id="routeForm" >
			      	<table  class="tab">
			      	
				      	<tr>
				      		<td align="center" ><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" ><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="center"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      	</tr>
				      	 <tr>
				  			<td style="display:none"><input name="routeID" value="${info.cpInfId}" />
				  			<br></td>	
  				
  						</tr>
			      	</table>
		      	</form> 
      			</c:forEach>
      		</div>
      		<div id="p4" style="display:none">
	      		<c:forEach var = "info" items = "${t4}" >
		      	<form name="routeForm" class="light" id="routeForm" >
			      	<table  class="tab">
			      	
				      	<tr>
				      		<td align="center" ><c:out value="${info.cpInfStart}" /></td>
				      		<td align="center" ><c:out value="${info.cpInfEnd}" /></td>
				      		<td align="center"><c:out value="${info.cpInfDepartTime}" /></td>
				      		<td align="center"><c:out value="${info.cpInfPublishTime}" /></td>
				      	</tr>
				      	 <tr>
				  			<td style="display:none"><input name="routeID" value="${info.cpInfId}" />
				  			<br></td>	
  				
  						</tr>
			      	</table>
		      	</form> 
      			</c:forEach>
      		</div>
      	</td>
      </tr>
      	</table>	
	</div>
	<div style="width:75%" >
			<form class="light" id="routeForm">
				<table class="ui"  border="1" id="published">
					<tr><th colspan="2" align="center" >已发布线路</th></tr>
					<tr><td>起点:</td><td id="p" >${published.cpInfStart}</td></tr>
					<tr><td>终点:</td><td >${published.cpInfEnd}</td></tr>
					<tr><td>出发时间:</td><td>${published.cpInfDepartTime }</td></tr>
					<tr><td style="display:none"><input name="routeID" id="pp" value="${published.cpInfId}" /></tr>
				</table>
			</form>
			
			<form class="light" id="routeForm">
				<table class="ui" border="1" id="applied">
					<tr><th colspan="2" align="center">已报名线路</th></tr>
					<tr style="display:none"><td><input name="lost" value="1"/></td></tr>
					<tr><td>起点:</td><td id="a" >${applied.cpInfStart }</td></tr>
					<tr><td>终点:</td><td >${applied.cpInfEnd }</td></tr>
					<tr><td>出发时间:</td><td>${applied.cpInfDepartTime }</td></tr>
					<tr><td style="display:none"><input name="routeID" id="aa" value="${applied.cpInfId}" /></tr>
				</table>
			</form>
			
			<h3 align="center">参与过的线路</h3>
			<c:forEach var = "info" items = "${usedInfo}" >
			<form class="light" id="routeForm">
			<table class="ui"  border="1">
				<tr style="display:none"><td><input name="lost" value="1"/></td></tr>
				<tr><td>起点:</td><td ><c:out value="${info.cpInfStart }" /></td></tr>
				<tr><td>终点:</td><td ><c:out value="${info.cpInfEnd }" /></td></tr>
				<tr><td>出发时间:</td><td><c:out value="${info.cpInfDepartTime }" /></td></tr>
				<tr><td style="display:none"><input name="routeID" value="${info.cpInfId}" /><br><br><br><br><br><br><br><br></tr>
			</table>
			</form>
			</c:forEach>
	</div>	
</div>
  <p>&nbsp;</p>
</body>
</html>

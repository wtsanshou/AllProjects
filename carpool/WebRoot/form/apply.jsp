<%@ page language="java" contentType="text/html; charset=GBK"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
    <title>线路详情</title>
    
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
     		$("#start").val($(".start").text());
     		$("#end").val($(".end").text());
     		$("#ss").mouseover(function(){
     			route();
     		});
     		$("#exit").click(function(){
     			with(document.getElementById("applyForm")){
     				method = "post";
					action = "exit.do";
					submit();
     			}
     		});
     			if(($("#img1").attr("src")) == ""){
	     			$("#img1").css("display","none");
     			}
     			if(($("#img2").attr("src")) == ""){
	     			$("#img2").css("display","none");
     			}
     			if(($("#img3").attr("src")) == ""){
	     			$("#img3").css("display","none");
     			}
     		$("#delete").mousedown(function(){
			$("#delete").css("border-top" ,"1px solid #000000")
						.css("border-left" ,"1px solid #000000")
						.css("border-bottom" ,"1px solid #EEEEEE")
						.css("border-right" ,"1px solid #EEEEEE")
						;
			});
			$("#delete").mouseup(function(){
				$("#delete").css("border-top" ,"1px solid #EEEEEE")
					.css("border-left" ,"1px solid #EEEEEE")
		
					.css("border-bottom" ,"1px solid #000000")
					.css("border-right" ,"1px solid #000000");
			});
     		var applier = $("#user1").attr("value");
     		var publisher = $("#user2").attr("value");
     		if(applier == publisher){
     			$("#test").css("display","none");
     			$("#showDel").css("display","block");
     		}else {
     			$("#test").css("display","block");
     			$("#showDel").css("display","none");
     		}
     		var pub = $("#pub");
     		var app1 = $("#app1");
     		var app2 = $("#app2");
     		var app3 = $("#app3");
     		$("#a1").hover(function(){
     			app1.css("display","block");
     			app2.css("display","none");
     			app3.css("display","none");
     			pub.css("display","none");
     		},function(){
     			app1.css("display","none");
     		});
     		$("#a2").hover(function(){
     			app2.css("display","block");
     			app1.css("display","none");
     			app3.css("display","none");
     			pub.css("display","none");
     		},function(){
     			app2.css("display","none");
     		});
     		$("#a3").hover(function(){
     			app3.css("display","block");
     			app1.css("display","none");
     			app2.css("display","none");
     			pub.css("display","none");
     		},function(){
     			app3.css("display","none");
     		});
     		$("#p1").hover(function(){
     			pub.css("display","block");
     			app1.css("display","none");
     			app2.css("display","none");
     			app3.css("display","none");
     		},function(){
     			pub.css("display","none");
     		});
     		
     		var lost = $("#lost").attr("value");
     		if(lost == "lost"){
     			$("#test").css("display","none");
     			$("#showDel").css("display","none");
     		}
     	});
     	
     	function apply(){
     		with(document.getElementById("applyForm")){
				method = "post";
				action = "apply.do";
				submit();
			}	
     	}
     	function del(){
     		with(document.getElementById("applyForm")){
				method = "post";
				action = "delete.do";
				submit();
			}
     	}
     </script>
     
     <style type="text/css">
	.light table td {
		width: 50%;
		font-size:13px;
		align: center;
	}
	#delete {
	font-family: Arial;
	font-size: .8em;
	text-align:center;
	margin:3px;
	cursor:hand;
	color: #FFFFFF;
	padding:4px 10px 4px 10px;
	background-color: #0A5CA7;
	text-decoration: none;
	border-top: 1px solid #EEEEEE;		
	border-left: 1px solid #EEEEEE;
	border-bottom: 1px solid #000000;
	border-right: 1px solid #000000;
	} 
	.detail {
		display:none;
		position:absolute;	
	}
	.det {
		background-color:#FFFFDD;
		border-color: #000000;
	}	
	.det td {
		border: 0px;
	}
</style> 
     
  </head>
  
  <body onLoad="initialize()" id="ss">
 <div> 
	<div style="float:left;margin:0px;width:10%">
		<a id="logo"  style="cursor:hand" ><img src="../images/logo.jpg" /></a>
	</div>
	
	<div>
	  <table width="86%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	    
	      <td width="46%">&nbsp;</td>
	     
	      <td width="20%" style="color:white;font-size:15px">${user}欢迎您！ |</td>
	     
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/postInfo.jsp">信息发布 |</a></td>
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/success.jsp">返回主页 |</a></td>
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/personal.jsp">个人主页 |</a></td>
	      <td width="5%" style=";font-size:15px;color:white"><a href="#" id="exit">退出</a></td>
	    </tr>
	
	  </table>
	</div>
<div style="padding-top:0px;padding-left:50px" >
	<center>
		<h1>线路详情</h1>
	</center>
	<table >
	<tr style="display:none"><td><input id="user1" value="${user}"/><input id="user2" value="${publisher.userName}"/></td></tr>
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
  <div style="float:left;background-color:#FFFFFF;height:600px;width:25%">
  	    
<form name="applyForm" class="light" id="applyForm" >
	<table  width="100%" > 
  	<tr>
      <th scope="col" colspan="3"><h2>详细信息</h2></th>
      
    </tr>
    <tr><td colspan="2"><hr></td></tr>
    <tr>
    	<td >起点:</td>
      <td class="start" >${result.cpInfStart}</td>
    </tr>
    <tr>
      <td>终点:</td>
      <td class="end">${result.cpInfEnd}</td>
    </tr>
    <tr>
      <td>出发时间:</td>
      <td>${result.cpInfDepartTime}</td>
    </tr>
    <tr>
      <td>人数限制:</td>
      <td>${result.cpInfPeopNumb}</td>
    </tr>
    <tr >
      <td>已报名人数:</td>
      <td>${result.cpInfAvilableNum}</td>
    </tr>
    
    <tr>
      <td>性别要求:</td>
      <td id="sex">${result.cpInfSex }</td>
    </tr>
    <tr>
	<td colspan="2">
		<table width="100%">
			<tr>
				<td colspan="3" align="center" ><hr>报名人<hr></td>
			</tr>
			<tr>
				<td><img id="img1" style="width:40px" src="${applicant1.userProfile}"/></td><td><img style="width:40px" id="img2" src="${applicant2.userProfile}"/></td><td><img style="width:40px" id="img3" src="${applicant3.userProfile}"/></td><td width="30%"></td>
				
			</tr>
			<tr>
				<td><a href="#" id="a1" >${applicant1.userNickname}</a><input style="display:none" name="applicant1" value="${applicant1.userName}"/></td><td><a href="#" id="a2" onClick="look()">${applicant2.userNickname}</a><input style="display:none" name="applicant2" value="${applicant2.userName}"/></td><td><a href="#" id="a3" onClick="look()">${applicant3.userNickname}</a><input style="display:none" name="applicant3" value="${applicant3.userName}"/></td><td width="30%"></td>
			</tr>
			<tr>
				<td colspan="3"><hr></td>
			</tr>
		</table>
	</td>    
      
    </tr>
    <tr>
      <td rowspan="2">信息发布人:</td><td><img style="width:40px" src="${publisher.userProfile}"/></td>
    </tr>
    <tr>  
      <td><a href="#" id="p1" onClick="look()">${publisher.userNickname}</a><input style="display:none" name="publisher" value="${publisher.userName}"/></td>
    	
    </tr>
   	<tr>
   		<td colspan="2"><hr></td>
   	</tr>
    <tr>
      <td>发布人电话:</td>
      <td>${publisher.userContact}</td>
    </tr>
    <tr>
    	<td>信息发布时间:</td>
    	<td>${result.cpInfPublishTime}</td>
    </tr>
    <tr>
      <td>拼车周期:</td>
      <td>${result.cpInfFrequency }</td>
    </tr>
    <tr><td colspan="2"><hr></td></tr>
    <tr>
      <td>备注信息:</td>
      <td>${result.cpInfRemark }</td>
    </tr>
    <tr><td colspan="2"><hr></td></tr>
    <tr>
      <td style="display:none"><input id="lost" value="${lost}"/></td>
  			
  				<td align="right">
					<div   style="width:100px">
					<p id="test" onClick="apply()" >立即加入</p>
					</div>
					<div id="showDel"  style="width:100px;display:block">
					<p id="delete" onClick="del()" >取消线路</p>
					</div>
				</td>
  	</tr>
    <tr style="display:none">
      <td ><input name="routeID" value="${result.cpInfId}" /></td>
      <td><input name="userID" value="${userinfo.userId}" /></td>
    </tr>
    <tr>
    	<td colspan="3">
    		
    	</td>
    </tr>
   </table>  	
  </form>   	
</div>
  
   <div  style="background-color:#EEEEEE;height:44px;width:100%"></div>
	<div id="map" style="width: 100%; height: 600px;" ></div>    
	    
	<div id="directionsPanel"  style="position:absolute; right:0px; top:87px; width:200px;background-color:#CEEDFF">
		
	</div>
	<div style="position:absolute; right:10px; top:105px;">
		<a href="#" ><img title="隐藏面板" style="display:block;border:0px" id="hidden" src="../images/mark1.jpg"  /></a>		
	</div>
	<div style="position:absolute; right:10px; top:105px;">
		
		<a href="#" ><img title="显示面板" style="display:none;border:0px" id="show" src="../images/mark2.jpg" /></a>
	</div>
 </div>
 <div class="detail" id="pub" style="top:480px;left:175px" >
 	<table class="det" border="1">
		<tr><td>等级:</td><td>${publisher.userLevel}</td></tr>
		<tr><td>性别:</td><td>${publisher.userSex}</td></tr>
		<tr><td>拼车成功次数:</td><td>${publisher.userSuccessTimes}</td></tr>
		<tr><td>拼车失败次数:</td><td>${publisher.userBadTimes}</td></tr>
    </table>
 </div >
 <div class="detail" id="app1" style="top:380px;left:53px;">
 	<table class="det" border="1">
		<tr><td>等级:</td><td>${applicant1.userLevel}</td></tr>
		<tr><td>性别:</td><td>${applicant1.userSex}</td></tr>
		<tr><td>拼车成功次数:</td><td>${applicant1.userSuccessTimes}</td></tr>
		<tr><td>拼车失败次数:</td><td>${applicant1.userBadTimes}</td></tr>
    </table>
 </div >
 <div class="detail" id="app2" style="top:380px;left:150px;">
 	<table class="det" border="1">
		<tr><td>等级:</td><td>${applicant2.userLevel}</td></tr>
		<tr><td>性别:</td><td>${applicant2.userSex}</td></tr>
		<tr><td>拼车成功次数:</td><td>${applicant2.userSuccessTimes}</td></tr>
		<tr><td>拼车失败次数:</td><td>${applicant2.userBadTimes}</td></tr>
    </table>
 </div>
 <div class="detail" id="app3" style="top:380px;left:240px">
 	<table class="det" border="1">
		<tr><td>等级:<br></td><td>${applicant3.userLevel}<br></td></tr>
		<tr><td>性别:<br></td><td>${applicant3.userSex}<br></td></tr>
		<tr><td>拼车成功次数:<br></td><td>${applicant3.userSuccessTimes}<br></td></tr>
		<tr><td>拼车失败次数:<br></td><td>${applicant3.userBadTimes}<br></td></tr>
    </table>
 </div>
 <button id="undo" style="display:none;margin:auto" onClick="undo()">Undo          </button>
  </body>
</html>

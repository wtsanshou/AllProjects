<%@ page language="java" import="java.util.*" pageEncoding="GBK"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
		<meta http-equiv="Content-Type" content="text/html; charset=GBK">
		<title>注册</title>
		<link type="text/css" rel="stylesheet" href="../css/main.css" />
		<link type="text/css" rel="stylesheet" href="../css/regist.css" />
		<script type="text/javascript" src="../js/jquery.js"></script>
		<script type="text/javascript" src="../js/regist.js"></script>
		<script type="text/javascript" src="../js/map.js"></script>
		<script type="text/javascript">
			$(function(){
				$("#un").focus();
				$("#test").click(function(){
					var flag = true;
					for(var i = 0; i < 6; i++){
						if($(".right")[i].style.display == "none"){
							alert(i);
							flag = false;
							$(".wrong")[i].css("display","block");
							$(".error")[i].css("display","block");
							black;
						}
					}
					if(flag == true){
						with(document.getElementById("registerForm")){
						method = "post";
						action = "regist.do";
						submit();
						}	
					}	
				});
				$("#exit").click(function(){
	     			with(document.getElementById("registerForm")){
	     				method = "post";
						action = "exit.do";
						submit();
	     			}
     			});
     			$("#UNLlogo").click(function(){
     			$(this).attr("href","../index.jsp");
     		});
			});
		function login(){
			with(document.getElementById("registerForm")){
				method = "post";
				action = "login.do";
				submit();
			}
		}	
</script>



<style type="text/css">
	
</style>

</head>
  
  <body>
<form name="registerForm" id="registerForm" >
<div>	
	 <div style="float:left;margin:0px;width:10%">
		<a id="UNLlogo"  style="cursor:hand" ><img src="../images/logo.jpg" /></a>
	</div>
	
	<div >
	  <table width="86%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	    
	      <td width="42%">&nbsp;</td>
	      <td width="10%" style="color:white;font-size:15px;" align="right">用户名:</td>
	      <td width="10%"><input name="username" type="text" size="15"></td>
	      <td width="8%" style="color:white;font-size:15px" align="right">密码:</td>
	      <td width="10%"><input name="password" type="password" size="15"></td>
	      <td width="8%" style=";font-size:15px"><a href="#"   onclick="login()">登录 |</a></td>
	      <td width="6%" style=";font-size:15px;color:white"><a href="#" id="exit">退出</a></td>
	    </tr>
	  </table>
	</div>
	<dir >
		<center>
			<h1>注册用户</h1>
		</center>
	</dir>
</div>	
	<div id="table"  style="width:100%;" align="center" >
		<table id="regist" border="2" cellspacing="10">
			
			<tr><td align="right" width="20%">用户名:</td><td width="35%"><input type="text" name="un" id="un"/></td><td width="45%"><img id="nur" class="right" src="../images/right.gif" /><img id="nuw" class="wrong" src="../images/wrong.gif" /><input id="nue" class="error" type="text" style="font-size:20px"/></td>
			
			</tr>
			<tr><td align="right">真实姓名:</td><td><input type="text" name="nickname" id="nn"/></td><td><img class="right" id="nnr" src="../images/right.gif" /><img id="nnw" class="wrong" src="../images/wrong.gif" /><input id="nne" class="error" type="text" style="font-size:20px"/></td>
			
			</tr>
			<tr><td align="right">密码:</td><td><input type="password" name="pw" id="pw"/></td><td><img class="right" id="pwr" src="../images/right.gif" /><img id="pww" class="wrong" src="../images/wrong.gif" /><input id="pwe" class="error" type="text" style="font-size:20px"/></td>
			
			</tr>
			<tr><td align="right">确认密码:</td><td><input type="password" name="repassword" id="rpw"/></td><td><img class="right" id="rpwr" src="../images/right.gif" /><img id="rpww" class="wrong" src="../images/wrong.gif" /><input id="rpwe" class="error" type="text" style="font-size:20px"/></td>
			
			</tr>
			<tr><td align="right">性别:</td>
			        <td><input type="radio" name="sex" value="男" checked/>
			        男
			      <input type="radio" name="sex" value="女" />
			    	女</td><td></td>
    		</tr>
			<tr><td align="right">Email:</td><td><input type="text" name="email" id="em"/></td><td><img class="right" id="emr" src="../images/right.gif" /><img id="emw" class="wrong" src="../images/wrong.gif" /><input id="eme" class="error" type="text" style="font-size:20px"/></td>
			
			</tr>
			<tr><td align="right">手机号码:</td><td><input type="text" name="phone" id="ph"/></td><td><img class="right" id="phr" src="../images/right.gif" /><img id="phw" class="wrong" src="../images/wrong.gif" /><input id="phe" class="error" type="text" style="font-size:20px"/></td>
			
			</tr>		
				<tr>
				<td></td>
				<td>
					<div style="width:200px" align="center">
					<p id="test" >立即注册</p>
					</div>
				</td>
				<td>	
				 </td>
			</tr>
			
		</table>
	</div>
</form>	
</body>
</html>

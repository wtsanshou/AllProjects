<%@ page language="java" contentType="text/html; charset=GBK"%>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
  		<meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
		<meta http-equiv="Content-Type" content="text/html; charset=GBK">
		<title>�޸���Ϣ</title>
		<link type="text/css" rel="stylesheet" href="../css/main.css" />
		
		<script type="text/javascript" src="../js/jquery.js"></script>
		
		<script type="text/javascript" src="../js/map.js"></script>
		<script type="text/javascript">
			
			$(function(){
				$("#changeface").click(function(){
					$("#face").css("display","block");
				});
				$("#face img").click(function(){
					var newface = $(this).attr("src");
					$("#userProfile").attr("value",newface);
					$("#mainface").attr("src",newface);
				});
				
				var pw = ${userinfo.userPwd};
				$("#test").click(function(){
					var opw = $("#opw").val();
					var npw = $("#npw").val();
					var rnpw = $("#rnpw").val();
					if(npw != "" || rnpw != ""){
						if(npw != rnpw){
							alert("�������������벻һ�£�");
						}else if(rnpw.length < 6 || rnpw == ""){
									alert("�����벻��С��6λ��");
						}
					}else if(pw != opw){
							alert("�����벻��ȷ��");
					}else {
						with(document.getElementById("changeForm")){
							method = "post";
							action = "change.do";
							submit();
						}	
					}
				});
				
				$("#exit").click(function(){
	     			with(document.getElementById("changeForm")){
	     				method = "post";
						action = "exit.do";
						submit();
     				}
     			});
			});
			
		</script>



<style type="text/css">
#regist {
		line-height:40px;
		border-color:#0A5CA7;
		width: 70%;
	}
	#regist td {
		align: right;
		border: 5px;
		font-size:20px;
	}
	#regist input {
		font-size:20px;
	}
	
	#face img {
		cursor: hand;
		width: 50px;
	}
	#changeface {
		font-size: 20px;
	}

</style>

  </head>
  
  <body>
<form name="changeForm" id="changeForm" >
<div>	
	 <div style="float:left;margin:0px;width:10%">
		<a id="logo" style="cursor: hand;"><img  src="../images/logo.jpg" /></a>
	</div>
	
	<div >
	  <table width="87%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	      <td width="40%">&nbsp;</td>
	     <td width="16%" style="color:white;font-size:15px">${user}��ӭ���� |</td>	     
	      <td width="10%" style=";font-size:15px"><a href="../form/postInfo.jsp">��Ϣ���� |</a></td>
	      <td width="10%" style=";font-size:15px"><a href="../form/success.jsp">������ҳ |</a></td>
	      <td width="10%" style=";font-size:15px"><a href="../form/personal.jsp">������ҳ |</a></td>
	      <td width="5%" style=";font-size:15px;color:white"><a href="#" id="exit">�˳�</a></td>
	    </tr>
	  </table>
	</div>
	<dir >
		<center>
			<h1>�޸���Ϣ</h1>
		</center>
	</dir>
</div>	
	<div id="table"  style="width:1000px;" align="center" >
		<div>
		<table id="regist" border="2" cellspacing="10">
			<tr><td style="display:none"><input   name="username" value=${user} /></td>
			<td align="right" width="20%"><input style="display:none" id="userProfile" name="profile" value=${userinfo.userProfile} /></td><td width="35%"  align="center"><img id="mainface" style="width:60px" src="${userinfo.userProfile}"/><a id="changeface"  href="#">����ͷ��</a></td>
																		<td rowspan="8">
																			<table id="face" style="display:none">
																				<tr>
																					<td><img src="../images/w/hn01.gif"/></td><td><img src="../images/w/hn02.gif"/></td><td><img src="../images/w/hn03.gif"/></td><td><img src="../images/w/hn04.gif"/></td><td><img src="../images/w/hn06.gif"/></td>
																				</tr>
																				<tr>
																					<td><img src="../images/w/hn09.gif"/></td><td><img src="../images/w/hn10.gif"/></td><td><img src="../images/w/hn12.gif"/></td><td><img src="../images/w/hn19.gif"/></td><td><img src="../images/w/hn21.gif"/></td>
																				</tr>
																				<tr>
																					<td><img src="../images/m/hn05.gif"/></td><td><img src="../images/m/hn07.gif"/></td><td><img src="../images/m/hn08.gif"/></td><td><img src="../images/m/hn11.gif"/></td><td><img src="../images/m/hn13.gif"/></td>
																				</tr>
																				<tr>
																					<td><img src="../images/m/hn14.gif"/></td><td><img src="../images/m/hn15.gif"/></td><td><img src="../images/m/hn16.gif"/></td><td><img src="../images/m/hn17.gif"/></td><td><img src="../images/m/hn18.gif"/></td>
																				</tr>
																			</table>
																		</td>
			</tr>
			
			<tr><td align="right" >�ǳ�:</td><td ><input type="text" name="nickname" id="nn"/></td>
			
			</tr>
			<tr><td align="right">������:</td><td><input type="password" name="oldpassword" id="opw"/></td>
			
			</tr>
			<tr><td align="right">������:</td><td><input type="password" name="password" id="npw"/></td>
			
			</tr>
			<tr><td align="right">ȷ������:</td><td><input type="password" name="repassword" id="rnpw"/></td>
			
			</tr>
			<tr><td align="right">�Ա�:</td>
			        <td><input type="radio" name="sex" value="��" checked/>
			        ��
			      <input type="radio" name="sex" value="Ů" />
			    	Ů</td>
    		</tr>
			<tr><td align="right">Email:</td><td><input type="text" name="email" id="em"/></td>
			
			</tr>
			<tr><td align="right">�ֻ�����:</td><td><input type="text" name="phone" id="ph"/></td>
			
			</tr>
			<tr>
				<td align="right" >����ǩ��:</td><td ><input type="text" name="introduction" /></td>
			</tr>
			
			<tr>
				<td></td>
				<td>
					<div style="width:200px" align="center">
					<p id="test" >�ύ�޸�</p>
					</div>
				</td>
				<td>
					
				 </td>
			</tr>
			
		</table>
		</div>
		
	</div>
	
	
</form>	
</body>
</html>

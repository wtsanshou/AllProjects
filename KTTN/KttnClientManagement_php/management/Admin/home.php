<?php

	/*
	 * Created on 2013-1-5
	 *
	 * the home page of Admin
	 * 
	 */
	/*start a session*/
	session_start();
	
	/*quote the DAO of user*/
	require ("../DAO/userDB.php");
	
	$a = new User();
	
	/*check if the user is admin user in the database*/
	$u = $_POST['username'];
	$p = $_POST['password'];
	//echo $u.$p;
	
	?>
		
<html>
<head>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8">


<title>Admin Home page</title>

<link rel="stylesheet" type="text/css" href="../../css/main.css" />

<script type="text/javascript" src="../../js/jquery.js"></script>



<!--ajax-->
<script type="text/javascript" src="../../js/adminAjax.js"></script>
<script type="text/javascript" src="../../js/admin.js"></script>



</head>
<body>
	<center>
	
<!--logo-->
	<div id="top" >
			<img id="logo" src="../../img/KttnLogo.png" />
		</div>
		
	
		
	<?php
		if (!$a->matchAdmin($u, $p)) {
	?>
	
<!--error message-->
		<div id="errorMes">
			<table>
				<tr>
					<td><img src="../../img/attention.png"/></td>
					
				</tr>
				<tr>
					<td>用户名或密码错误</td>
					
				</tr>
				<tr>
					<td><a href="index.html">返回</a></td>
					
				</tr>
			</table>

		</div>
	<?php
		} 
		else {
			$_SESSION['username']= $u;
			$_SESSION['usertype']= 0;
	?>
	
<!--menu-->
	
		<div id="tabs" >
			<ul>
            	<li id="admPage"><a href="#" >管理员帐号</a></li>
                <li id="manaPage"><a href="#" >经理帐号</a></li>
                <li id="staffPage"><a href="#" >员工帐号</a></li>
                <li id="addPage"><a href="#" >添加用户</a></li>
           		<li id="logPage"><a href="#" >修改记录</a></li>
           		<li id="exitPage"><a href="index.html" >退出</a></li>
			</ul>
	    </div>
	    
<!--ajax context-->
		<div id="mainInfo">
		<!--管理员帐号-->
			<div id="admin">
				
			</div>
			
		<!--经理帐号-->
			<div id="manager">
				
			</div>
			
		<!--员工帐号-->
			<div id ="staff">
				
			</div>
			
		<!--修改用户-->
			<div id ="modUser">
				
			</div>
			
		<!--添加用户-->
			<div id ="addUser">
				
			</div>
			
		<!--修改记录-->
			<div id ="modLog">
				
			</div>
			

		</div>
	<?php
		}
	?>
	
	</center>
</body>
</html>

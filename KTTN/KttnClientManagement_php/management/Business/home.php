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
<script type="text/javascript" src="../../js/busiAjax.js"></script>
<script type="text/javascript" src="../../js/business.js"></script>



</head>
<body>
	<center>
	
<!--logo-->
	<div id="top" >
			<img id="logo" src="../../img/KttnLogo.png" />
		</div>
		
	
		
	<?php
		$ut = $a->matchUser($u, $p);
		if ($ut !=1 && $ut != 2) {
			
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
					<td><a href="../../index.html">返回</a></td>
					
				</tr>
			</table>

		</div>
	<?php
		} 
		else {
			$_SESSION['username']= $u;
			$_SESSION['usertype']= $ut;
			
	?>
	
<!--menu-->
	
		<div id="listTabs" >
			<ul>
            	<li class="listMenu">
            		<a href="#" >客      户</a>
            		<ul>
            			<li class="highlight"><a href="#" id="viewClient">查看</a></li>
            			<li class="highlight"><a href="#" id="addCliPage">添加</a></li>
            			<li class="highlight"><a href="#" >统计</a></li>
            		</ul>
            	</li>
                <li class="listMenu">
                	<a href="#" >专      家</a>
                	<ul>
            			<li class="highlight"><a href="#" >查看</a></li>
            			<li class="highlight"><a href="#" >添加</a></li>
            			<li class="highlight"><a href="#" >统计</a></li>
            		</ul>
        		</li>
                <li class="listMenu">
                	<a href="#" >会员卡</a>
            		<ul>
            			<li class="highlight"><a href="#" >查看</a></li>
            			<li class="highlight"><a href="#" >统计</a></li>
            		</ul>
        		</li>
                <li class="listMenu">
                	<a href="#" >活      动</a>
                	<ul>
            			<li class="highlight"><a href="#" >查看</a></li>
            			<li class="highlight"><a href="#" >添加</a></li>
            		</ul>
        		</li>
           		<li class="listMenu">
           			<a href="#" >服    务</a>
           			<ul>
            			<li class="highlight"><a href="#" >查看</a></li>
            			<li class="highlight"><a href="#" >添加</a></li>
            		</ul>
        		</li>
           		<li class="listMenu"><a href="../../index.html" >退     出</a></li>
			</ul>
	    </div>
	    
<!--ajax context-->
		<div id="busiInfo">
		<!--客户-->
			<!--客户信息-->
				<div id="clientInfo">
					test
				</div>
			<!--客户详细信息-->
				<div id="clientDeta">
					test
				</div>
			<!--修改客户信息-->
				<div id="clientMod">
					
				</div>
			<!--客户开卡或续卡-->
				<div id="cliRegMem">
					
				</div>
			<!--客户添加-->
				<div id="clientAdd">
					
				</div>
			<!--客户统计-->
				<div id="clientSta">
					
				</div>
				
		<!--专家-->	
			<!--专家信息-->
				<div id ="expert">
					
				</div>
			<!--修改专家信息-->
				<div id="expertMod">
					
				</div>	
			<!--专家添加-->
				<div id ="expertAdd">
					
				</div>
			<!--专家统计-->
				<div id="expertSta">
					
				</div>
			
		<!--会员卡-->	
			<!--会员卡信息-->
				<div id ="memInfo">
					
				</div>
			<!--会员卡业务-->
				<div id ="memServ">
					
				</div>	
			<!--专家统计-->
				<div id="memSta">
					
				</div>
		<!--活动-->	
			<!--活动信息-->
				<div id ="actInfo">
					
				</div>
			<!--活动修改-->
				<div id ="actMod">
					
				</div>	
			<!--活动添加-->
				<div id="actAdd">
					
				</div>
		<!--服务-->	
			<!--服务信息-->
				<div id ="servInfo">
					
				</div>
			<!--服务修改-->
				<div id ="servMod">
					
				</div>	
			<!--服务添加-->
				<div id="servAdd">
					
				</div>
		</div>
	<?php
		}
	?>
	
	</center>
</body>
</html>

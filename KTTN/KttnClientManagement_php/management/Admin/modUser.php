<?php
/*
 * Created on 2013-1-6
 *
 * modify a user into the database
 */
 
 /*start a session*/
	session_start();
 
	$user_name = $_POST['user_name'];
	
	$password = $_POST['password'];
	$userTypeStr = $_POST['user_type'];
	$user_type = 2;
	if($userTypeStr == "管理员")
	{
		$user_type = 0;
	}
	else if($userTypeStr == "经理")
	{
		$user_type = 1;
	}
	
	$remark = $_POST['remark'];
	
	
	$mod_name = $_SESSION['username'];
	$mod_time = date('Y-m-d G:i:s');
	
	/*quote the DAO of user*/
	require ("../DAO/userDB.php");
	
	$ud = new User();
	
	$mu = $ud->modUser($user_name,$password,$user_type,$remark,$mod_name,$mod_time);
	
	if($mu)
	{
		echo '修改成功';
	}
	else
	{
		echo '修改失败';
	}
?>

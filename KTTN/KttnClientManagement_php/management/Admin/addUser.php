<?php
/*
 * Created on 2013-1-6
 *
 * add a user into the database
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
	$ope_name = $_SESSION['username'];
	
	$ope_time = date('Y-m-d G:i:s');
	
	$mod_name = $_SESSION['username'];
	$mod_time = date('Y-m-d G:i:s');
	
	/*quote the DAO of user*/
	require ("../DAO/userDB.php");
	
	$ud = new User();
	
	$r = $ud->addUser($user_name,$password,$user_type,$remark,$ope_name,$ope_time,$mod_name,$mod_time);
	//echo $r;
	if($r)
	{
		echo '添加成功';
	}
	else
	{
		echo '添加失败';
	}
?>

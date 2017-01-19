<?php
/*
 * Created on 2013-1-6
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
  /*start a session*/
	session_start();
 
 require("../DAO/userDB.php");
	
	$user_name = $_POST['user_name'];
	
	$ope_name = $_SESSION['username'];
	
	$user = new User();
	
	$result = $user->delUser($user_name,$ope_name);
	
	if($result)
	{
		echo '删除用户： '.$user_name;
	}
	else
	{
		echo '删除用户失败 ';
	}
 
?>

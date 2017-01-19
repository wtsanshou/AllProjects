<?php
/*
 * Created on 2013-1-2
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
	require("../management/DAO/userDB.php");
	 
	$a = new User();
	
	/*
	 * test function getAllUsers()
	 * */
	/*$arr = $a->getAllUsers();
	//$arr = $a->getUserArray();
	 echo count($arr);
	 //echo $a->getUserName();
	 foreach($arr[1] as $key =>$value)
	{
		echo '|'.$key.' : '.$value.'<br />';
	}
	
	echo $arr[1][2];*/
	
	/*
	 * test function getAdmin()
	 * */
	 /*$arr = $a->getAdmin();
	 $c = sizeof($arr);
	 echo $c;
	 for($i = 0; $i<$c; $i++)
	 {
	 	foreach($arr[$i] as $key =>$value)
		{
			echo '|'.$key.' : '.$value.'<br />';
		}
	 }*/
	 
	 /*
	 * test function matchAdmin()
	 * */
	 $u = $_POST['username'];
	 $p = $_POST['password'];
	 //echo $u.$p;
	 if($a->matchAdmin($u,$p))
	 {
	 	echo 'success';
	 }
	 else
	 {
	 	echo 'shit';
	 }
	 
?>

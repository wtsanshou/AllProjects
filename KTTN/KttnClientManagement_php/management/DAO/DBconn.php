<?php

	/* quote the exception defined in connon fold*/
	//require "../../common/exception.php";

	/*connect the Database*/

	define('DB_USER',		 "root");
	define('DB_PASSWORD',	 "admin");
	define('BD_NAME' ,		 "kttn");
	define('BD_HOST' ,		 "localhost"); 	

	try
	{
		$db = mysql_connect(BD_HOST,DB_USER, DB_PASSWORD) ;
		if($db == false)
		{
			echo  "DB connected error. please check connection String.";
			exit();
		}
		mysql_select_db(BD_NAME);	
	}
	catch(Exception $e)
	{
		$e->errorMessage();
	}
	
	
?>

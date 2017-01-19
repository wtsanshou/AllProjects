
<?php
////////////////////////////////////////////////////
// User - Database table User
//
// Class for operate table User in database kttn.
// The Database use mySql;
// Variables: int id, String user_name, String password, int user_type, String remark,
// 			  String ope_name, Date ope_time, String mod_name, Date mod_time;
// The Methods include: getAllUsers(), getAdmin(), getManager(), getStaff(),
//                      matchAdmin($user_name,$password),
//                      matchManager($user_name,$password),
//                      matchStaff($user_name,$password),
//    					addAdmin($user_name,$password,0,$remark,$op_name,$ope_time,$mod_name,$mod_time),
//                      addManager($user_name,$password,1,$remark,$op_name,$ope_time,$mod_name,$mod_time),
//                      addUser($user_name,$password,1,$remark,$op_name,$ope_time,$mod_name,$mod_time), 
//                      
//                      modUser($conlum_name, $value,$mod_name,$mod_time),
//						delAdmin($user_name), delManager($user_name),delUser($user_name),
//						logSQL($sql,$query_time,$user_IP,$ope_name,$remark);
////////////////////////////////////////////////////

/**
 * User - User operate class
 * @package management/Admin
 * @author Tao Wei
 * @copyright 2012 - 2013 Kttn
 */


 	
 	class User
 	{
 		/////////////////////////////////////////////////
	    // PUBLIC VARIABLES
	    /////////////////////////////////////////////////
	
	    
	     
	    
	     
	   
	     
	     /**
	     * the type of user(0:admin, 1:manager, 2:nomal user)
	     * @var Int
	     */
	     var $user_type = 2;
	     
	     /**
	     * Remark
	     * @var String
	     */
	     var $remark = "";
	     
	     /**
	     * the name of operater who use this account
	     * @var String
	     */
	     var $ope_name = "";
	     
	     /**
	     * the time of operating this account
	     * @var Date
	     */
	     var $ope_time;
	     
	     /**
	     * the name of modifier who modify this account
	     * @var String
	     */
	     var $mod_name = "";
	     
	     /**
	     * the time of modifying this account
	     * @var Date
	     */
	     var $mod_time;
	     
	     /**
	     * a array used to save all information of users
	     * @var Date
	     */
	     var $user_array;
	     
	     
 		/**
	     * Returns all user in the Database user.
	     * @access public
	     * @return array[][]
	     */
		function getAllUsers()
		{
			/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$SQL = 'select * from user';
	
			mysql_query("SET NAMES 'utf8'"); 
			$result = mysql_query($SQL);
			
			//$ua[0] = mysql_fetch_array($result,MYSQL_ASSOC);
			try
			{
				$i = 0;
				while($myrow = mysql_fetch_array($result))
				{
					$user_list[$i] = $myrow;
					/*foreach($myrow2 as $key =>$value)
					{
						echo '|'.$key.' : '.$value.'<br />';
					}*/
					$i++;
				}
			}
			catch(Exception $e)
			{
				$e->errorMessage();
			}
			
			return $user_list;
		}
		
		/**
	     * Returns admin user in the Database user.
	     * @access public
	     * @return array[][]
	     */
		function getAdmin()
		{
			/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$SQL = 'select * from user where UserType = 0';
	
			mysql_query("SET NAMES 'utf8'"); 
			$result = mysql_query($SQL);
			$admin_list = null;
			//$ua[0] = mysql_fetch_array($result,MYSQL_ASSOC);
			$i = 0;
			while($myrow = mysql_fetch_array($result))
			{
				$admin_list[$i] = $myrow;
				/*foreach($myrow as $key =>$value)
				{
					echo '|'.$key.' : '.$value.'<br />';
				}*/
				$i++;
			}
			return $admin_list;
		}
		
		/**
	     * Returns management user in the Database user.
	     * @access public
	     * @return array[][]
	     */
		function getManager()
		{
			/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$SQL = 'select * from user where UserType = 1';
			
			mysql_query("SET NAMES 'utf8'"); 
			$result = mysql_query($SQL);
			$manage_list = null;
			//$ua[0] = mysql_fetch_array($result,MYSQL_ASSOC);
			$i = 0;
			while($myrow = mysql_fetch_array($result))
			{
				$manage_list[$i] = $myrow;
				
				$i++;
			}
			return $manage_list;
		}
		
		/**
	     * Returns staff user in the Database user.
	     * @access public
	     * @return array[][]
	     */
		function getStaff()
		{
			/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$SQL = 'select * from user where UserType = 2';
	
			mysql_query("SET NAMES 'utf8'"); 
			$result = mysql_query($SQL);
			$staff_list = null;
			//$ua[0] = mysql_fetch_array($result,MYSQL_ASSOC);
			$i = 0;
			while($myrow = mysql_fetch_array($result))
			{
				$staff_list[$i] = $myrow;
				
				$i++;
			}
			return $staff_list;
		}
		
		/**
	     * Returns log in the Database sqllog.
	     * @access public
	     * @return array[][]
	     */
		public function getLog()
		{
			/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$SQL = 'select * from sqllog ';
	
			mysql_query("SET NAMES 'utf8'"); 
			$result = mysql_query($SQL);
			$log_list = null;
			//$ua[0] = mysql_fetch_array($result,MYSQL_ASSOC);
			$i = 0;
			while($myrow = mysql_fetch_array($result))
			{
				$log_list[$i] = $myrow;
				
				$i++;
			}
			
			return $log_list;
		}
		
		/**
	     * Judge if the username and password match the admin user in database.
	     * @access public
	     * @return boolean
	     */
	     public function matchAdmin($user_name, $password)
	     {
	     	/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$match = false;
			
			$SQL = "select * from user where usertype = 0 and username = \"".$user_name.
					"\" and password = \"".$password."\"";
	
			mysql_query("SET NAMES 'utf8'"); 		
			$result = mysql_query($SQL);
			
			//echo mysql_num_rows($result);
			if(mysql_num_rows($result)>0)
			{
				$match = true;
			}
			
			return $match;
	     }
	     
	     /**
	     * Judge if the username and password match the management or staff user in database.
	     * @access public
	     * @return boolean
	     */
	     public function matchUser($user_name, $password)
	     {
	     	/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$user_type = -1;
			
			$SQL = "select * from user where username = \"".$user_name.
					"\" and password = \"".$password."\"";
			
			mysql_query("SET NAMES 'utf8'"); 	
			$result = mysql_query($SQL);
			
			//echo mysql_num_rows($result);
			if(mysql_num_rows($result)>0)
			{
				$myrow = mysql_fetch_array($result);
				$user_type = $myrow["UserType"];
			}
			
			return $user_type;
	     }
	     
	     /**
	     * Judge if the username and password match the management user in database.
	     * @access public
	     * @return boolean
	     */
	     public function matchManager($user_name, $password)
	     {
	     	/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$match = false;
			
			$SQL = "select * from user where usertype = 1 and username = \"".$user_name.
					"\" and password = \"".$password."\"";
			
			mysql_query("SET NAMES 'utf8'"); 	
			$result = mysql_query($SQL);
			
			echo mysql_num_rows($result);
			if(mysql_num_rows($result)>0)
			{
				$match = true;
			}
			
			return $match;
	     }
	     
	     /**
	     * Judge if the username and password match the management user in database.
	     * @access public
	     * @return boolean
	     */
	     public function matchStaff($user_name, $password)
	     {
	     	/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$match = false;
			
			$SQL = "select * from user where usertype = 2 and username = \"".$user_name.
					"\" and password = \"".$password."\"";
		
			mysql_query("SET NAMES 'utf8'"); 	
			$result = mysql_query($SQL);
			
			echo mysql_num_rows($result);
			if(mysql_num_rows($result)>0)
			{
				$match = true;
			}
			
			return $match;
	     }
		
		/**
	     * Add a user into database.
	     * @access public
	     * @return boolean
	     */
		 public function addUser($user_name,$password,$user_type,$remark,$op_name,$ope_time,$mod_name,$mod_time)
		 {
		 	/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$sf = false;
			
			if(!$this->findByName($user_name))
			{
				
				$SQL = " insert into user (username,password,usertype,remark,opename,opetime,modname,modtime) values (\"" .
						$user_name."\",\"".$password."\",".$user_type.",\"".$remark."\",\"".
						$op_name."\",\"".$ope_time."\",\"".$mod_name."\",\"".$mod_time."\")";
				
				/*
				 * log the sql
				 * */
				 $user_IP = $_SERVER["REMOTE_ADDR"];
				 $t = "insert table user: ".
				 "(username,password,usertype,remark,opename,opetime,modname,modtime):(".
				 $user_name.",".$password.",".$user_type.",".$remark.",".
						$op_name.",".$ope_time.",".$mod_name.",".$mod_time.")";
				$test = $this->logSQL($t,$ope_time,$user_IP,$op_name,"add");
				
				mysql_query("SET NAMES 'utf8'"); 
				$result = mysql_query($SQL);
				
				if(mysql_affected_rows()>0)
				{
					$sf = true;
				}
			}
			return $sf;
		 }
		 
		 /**
	     * find a user by username .
	     * @access public
	     * @return boolean
	     */
		 public function findByName($user_name)
		 {
			$find = false;
			
			$SQL = "select * from user where username=\"".$user_name."\"";
			
			mysql_query("SET NAMES 'utf8'"); 	
			$result = mysql_query($SQL);
			
			if(mysql_num_rows($result)>0)
			{
				$find = true;
			}
			
			return $find;
		 }
		 
		 /**
	     * modify a user in the database.
	     * @access public
	     * @return boolean
	     */
		 public function modUser($user_name,$password,$usertype,$remark,$mod_name,$mod_time)
		 {
		 	/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$modfy = false;
			
			$SQL = "update user set password = \"".$password."\",usertype = ".$usertype.
			",remark = \"".$remark."\",modname = \"".$mod_name."\", modtime = \"".$mod_time.
			"\" where username = \"".$user_name."\"";
			
			/*
			 * log the sql
			 * */
			 $user_IP = $_SERVER["REMOTE_ADDR"];
			 $t = "update table user: ".
			 "(username,password,usertype,remark,modname,modtime):(".
			 $user_name.",".$password.",".$remark.",".
					$mod_name.",".$mod_time.")";
			 $test = $this->logSQL($t,$mod_time,$user_IP,$mod_name,"modify");
		
		
			mysql_query("SET NAMES 'utf8'"); 
			$result = mysql_query($SQL);
			
			if(mysql_affected_rows()>0)
			{
				$modfy = true;
			}	
				
			
			return $modfy;
		 }
		 
		 
		 /**
	     * delete a user from the database.
	     * @access public
	     * @return boolean
	     */
		 public function delUser($user_name,$ope_name)
		 {
		 	/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$delete = false;
			
			if($this->findByName($user_name))
			{
				
				$SQL = " delete from user where username=\"".$user_name."\"";
				
				/*
				 * log the sql
				 * */
				 $user_IP = $_SERVER["REMOTE_ADDR"];
				 $t = "delete a user in table user: ".
						 "(username):(".
						 $user_name.")";
						 
				$mod_time = date('Y-m-d G:i:s');
				
				$remark = "delete";
				$test = $this->logSQL($t,$mod_time,$user_IP,$ope_name,$remark);
				
				mysql_query("SET NAMES 'utf8'"); 
				$result = mysql_query($SQL);
				
				if(mysql_affected_rows()>0)
				{
					$delete = true;
				}
			}
			
			return $delete;
		 }
		
		/**
	     * Add a log into database when anyone do add, update or delete operation .
	     * @access public
	     * @return boolean
	     */
		public function logSQL($sql,$query_time,$user_IP,$ope_name,$remark)
		{
			
			$insert = false;
			
			$SQL = "INSERT INTO `sqllog`(`Sql`, `QueryTime`, `UserIP`, `OpeName`, `remark`) VALUES (\"".
						$sql."\",\"".$query_time."\",\"".$user_IP."\",\"".
						$ope_name."\",\"".$remark."\")";
				//$SQL = "select * from sqllog";
				
				mysql_query("SET NAMES 'utf8'"); 
				$rr = mysql_query($SQL);
				
				if(mysql_affected_rows()>0)
				{
					$insert = true;
				}
			
			return $insert;
		}

 	}

 
?>

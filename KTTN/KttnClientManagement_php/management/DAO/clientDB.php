<?php
/*
 * Created on 2013-1-9
 *
 */
 
 ////////////////////////////////////////////////////
// Client - Database table client
//
// Class for operate table client in database kttn.
// The Database use mySql;
// Variables: int id, String name, int sex, String born_date, String born_addr,
// 			  	String mob_phone, String telephone, String home_addr, String work_addr,
//				String email, String id_card, String mem_state, String remark,
//				String ope_name, Date ope_time, String mod_name, Date mod_time;
// The Methods include: getAllClients(),
//                      matchClient($name,$id_card),
//                      
//    					addClient(int id, String name, int sex, String born_date, String born_addr,
// 			  					String mob_phone, String telephone, String home_addr, String work_addr,
//								String email, String id_card, String mem_state, String remark,
//								String ope_name, Date ope_time, String mod_name, Date mod_time),
//                      
//                      modClient($conlum_name, $value,$mod_name,$mod_time),
//                      
//						delClient($user_name,$id_card),
//
//						logSQL($sql,$query_time,$user_IP,$ope_name,$remark);
////////////////////////////////////////////////////

/**
 * Client - User operate class
 * @package management/Business
 * @author Tao Wei
 * @copyright 2012 - 2013 Kttn
 */

	class Client
	{
		function getAllClients()
		{
			/* quote the exception defined in connon fold*/
			require("DBconn.php");
			
			$SQL = 'select * from client';
	
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
	}
 
?>

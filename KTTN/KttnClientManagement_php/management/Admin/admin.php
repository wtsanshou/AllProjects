<?php
/*
 * Created on 2013-1-5
 *
 * get admin infomation from database, and put them into a table. 
 * In this table, user could modify, delete the admin user.
 */
 
/*start a session*/
	session_start();
?>
<table  style="width:800px;"  border="1px">
	<tr>
		<td>ID</td>
		<td>用户名</td>
		<td>密码</td>
		<td>创建人</td>
		<td>创建时间</td>
		<td>修改人</td>
		<td>修改时间</td>
		<td colspan="2"><div id="delResult" align="center"></div></td>
	</tr>
<?php
	require("../DAO/userDB.php");
	
	
	$admin = new User();
	
	$result = $admin->getAdmin();
	
	$n = count($result);
	for($i=0; $i<$n;$i++)
	{
		
	
?>

		<tr>
		<!--ID -->
			<td class="id"><?php echo $result[$i][0] ?></td>
		<!--UserName -->
			<td class="un"><?php echo $result[$i][1] ?></td>
		<!--Password -->
			<td><?php echo $result[$i][2] ?></td>
		<!--OpeName -->
			<td><?php echo $result[$i][5] ?></td>
		<!--OpeTime -->
			<td><?php echo $result[$i][6] ?></td>
		<!--ModName -->
			<td><?php echo $result[$i][7] ?></td>
		<!--ModTime -->
			<td><?php echo $result[$i][8] ?></td>
			<td><input type="button" class="modify" value="修改"/></td>
			<td><input type="button" class="delete" value="删除" style="display:<?php 
				/*不可删除本人帐号*/
				if($result[$i][1] == $_SESSION['username'])
				{
					echo 'none';
				}
				else
				{
					echo 'block';
				}
			 ?>"/></td>
			
		</tr>
		
<?php
		
	}
	
?>
</table>
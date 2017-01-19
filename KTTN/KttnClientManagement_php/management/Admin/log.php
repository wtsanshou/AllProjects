<?php
/*
 * Created on 2013-1-5
 *
 * record the actions: add, delete and modify user.
 */
 
/*start a session*/
	session_start();
?>
<table style="width:800px;"  border="1px">
	<tr>
		<td>ID</td>
		<td>SQL操作</td>
		<td>操作时间</td>
		<td>操作人IP</td>
		<td>操作人</td>
		<td>备注</td>
		
	</tr>
<?php
	require("../DAO/userDB.php");
	
	
	$admin = new User();
	
	$result = $admin->getLog();
	
	$n = count($result);
	for($i=$n-1; $i>-1;$i--)
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
			<td><?php echo $result[$i][3] ?></td>
		<!--OpeTime -->
			<td><?php echo $result[$i][4] ?></td>
		<!--ModName -->
			<td><?php echo $result[$i][5] ?></td>
		
			
		</tr>
		
<?php
		
	}
	
?>
</table>
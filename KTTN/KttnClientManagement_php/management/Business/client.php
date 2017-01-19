<?php
/*
 * Created on 2013-1-9
 *
 * get client infomation from database, and put them into a table. 
 * In this table, manager could modify, delete the admin user and regist member card.
 * 					staff could modify the admin user and regist member card.
 */
 
 /*start a session*/
	session_start();
?>
<h1>客户信息</h1>
<table  style="width:800px;"  border="1px">
	<tr>
		<td>ID</td>
		<td>客户名</td>
		<td>性别</td>
		<td>出生日期</td>
		
		<td>手机</td>
		
		<td>会员状态</td>
		
		<td colspan="3"><div id="delResult" align="center"></div></td>
	</tr>
<?php
	require("../DAO/clientDB.php");
	
	
	$client = new Client();
	
	$result = $client->getAllClients();
	
	$n = count($result);
	for($i=0; $i<$n;$i++)
	{
		
	
?>

		<tr>
		<!--ID -->
			<td class="id"><?php echo $result[$i][0] ?></td>
		<!--客户名 -->
			<td class="un"><?php echo $result[$i][1] ?></td>
		<!--性别 -->
			<td>
			<?php if($result[$i][2] ==1)
				{
					echo '男';
				}
				else if($result[$i][2] ==2)
				{
					echo '女';
				}
				else
				{
					echo '';
				}
			?>
			</td>
		<!--出生日期 -->
			<td><?php echo $result[$i][3] ?></td>
		
		<!--手机 -->
			<td><?php echo $result[$i][5] ?></td>
		
		<!--会员状态 -->
			<td><?php echo $result[$i][11] ?></td>
			<td><input type="button" class="detail" value="详细信息"/></td>
			<td><input type="button" class="modify" value="修改"/></td>
			<td><input type="button" class="delete" value="删除" style="display:<?php 
				/*不可删除本人帐号*/
				if($_SESSION['usertype'] == 1)
				{
					echo 'block';
				}
				else
				{
					echo 'none';
				}
			 ?>"/></td>
			
		</tr>
		
<?php
		
	}
	
?>
</table>

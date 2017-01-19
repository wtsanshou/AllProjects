<?php
/*
 * Created on 2013-1-6
 *
 * add user: 0:admin, 1:manager, 3:staff
 */
 
 /*start a session*/
	session_start();
	
	if($_SESSION['usertype']==0)
	{
		$user = $_POST['user_name'];
		?>
		<from>
			<table width="481" height="438" class="UserTable">
			  <tr height="50" style="background-color: #008FD7">
			    <td colspan="2" style="color: #FFFFFF; font-size: 35px;" ><div align="center">用户修改</div></td>
			  </tr>
			  <tr>
			    <td width="135"><div align="right">用户名：</div></td>
			    <td width="332" >
			      <div id="modUsername"><?php echo $user; ?></div>
			        
			        
			       </td>
			  </tr>
			  <tr>
			    <td><div align="right">密码:</div></td>
			    <td>
			    
			      <input type="password" id="modPassword" />
			     
			    </td>
			  </tr>
			  <tr>
			    <td><div align="right">用户类型：</div></td>
			    <td>
			      
			        <select name="select" id="modUsertype">
				          <option>员工</option>
				          <option>经理</option>
				          <option>管理员</option>
				        </select>
			        </td>
			  </tr>
			  <tr>
			    <td><div align="right">备注：</div></td>
			    <td><textarea id="modRemark"cols="30" rows="4"></textarea></td>
			  </tr>
			  <tr>
			    <td>
			    	<div align="center">
			    		<input type="reset" class="button" id="resetMU"  value="清    空" />
		    		</div>
	    		</td>
			    <td>
			     <div align="center">
			      	<input type="submit" class="button" id="mod" value="提   交" />
			     </div>
			    </td>
			  </tr>
			</table>
		<div id="modState"></div>
		</from>
		<?php
	}
 	else
 	{
 		?>
	
<!--error message-->
		<div id="errorMes">
			<table>
				<tr>
					<td><img src="../../img/attention.png"/></td>
					
				</tr>
				<tr>
					<td>您无管理员权限，请重新登录</td>
					
				</tr>
				<tr>
					<td><a href="index.html">返回</a></td>
					
				</tr>
			</table>

		</div>
	<?php
 	}
?>

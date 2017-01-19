<?php
/*
 * Created on 2013-1-6
 *
 * add client
 */
 
 /*start a session*/
	session_start();
	
	if($_SESSION['usertype']==1 ||$_SESSION['usertype']==2)
	{
		
		?>
		<from>
			<table width="481" height="438" class="UserTable">
			  <tr height="50" style="background-color: #008FD7">
			    <td colspan="2" style="color: #FFFFFF; font-size: 35px;" ><div align="center">客户添加</div></td>
			  </tr>
			  <tr>
			    <td width="135"><div align="right">客户名：</div></td>
			    <td width="332">
			      
			        <input type="text" id="username"/>
			        
			       </td>
			  </tr>
			  
			  <tr>
			    <td><div align="right">性别：</div></td>
			    <td>
			      
			        <select name="select" id="usertype">
				          <option></option>
				          <option>男</option>
				          <option>女</option>
				        </select>
			        </td>
			  </tr>
			  <tr>
			    <td><div align="right">出生日期:</div></td>
			    <td>
			    
			      <input type="text" id="datepicker">
			     
			    </td>
			  </tr>
			  <tr>
			    <td><div align="right">备注：</div></td>
			    <td><textarea id="remark"cols="30" rows="4"></textarea></td>
			  </tr>
			  <tr>
			    <td>
			    	<div align="center">
			    		<input type="reset" class="button" id="resetAU"  value="清    空" />
		    		</div>
	    		</td>
			    <td>
			     <div align="center">
			      	<input type="submit" class="button" id="add" value="提   交" />
			     </div>
			    </td>
			  </tr>
			</table>
		<div id="addState"></div>
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

<%@ page language="java" import="java.util.*" pageEncoding="GBK"%>
<%
String path = request.getContextPath();
String basePath = request.getScheme()+"://"+request.getServerName()+":"+request.getServerPort()+path+"/";
%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
  <head>
  <meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <title>��Ϣ����</title>
    <script type="text/javascript" src="../js/jquery.js"></script>
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />

    <script src="http://maps.google.com/maps/api/js?sensor=true" type="text/javascript"></script>
    <script src="http://google-maps-utility-library-v3.googlecode.com/svn/trunk/routeboxer/src/RouteBoxer.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/map.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/main.css" />
    <script type="text/javascript" src="../js/calender.js"></script>
     <script type="text/javascript">
   
	function postInfo(){
		with(document.getElementById("postForm")){
				method = "post";
				action = "postInfo.do";
				submit();
			}
	}
	$(function(){
			$("#exit").click(function(){
				
     			with(document.getElementById("postForm")){
     				method = "post";
					action = "exit.do";
					submit();
     			}
     		});
	});

</script>
<style type="text/css">
	table {
		font-size:15px;
	}
	table input {
		font-size:15px;
	}
</style>
	
</head>
  
<body onload="initialize()">
<form id="postForm" name="form1"  >
   <div style="display:none">
  		<input type="text" id="username" name="userName" value=${user} />
   		<input type="text" id="scord" name="scord"/>
		<input type="text" id="ecord" name="ecord"/>
   		<button id="undo" style="display:none;margin:auto" onClick="undo()">Undo          </button>
     </div>
<div>
	<div style="float:left;margin:0px;width:10%">
		<a id="logo"  style="cursor:hand" ><img src="../images/logo.jpg" /></a>
	</div>
	
	<div>
	  <table width="85%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	    
	      <td width="40%">&nbsp;</td>
	     
	      <td width="18%" style="color:white;font-size:15px">${user}��ӭ���� |</td>
	     
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/success.jsp">������ҳ |</a></td>
	      
	      <td width="10%" style=";font-size:15px;color:white"><a href="../form/personal.jsp">������ҳ |</a></td>
	      <td width="5%" style=";font-size:15px;color:white"><a href="#" id="exit">�˳�</a></td>
	    </tr>
	
	  </table>
	</div>
	<dir >
		<center>
			<h1>��Ϣ����</h1>
		</center>
	</dir>
</div>
<div  >
	
	
  <div style="clear:both" >
    <div style="float:left;background-color:#EEEEEE;height:500px;width:32%;">
   	
    <table width="90%" height="412" border="0" bordercolor="#FFFFFF" id="table">
      <tr>
        <td><div align="right">������:</div></td>
        <td><label>
          <input name="start" type="text" size="15" maxlength="30"  id="start" onBlur="showstart()"/>
        </label></td>
      </tr>
      <tr>
        <td><div align="right">Ŀ�ĵ�:</div></td>
        <td><label>
          <input name="end" type="text" size="15" maxlength="30"  id="end" onBlur="showend()"/>
        </label></td>
      </tr>
      <tr align="center" style="display:none">
        <td colspan="2"><label>
           <input name="button" type="submit" value="��ѯƴ��" />
        </label>          <label>
          <input type="button" name="Submit2" value="��������"  onclick="codeAddress()"/>
          </label><br><br></td>
      </tr>
      <tr>
        <td><div align="right" >��������:</div></td>
        <td><label>
          <input name="date" type="text" size="8" maxlength="20" onfocus="HS_setDate(this)"/>
        </label></td>
      </tr>
      
      
      <td align="right" >ƴ��ʱ��:</td>
      <td>
      	<table>
      		<tr>
      		<td>
      		  <select name="hour" size="1">
	          <option value="01">1</option>
	          <option value="02">2</option>
	          <option value="03">3</option>
	          <option value="04">4</option>
	          <option value="05">5</option>
	          <option value="06">6</option>
	          <option value="07">7</option>
	          <option value="08">8</option>
	          <option value="09">9</option>
	          <option value="10">10</option>
	          <option value="11">11</option>
	          <option value="12">12</option>
	          <option value="13">13</option>
	          <option value="14">14</option>
	          <option value="15">15</option>
	          <option value="16">16</option>
	          <option value="17">17</option>
	          <option value="18">18</option>
	          <option value="19">19</option>
	          <option value="20">20</option>
	          <option value="21">21</option>
	          <option value="22">22</option>
	          <option value="23">23</option>
	          <option value="24">24</option>
        	</select>
        	</td>
        	<td>��</td>
        	<td>
        		<select name="minute" size="1">
		          <option value="00">00</option>
		          <option value="10">10</option>
		          <option value="20">20</option>
		          <option value="30">30</option>
		          <option value="40">40</option>
		          <option value="50">50</option>
		        </select>
        	</td>
        	<td>��</td>
      		</tr>
      	</table>
      </td>
      <tr>
      	<td align="right">ƴ������:</td>
      	<td><label>
          <select name="frequency" size="1">
		    <option value="��ʱƴ��">��ʱƴ��</option>
		    <option value="������ƴ��">������ƴ��</option>
		    <option value="�ڼ���ƴ��">�ڼ���ƴ��</option>
		  </select>
        </label></td>
      </tr>
      <tr>
        <td><div align="right">��������:</div></td>
        <td><label>
          <select name="limit" size="1">
		    <option value="1">1</option>
		    <option value="2">2</option>
		    <option value="3">3</option>
		  </select>
        </label></td>
      </tr>
      <tr>
        <td><div align="right" >�Ա�Ҫ��:</div></td>
        <td><label>
        <select name="sex" size="1">
		    <option value="����">����</option>
		    <option value="����Ů��">����Ů��</option>
		    <option value="��������">��������</option>
		  </select>
		  </label></td>
      </tr>
      
      <tr>
        <td><div align="right" >��ע:</div></td>
        <td><label>
          <textarea name="remark" cols="23" rows="5"></textarea>
        </label></td>
      </tr>
      <tr>
      <td ></td>
        <td   >
         
            	<div  style="width:150px">
					<p id="test" onClick="postInfo()">�ύ��Ϣ</p>
				</div>
           
                 </td>
      </tr>
    </table >
   
    </div>
     
  
</div>  

	<div  style="background-color:#FFFFFF;height:44px;width:100%"></div>
	<div id="map" style="width: 100%; height: 500px;" ></div>    
	    
	<div id="directionsPanel"  style="position:absolute; right:0px; top:90px; width:200px;background-color:#CEEDFF">
		
	</div>
	<div style="position:absolute; right:10px; top:110px;">
		<a href="#" ><img title="�������" style="display:none;border:0px" id="hidden" src="../images/mark1.jpg"  /></a>
		
	</div>
	<div style="position:absolute; right:10px; top:110px;">
		
		<a href="#" ><img title="��ʾ���" style="display:none;border:0px" id="show" src="../images/mark2.jpg" /></a>
	</div>
 
   
    <p>&nbsp;    </p>
  
</form>

</body>
</html>

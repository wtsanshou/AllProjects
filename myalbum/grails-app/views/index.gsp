<%@ page import="ourAlbum.*" %>

<html>
  <head>
 
<title>Login page</title>
<script type="text/javascript" src="js/jquery.js"></script>

<script type="text/javascript">
	$(function(){
		$(".button").mousedown(function(){
			$(".button").css("border-top" ,"1px solid #000000")
						.css("border-left" ,"1px solid #000000")
						.css("border-bottom" ,"1px solid #EEEEEE")
						.css("border-right" ,"1px solid #EEEEEE")
						;
		});
		$(".button").mouseup(function(){
			$(".button").css("border-top" ,"1px solid #EEEEEE")
					.css("border-left" ,"1px solid #EEEEEE")
					.css("border-bottom" ,"1px solid #000000")
					.css("border-right" ,"1px solid #000000");
		});
		
		$(".img").mouseover(function(){
			$(this).css("width","180px").css("height","180px");
		}).mouseout(function(){
			$(this).css("width","120px").css("height","120px");
		});
		
	});
	
	function register(){
		with(document.getElementById("searchForm")){
				method = "post";
				action = "search.do";
				submit();
			}	
	}
	
	
</script>

<style type="text/css">

.button {
	font-family: Arial;
	font-size: .8em;
	text-align:center;
	margin:3px;
	cursor:hand;
	color: #FFFFFF;
	padding:4px 10px 4px 10px;
	background-color: #0A5CA7;
	text-decoration: none;
	border-top: 1px solid #EEEEEE;		
	border-left: 1px solid #EEEEEE;
	border-bottom: 1px solid #000000;
	border-right: 1px solid #000000;
} 

#image td{
    width:180px;
    height:180px
}
</style>
</head>
<body >

<div>
	<g:form controller="user">
	  <table width="100%" border="0" style="background-color:#0A5CA7;"  id="head">
	    <tr bordercolor="#FFFFFF">
	    <td width="10%"></td>
	    <td width="20%" style="color:white;font-size:20px;" ><a style="font-size:25px;color:white">myalbum</a></td>
	      <td width="20%">&nbsp;</td>
	      <td width="7%" style="color:white;font-size:15px;" align="right">Name:</td>
	      <td width="10%"><input name="username" type="text" size="15"></td>
	      <td width="5%" style="color:white;font-size:15px" align="right">Password:</td>
	      <td width="10%"><input name="password" type="password" size="15"></td>
	      <td width="5%" style="color:white;font-size:15px"><g:actionSubmit value="Log in" class="button" action="login"/></td>
	      <td width="5%" >&nbsp;</td>
	      
	    </tr>
	  </table>
	</g:form>
	</div>
<div>
<div style="float:left;width:60%; height:500px;" align="center" >
<table  id="image">
     <tr>
     <g:set var="key" value="${0}" />
	<g:each in="${Picture.list(max:9)}">
		
		<g:if test="${key % 3 == 0 && key != 0}">
	     	</tr><tr><td><a href="#"><img class="img" src="${it.path }" width="120px" height="120px"></img></a></td>
	     	<g:set var="key" value="${key + 1}" />
		</g:if>
		<g:elseif test="${key < 9}">
		    <td><a href="#"><img class="img" src="${it.path }" width="120px"  height="120px"></img></a></td>
		    <g:set var="key" value="${key + 1}" />
		</g:elseif>
		
		
	</g:each>
	
</table>

</div>

<div style="width:40%">
<g:form controller="user">
  <table width="360" height="338" border="0" style="margin-top:50px">
  
    <tr>
      <td  > <div align="right" >User Name:</div></td>
      <td width="234"><input name="username" type="text" size="30" style="height:30px"/></td>
    </tr>
    <tr>
      <td><div align="right">Password:</div></td>
      <td><input name="password" type="password" size="30" style="height:30px"/></td>
    </tr>
    <tr>
      <td style="width:140px"><div align="right">Reenter Password:</div></td>
      <td><input name="repassword" type="password" size="30" style="height:30px"/></td>
    </tr>
    <tr>
      <td><div align="right">Sex:</div></td>
      <td>
      	<input type="radio" name="sex" value="Male" checked/>
			        Male
			      <input type="radio" name="sex" value="Female" />
			    	Female
      </td>
    </tr>
    <tr>
      <td><div align="right"></div></td>
      <td>
      <g:actionSubmit class="button" value="Register" action="register"/>
         </td>
    </tr>
  </table>
  
  
</g:form>
</div>
</div>
</body>
</html>
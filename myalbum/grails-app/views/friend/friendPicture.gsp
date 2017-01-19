
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />


<title>Friend picture page</title>
<script type="text/javascript" src="../../js/jquery.js"></script>
<g:javascript library="prototype" />
<script type="text/javascript">
	(function($){
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
		
	
		$(".highlight").hover(function(){
			$(this).css("background-color","#3399FF");
			$(this).children("a").css("color","#FFFFFF");
			//$(this).css("cursor","hand");
			//$(this).next("a").css("color","#FFFFFF");
		},function(){
			$(this).css("background-color","#FFFFFF");
			$(this).children("a").css("color","#0A5CA7");
		});
		
		$(".main > a").click(function(){
			
			var ulNode = $(this).next("ul");
			$(this).next("ul").slideToggle();
			//ulNode.css("display","block");
			//alert(ulNode.css("display"));
			//ulNode.css("display","block");
		});
		
		$("#showAS").click(function(){
			$("#defaultInfo").css("display","none");
			
			$("#PS").css("display","none");
			$("#addAlbumInfo").css("display","none");
			$("#topten").css("display","none");
			$("#album").css("display","none");
			$("#AS").css("display","block");
		});
		
		$("#showPS").click(function(){
			$("#defaultInfo").css("display","none");
			$("#AS").css("display","none");
			
			$("#addAlbumInfo").css("display","none");
			$("#topten").css("display","none");
			$("#album").css("display","none");
			$("#PS").css("display","block");
		});
		
		$("#showAddAlubm").click(function(){
			$("#defaultInfo").css("display","none");
			$("#AS").css("display","none");
			$("#PS").css("display","none");
			$("#topten").css("display","none");
			$("#album").css("display","none");
			$("#addAlbumInfo").css("display","block");
		});
		
		$("#top").click(function(){
			$("#defaultInfo").css("display","none");
			$("#AS").css("display","none");
			$("#PS").css("display","none");
			$("#addAlbumInfo").css("display","none");
			$("#album").css("display","none");
			$("#topten").css("display","block");
		});
		
		$("#viewAlbum").click(function(){
			$("#defaultInfo").css("display","none");
			$("#AS").css("display","none");
			$("#PS").css("display","none");
			$("#addAlbumInfo").css("display","none");
			$("#topten").css("display","none");
			$("#album").css("display","block");
		});
		
		
		$(".img").click(function(){
			$("#showComment").css("display","block")
			var img =$(this).attr("src");
			$("#mainImg").attr("src",img).css("width","200px").css("height","200px");
			var pname = $(this).next(".pname").val();
			var pval = $(this).siblings(".pval").val();
			$("#comment").val("");
			$("#pv").html(pval);
			$("#needname").val(pname);
		});
		
		$(".addF").click(function(){
			
			$(this).css("display","none");
		});
		
		
		$("#search").click(function(){
			alert("sdfsfsda");
			with(document.getElementById("searchForm")){
				method = "post";
				//controller = "album";
				action = "search";
				submit();
				
			}
			
		});
		
	});
	})(jQuery);

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
	a
	{
		text-decoration: none;
		
	}
	.main{
		
		width:200px;
		background:#0A5CA7;
		background-repeat: repeat-x;
	}
	.main li{
		background:#FFFFFF;
		
		color:#0A5CA7;
	}
	ul,li{
		padding-top: 3px;
		list-style: none;
		
	}
	ul{
		padding: 0;
		margin: 0;
	}
	li a{
		color:#FFFFFF;
		width:180px;
	}
	.main a {
		font-size:22px;
	}
	.main li a{
		color:#0A5CA7;
		font-size:18px;
	}
	.main ul {
		display: none;
	}
	
</style>
</head>

<body>

	
	<div>
	  <table width="100%" border="0" style="background-color:#0A5CA7" id="head">
	    <tr bordercolor="#FFFFFF">
	    <td width="10%"></td>
	      <td width="20%" style="color:white;font-size:20px;" ><g:link controller="user" action="home" id="${session.user.id}" style="font-size:25px;color:white">myalbum</g:link></td>
	      
	      <td width="20%" ></td>
	      <td width="5%" style="color:white" > </td>
	      <td width="13%" style="color:white;font-size:15px" align="right"><g:link controller="user" action="home" id="${session.user.id}" style="font-size:15px;color:white">${session.user.username}</g:link> &nbsp;&nbsp;|</td>
	     
	      <td width="12%" style="color:white;font-size:15px;"align="right"><g:link controller="album" action="showSearch" id="${session.user.id}" style="font-size:15px;color:white">Search pictures</g:link>
	       &nbsp;&nbsp;|</td>
	      <td width="7%" style="font-size:15px;color:white">
	      <g:link controller="user" action="home" id="${session.user.id}" style="font-size:15px;color:white">Home</g:link> &nbsp;&nbsp;|</td>
	      <td width="13%" style="font-size:15px;color:white"><g:link controller="user" action="logout" style="font-size:15px;color:white">
						Exit</g:link> &nbsp;|</td>
	    </tr>
	
	  </table>
	</div >
    <div style="width:100%">
    	<div style="float:left;width:10%">
    
    	</div>
    	<div style="width:80%;padding-left:10%">
  <table width="100%"  border="0">
  <tr>
    <td width="25%" valign="top">
    <div style="float:left;width:20%">
    	<table width="200" border="0">
            <tr>
              <td><table width="200" border="0">
                <tr>
                  <td width="97" rowspan="3" ><img src="../../${user.img }" width="65" height="65" /></td>
                  <td width="93"><div align="left"> ${user.username }</div></td>
                </tr>
                
                <tr>
                  <td><div align="left">grade:&nbsp; ${user.grade }</div></td>
                </tr>
                <tr>
                	<td ><g:remoteLink class="addF" id="${user.id }" controller="friend" action="addFriend">Add Friend</g:remoteLink></td>
                </tr>
              </table></td>
            </tr>
            <tr>
              <td  >
              <hr/>
              <ul>
              	
              	
              	<li class="main">
              		 <a href="#" >Album</a>
              		
              		<ul>
              			
              			<li class="highlight"><a id="viewAlbum" href="#">View album</a></li>
              			
              		</ul>
              	</li>
              	<hr/>
              	<li class="main">
              		<a href="#" >Friends</a>
              	</li>
              </ul>
              
              </td>
            </tr>
            
            <tr>
              <td >
              
              <table width="100%" >
              	<g:each in="${user.friends}">
              		<tr>
              		<td rowspan="2"><img src="../../${it.img }" width="65" height="65"></img></td>
              		<td align="center">${it.username}</td>
              		</tr>
              		<tr align="center">
              			<td><g:link id="${it.username }" controller="friend" action="seeFriend" >See</g:link></td>
              		</tr>
              	</g:each>
              
              </table>
              
              
              </td>
            </tr>
            <tr>
            	<td><hr/></td>
            </tr>
          </table>
    </div>
    </td>
    <td width="50%" valign="top" id="defaultInfo" style="display:block">
    <h2>Welcome to Myalbum, ${session.user.username}</h2>
    <hr/>
    <div id="showComment" style="display:none">
    <g:form controller="friend">
    	<table >
    	
    	  <tr>
		    <td rowspan="4"><img src="" id="mainImg"></img></td>
		    <td>Prefer value:</td>
		    <td id="pv"></td>
		  </tr>
		  <tr>
		    <td>Comment:</td>
		    <td><textarea name="comment" id="comment" cols="20" rows="5"></textarea></td>
		  </tr >
		  <tr >
		    <td colspan="2">
		    	
		       <input type="radio" name="preferValue" value="1" id="r0" />1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		      
		       <input type="radio" name="preferValue" value="2" id="r1" />2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		       
		       <input type="radio" name="preferValue" value="3" id="r2" />3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		       
		       <input type="radio" name="preferValue" value="4" id="r3" />4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		       
		       <input type="radio" name="preferValue" value="5" id="r4" />5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		       
		    </td>
		    
		  </tr>
		  <tr >
		    <td colspan="2" align="center">
		    <input name="pname" id="needname" style="display:none" value=""> </input>
		    
		    <g:actionSubmit class="button"  value="Submit" action="comment" /></td>
		    
		  </tr>
		  <tr><td colspan="3"><hr></hr></td>
		  
		  </tr>
		  
    	</table>
    	</g:form>
    	</div>
    	<table>
					<thead>
						<tr>
							
							<th>Author :</th>
							<th>Album :</th>
							<th>Picture:</th>
							<th>Picture Name:</th>
							
							<g:sortableColumn property="pic_date" defaultOrder="desc" title="Prefer value:" titleKey="Picture.pic_date" />
						</tr>
					</thead>
					<tbody>
						<g:each in="${picture_list}" >
							
									<tr style="height: 50px" >
										
										<td align="center">${it.album.user.username }</td>
										<td align="center">${it.album.name }</td>
										<td align="center" ><a href="#"><img src="../../${it.path }" width="50" height="50" class="img"/>
										<input class="pname"  value="${it.name}" style="display:none">
										  </input>
										<input class="pval" value="${it.prefer_value}" style="display:none">												
										  </input>
										  
										  </a>

										</td>
										<td align="center">${it.name }</td>
										<td align="center" class="preferV">${it.prefer_value}</td>
										
											
									</tr>
							</g:each>
					</tbody>
				</table>
    </td>
    
    <td width="50%" id="album" valign="top" id="defaultInfo" style="display:none">
    <h2>Welcome to Myalbum, ${session.user.username}.</h2>
    <hr/>
    	<div >
				<table>
				<thead>
					<tr>
					<th>Album name</th>
					<th>Create date</th>
					<th>Click to see picture</th>
					</tr>
					</thead>
					<tbody>
					
					<g:each in="${album_list}">
						<tr>
							<td align="center">${it.name}</td>
							<td align="center">${it.album_date }</td>
							<td align="center"><g:link controller="friend" action="showFriendPicture" id="${it.id}">See it</g:link>
							</td>
						</tr>
					</g:each>
					</tbody>
				</table>
			</div>
    </td>
    
    <td width="50%" valign="top" id="topten" style="display:none">
    <h2>Welcome to Myalbum, ${session.user.username}.</h2>
    <hr/>
    	<table width="100%" border="0"cellspacing="0">
    	<tr>
    	<th>Picture:</th>
    	<th align="left">Prefer:</th>
    	<th align="left">Author:</th>
    	<th></th>
    	</tr>
    	<g:each in="${topPicture}">
	    	<tr>
	    		<td align="left"> <img src="../../${it.path }" width="250px"/></td>
	    		<td align="left">${it.prefer_value}</td>
	    		<td align="left">${it.album.user.username}</td>
	    		<td align="left"><g:link controller="friend" action="friendPicture" id="${it.name}"> See it</g:link> </td>
	    		
	    	</tr>
    	</g:each>
    	</table>
    </td>
    
    <td width="25%" valign="top">
    	
    	<table width="100%" border="0"cellspacing="0" style="padding-top:20px">
    		
    		<tr style="background:#0A5CA7"><th style="font-size:22px;color:#FFFFFF" width="75%">Top Ten</th><th align="right" style="font-size:12px;"><a href="#" id="top" style="color:#FFFFFF">See more</a></th></tr>
    		<tr><td colspan="2"> <hr></hr></td></tr>
    		<g:each in="${topPicture}">
    		<tr>
    		<td colspan="2"><img src="../../${it.path }" width="260px" ></img></td>
    		
    		</tr>
    		</g:each>
    	</table>
    <hr/>
    	
    </td>
  </tr>
</table>
  </div>
  
    </div>
</body>
</html>

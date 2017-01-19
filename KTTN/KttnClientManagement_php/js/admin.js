
$(function(){

	/*
	 * control the style of button click
	 * */
	buttonStyle();
	
	/*
	 * use ajax to show admin user information
	 * */
	$("#admPage").click(function(){
		
		$("#manager").css("display","none");
        $("#staff").css("display","none");
        $("#addUser").css("display","none");
        $("#modLog").css("display","none");
        $("#modUser").css("display","none");
        
        $("#admin").css("display","block");
		
      //function from ajaxApp.js
		getAdmin();
    });
	
	/*
	 * use ajax to show manager user information
	 * */
	$("#manaPage").click(function(){
		
		$("#addUser").css("display","none");
        $("#staff").css("display","none");
        $("#admin").css("display","none");
        $("#modLog").css("display","none");
        $("#modUser").css("display","none");
        
        $("#manager").css("display","block");
		
      //function from ajaxApp.js
		getManager();
		
    });
	
	/*
	 * use ajax to show manager user information
	 * */
	$("#staffPage").click(function(){
		
		$("#addUser").css("display","none");
        $("#manager").css("display","none");
        $("#admin").css("display","none");
        $("#modLog").css("display","none");
        $("#modUser").css("display","none");
        
        $("#staff").css("display","block");
		
      //function from ajaxApp.js
		getStaff();
		
    });
	
	/*
	 * use ajax to show add user page
	 * */
	$("#addPage").click(function(){
		
		$("#manager").css("display","none");
        $("#staff").css("display","none");
        $("#admin").css("display","none");
        $("#modLog").css("display","none");
        $("#modUser").css("display","none");
        
        $("#addUser").css("display","block");
		
      //function from ajaxApp.js
		toAddUser();
		
    });
	
	$("#logPage").click(function(){
		
		$("#manager").css("display","none");
        $("#staff").css("display","none");
        $("#admin").css("display","none");
        $("#addUser").css("display","none");
        $("#modUser").css("display","none");
        
        $("#modLog").css("display","block");
		
      //function from ajaxApp.js
		viewLog();
		
    });
	
	/*
	 * clear session when user click the menu 退出
	 * */
	/*$("#exitPage").click(function(){
		//function from ajaxApp.js
		alert("dsfsd");
		clearSession();
	});
	*/
});



/*
 * control the style of button click
 * */
function buttonStyle()
{
	//normal button
	$(".button").mousedown(
			
			function() {
				

				$(this).css("border-top", "1px solid #000000").css(
						"border-left", "1px solid #000000").css(
						"border-bottom", "1px solid #EEEEEE").css(
						"border-right", "1px solid #EEEEEE");
			});
	$(".button").mouseup(
			function() {
				$(this).css("border-top", "1px solid #EEEEEE").css(
						"border-left", "1px solid #EEEEEE").css(
						"border-bottom", "1px solid #000000").css(
						"border-right", "1px solid #000000");
			});
	
	//button modify
	$(".modify").mousedown(
			
			function() {
				
				$(this).css("border-top", "1px solid #000000").css(
						"border-left", "1px solid #000000").css(
						"border-bottom", "1px solid #EEEEEE").css(
						"border-right", "1px solid #EEEEEE");
			});
	$(".modify").mouseup(
			function() {
				$(this).css("border-top", "1px solid #EEEEEE").css(
						"border-left", "1px solid #EEEEEE").css(
						"border-bottom", "1px solid #000000").css(
						"border-right", "1px solid #000000");
			});
	
	//button delete
	$(".delete").mousedown(
			
			function() {
				
				$(this).css("border-top", "1px solid #000000").css(
						"border-left", "1px solid #000000").css(
						"border-bottom", "1px solid #EEEEEE").css(
						"border-right", "1px solid #EEEEEE");
			});
	$(".delete").mouseup(
			function() {
				$(this).css("border-top", "1px solid #EEEEEE").css(
						"border-left", "1px solid #EEEEEE").css(
						"border-bottom", "1px solid #000000").css(
						"border-right", "1px solid #000000");
			});
	
	
	//reset the addUser page
	$("#resetAU").click(
			function(){
				resetAddUser();
			}
	);
	
	//add a user
	$("#add").click(
			function(){
				var user_name = $("#username").val();
				var password = $("#password").val();
				var user_type = $("#usertype").val();
				var remark = $("#remark").val();
				//function from ajaxApp.js
				addUser(user_name,password,user_type,remark);
				
				
				resetAddUser();
			}
	);
	
	/*
	 * delete in admin, manager and staff table
	 * */
	$(".delete").click(
			function(){
				var user_name = $(this).parents("tr").children(".un").html();
				
				$(this).parents("tr").css("display","none");
				//function from ajaxApp.js
				delUser(user_name);
				
			});
	
	/*
	 * modify in admin, manager and staff table
	 * */
	$(".modify").click(
			function(){
				var user_name = $(this).parents("tr").children(".un").html();
				
				$("#manager").css("display","none");
		        $("#staff").css("display","none");
		        $("#admin").css("display","none");
		        $("#modLog").css("display","none");
		        $("#addUser").css("display","none");
		        
		        $("#modUser").css("display","block");
				
		      //function from ajaxApp.js
		        modUserPage(user_name);
				
			});
	/*
	 * reset the addUser page
	 * */
	$("#resetMU").click(
			function(){
				resetModUser();
			});
	
	$("#mod").click(
			function(){
				var user_name = $("#modUsername").html();
				var password = $("#modPassword").val();
				var user_type = $("#modUsertype").val();
				var remark = $("#modRemark").val();
				
				//function from ajaxApp.js
				modUser(user_name,password,user_type,remark);
				
				
				resetModUser();
	});
	
	
}

function resetModUser()
{
	$("#modPassword").val("");
	$("#modUsertype").val("员工");
	$("#modRemark").val("");
}

function resetAddUser()
{
	$("#username").val("");
	$("#password").val("");
	$("#usertype").val("员工");
	$("#remark").val("");
	
}


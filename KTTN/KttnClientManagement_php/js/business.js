$(function(){
	
	$(".listMenu > a").click(function(){
		
		var ulNode = $(this).next("ul");
		$(this).next("ul").slideToggle();
	});
	
	$(".highlight").hover(function(){
		$(this).css("background-color","#3399FF");
	},function(){
		$(this).css("background-color","#0A5CA7");
	});
	
	/*
	 * event 查看客户列表
	 * */
	$("#viewClient").click(function(){
		
		$("#clientDeta").css("display","none");
        $("#clientMod").css("display","none");
        $("#cliRegMem").css("display","none");
        $("#clientAdd").css("display","none");
        $("#clientSta").css("display","none");
        
        $("#expert").css("display","none");
        $("#expertMod").css("display","none");
        $("#expertAdd").css("display","none");
        $("#expertSta").css("display","none");
        
        $("#memInfo").css("display","none");
        $("#memServ").css("display","none");
        $("#memSta").css("display","none");
        
        $("#actInfo").css("display","none");
        $("#actMod").css("display","none");
        $("#actAdd").css("display","none");
        
        $("#servInfo").css("display","none");
        $("#servMod").css("display","none");
        $("#servAdd").css("display","none");
        
        $("#clientInfo").css("display","block");
		
		getClient();
	});
	
	/*
	 * event 进入客户信息添加页
	 * */
	
	$("#addCliPage").click(function(){
		
		$("#clientDeta").css("display","none");
        $("#clientMod").css("display","none");
        $("#cliRegMem").css("display","none");
        $("#clientInfo").css("display","none");
        $("#clientSta").css("display","none");
        
        $("#expert").css("display","none");
        $("#expertMod").css("display","none");
        $("#expertAdd").css("display","none");
        $("#expertSta").css("display","none");
        
        $("#memInfo").css("display","none");
        $("#memServ").css("display","none");
        $("#memSta").css("display","none");
        
        $("#actInfo").css("display","none");
        $("#actMod").css("display","none");
        $("#actAdd").css("display","none");
        
        $("#servInfo").css("display","none");
        $("#servMod").css("display","none");
        $("#servAdd").css("display","none");
        
        $("#clientAdd").css("display","block");
        
        toAddCliPage();
	});
	
	
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
	
	//button detail
	$(".detail").mousedown(
			
			function() {
				
				$(this).css("border-top", "1px solid #000000").css(
						"border-left", "1px solid #000000").css(
						"border-bottom", "1px solid #EEEEEE").css(
						"border-right", "1px solid #EEEEEE");
			});
	$(".detail").mouseup(
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
	
	/*$(".date").click(function(){
		$(this).DatePicker(
				flat: true,
				date: '2008-07-31',
				current: '2008-07-31',
				calendars: 1,
				starts: 1
			});
	});*/
	
	/*$('#datepicker').datepicker({  
        showOn: "button",  
        buttonImage: "Images/calendar.gif",  
        buttonImageOnly: true  
    });  */
	
}
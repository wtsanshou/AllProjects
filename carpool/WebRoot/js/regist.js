
$(function(){
			var un = $("#un");
			var nn = $("#nn");
			var pw = $("#pw");
			var rpw = $("#rpw");
			var em = $("#em");
			var ph = $("#ph");
			var code = $("#code");
			
			un.blur(function(){
				/*
			
				 */
				if(un.val().length < 1 ||un.val() == "" ){
					$("#nur").css("display","none"); 
				 	$("#nuw").css("display","block");
					$("#nue").css("display","block");
					$("#nue").val("用户名不能为空！");
					return;
				} 
				if(!((un.val().charAt(0) >='a' && un.val().charAt(0) <='z')||(un.val().charAt(0) >='A' && un.val().charAt(0) <='Z'))){
					$("#nur").css("display","none"); 
					$("#nuw").css("display","block");
					$("#nue").css("display","block");
					$("#nue").val("用户名必须为字母开头！");
					return;
				}
				$("#nuw").css("display","none");
				$("#nue").css("display","none");
				$("#nur").css("display","block"); 
				//nn.focus();
			});
				/*
				
				 */
				 nn.blur(function(){
					 if(nn.val().length < 1 || nn.val() == ""){
					 	$("#nnr").css("display","none"); 
					 	$("#nnw").css("display","block");
						$("#nne").css("display","block");
						$("#nne").val("昵称不能为空！");
						return;
					 }
					 $("#nnw").css("display","none");
					$("#nne").css("display","none");
					$("#nnr").css("display","block"); 
					//pw.focus();
				 });
				 /*
				
				 */
				 pw.blur(function(){
					 if(pw.val().length < 6 || pw.val() == ""){
					 	$("#pwr").css("display","none"); 
					 	$("#pww").css("display","block");
						$("#pwe").css("display","block");
						$("#pwe").val("密码必须大于六位！");
						return;
					 }
					 $("#pww").css("display","none");
					 $("#pwe").css("display","none");
					 $("#pwr").css("display","block"); 
					 //rpw.focus();
				 });
				 /*
				 
				  */
				rpw.blur(function(){
					
					 if(rpw.val() != pw.val()){
					 	$("#rpwr").css("display","none"); 
					 	$("#rpww").css("display","block");
						$("#rpwe").css("display","block");
						$("#rpwe").val("两次密码输入不一致！");
						return;
					 }
					 $("#rpww").css("display","none");
					 $("#rpwe").css("display","none");
					 $("#rpwr").css("display","block"); 
					// em.focus();
				 });
				 /*
				
				  */
				em.blur(function(){
				var pattern = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
					 if(!pattern.test(em.val())){
					 	$("#emr").css("display","none"); 
					 	$("#emw").css("display","block");
						$("#eme").css("display","block");
						$("#eme").val("输入邮箱格式不正确！");
						return;
					 }
					 $("#emw").css("display","none");
					 $("#eme").css("display","none");
					 $("#emr").css("display","block"); 
					// ph.focus();
				 });
				  /*
				
				   */
				ph.blur(function(){
					var pattern = /([0-9]{3,4}\-[1-9]{1}[0-9]{6})|(^1[0-9]{10})$/;
					 if(!pattern.test(ph.val())){
					 	$("#phr").css("display","none"); 
					 	$("#phw").css("display","block");
						$("#phe").css("display","block");
						$("#phe").val("电话号码格式不正确");
						return;
					 }
					 $("#phw").css("display","none");
					 $("#phe").css("display","none");
					 $("#phr").css("display","block"); 
					 //code.focus();
				 });
				 /*
				
				  */
				  	 
	});
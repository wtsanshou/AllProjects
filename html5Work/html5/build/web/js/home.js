$(function(){
    //tabs
    $("#tabs li a").hover(function(){
        $(this).css("opacity","0.9");
    }, function(){
         $(this).css("opacity","0.7");
    });
    
    $("#page1").click(function(){
        $("#info").css("display","none");
        $("#images").css("display","none");
        $("#home").css("display","block");
    });
    $("#page2").click(function(){
        ajaxFunction();
        
        
        $("#home").css("display","none");
        $("#images").css("display","none");
        $("#info").css("display","block");
        
       
//        alert($(".sight").length);
        
    });
    $("#page3").click(function(){
        $("#home").css("display","none");
        $("#info").css("display","none");
        $("#images").css("display","block");
    });
    
    //introduce
    $("#i1").click(function(){
        $(this).attr("src","images/frameselector_active.png");
        $("#i2").attr("src","images/frameselector.png");
        $("#i3").attr("src","images/frameselector.png");
        $("#i4").attr("src","images/frameselector.png");
        $("#i5").attr("src","images/frameselector.png");
        
        $(".intro1").css("display","block");
        $(".intro2").css("display","none");
        $(".intro3").css("display","none");
        $(".intro4").css("display","none");
        $(".intro5").css("display","none");
    });
    $("#i2").click(function(){
        $(this).attr("src","images/frameselector_active.png");
        $("#i1").attr("src","images/frameselector.png");
        $("#i3").attr("src","images/frameselector.png");
        $("#i4").attr("src","images/frameselector.png");
        $("#i5").attr("src","images/frameselector.png");
        
        $(".intro1").css("display","none");
        $(".intro2").css("display","block");
        $(".intro3").css("display","none");
        $(".intro4").css("display","none");
        $(".intro5").css("display","none");
    });
    $("#i3").click(function(){
        $(this).attr("src","images/frameselector_active.png");
        $("#i2").attr("src","images/frameselector.png");
        $("#i1").attr("src","images/frameselector.png");
        $("#i4").attr("src","images/frameselector.png");
        $("#i5").attr("src","images/frameselector.png");
        
        $(".intro1").css("display","none");
        $(".intro2").css("display","none");
        $(".intro3").css("display","block");
        $(".intro4").css("display","none");
        $(".intro5").css("display","none");
    });
    $("#i4").click(function(){
        $(this).attr("src","images/frameselector_active.png");
        $("#i2").attr("src","images/frameselector.png");
        $("#i3").attr("src","images/frameselector.png");
        $("#i1").attr("src","images/frameselector.png");
        $("#i5").attr("src","images/frameselector.png");
        
        $(".intro1").css("display","none");
        $(".intro2").css("display","none");
        $(".intro3").css("display","none");
        $(".intro4").css("display","block");
        $(".intro5").css("display","none");
    });
    $("#i5").click(function(){
        $(this).attr("src","images/frameselector_active.png");
        $("#i2").attr("src","images/frameselector.png");
        $("#i3").attr("src","images/frameselector.png");
        $("#i4").attr("src","images/frameselector.png");
        $("#i1").attr("src","images/frameselector.png");
        
        $(".intro1").css("display","none");
        $(".intro2").css("display","none");
        $(".intro3").css("display","none");
        $(".intro4").css("display","none");
        $(".intro5").css("display","block");
    });
    
    $("#i6").click(function(){
        var png = $(this).attr("src");
        var play = "images/play.png";
        var pause = "images/pause.png";
        if(png == play){
            $(this).attr("src","images/pause.png")
        }else if(png == pause){
            $(this).attr("src","images/play.png")
        }
    });
});
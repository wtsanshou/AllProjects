var map;
var geocoder;
var marker;
function initialize() {
        geocoder = new google.maps.Geocoder();
    var myOptions = {
      zoom: 6,
      center: new google.maps.LatLng(53.2052, -6.1535),
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map"),
        myOptions);
}
  function codeAddress() {

        var address = document.getElementById("forMap").value;
        
        geocoder.geocode( {'address': address}, function(results, status) {
          if (status == google.maps.GeocoderStatus.OK) {
          
              //var myLatlng = new google.maps.LatLng(results[0].geometry.location);
              
              map.setCenter(results[0].geometry.location);
              map.setZoom(7);
             marker = new google.maps.Marker({
			map: map, 
			draggable:true,
			animation: google.maps.Animation.DROP,
                        position: results[0].geometry.location,
                        title:address
		});
                
          } else {
                //alert("Geocode was not successful for the following reason: " + status);
          }
        });
  }
  
  function infoStyle(){
      $("#up").css("display","none");
        $(".sight").eq(3).css("display","none");
        $(".sight").eq(4).css("display","none");
        $(".sight").eq(5).css("display","none");
        $(".sight").eq(6).css("display","none");
        
        $(".sight").hover(function(){
          $(this).css("background-color","#1d1d1d")
          $(this).css("color","#FFFFFF");
          //alert($(this).find(".location").val());
          $("#forMap").val($(this).find(".location").val());
          codeAddress();
      },function(){
        $(this).css("background-color","#2a2a2a");
        $(this).css("color","#7f7f7f");
     		});
      
      
      
      $("button").hover(function(){
         $(this).css("opacity","0.8"); 
      },function(){
          $(this).css("opacity","0.5"); 
      });
      
      var key = 3;
      $("#up").click(function(){
          if(key==7){
              $("#down").css("display","block");
              $(".sight").eq(6).css("display","none");
              $(".sight").eq(3).css("display","block");
          }else if(key == 6){
              $(".sight").eq(5).css("display","none");
              $(".sight").eq(2).css("display","block");
          }else if(key == 5){
              $(".sight").eq(4).css("display","none");
              $(".sight").eq(1).css("display","block");
          }else if(key == 4){
              $(".sight").eq(3).css("display","none");
              $(".sight").eq(0).css("display","block");
              $("#up").css("display","none");
          }
           key --;
              
      });
      
      $("#down").click(function(){
          if(key==3){
              $("#up").css("display","block");
              $(".sight").eq(0).css("display","none");
              $(".sight").eq(3).css("display","block");
          }else if(key == 4){
              $(".sight").eq(1).css("display","none");
              $(".sight").eq(4).css("display","block");
          }else if(key == 5){
              $(".sight").eq(2).css("display","none");
              $(".sight").eq(5).css("display","block");
          }else if(key == 6){
              $(".sight").eq(3).css("display","none");
              $(".sight").eq(6).css("display","block");
              $("#down").css("display","none");
          }
             key ++;
              
      });
      
      
      
      
      $(".sight").click(function(){
          var name = $(this).find(".name").text();
//          alert(name);
          getDetails(name);
          $("#popupBack").css("display","block");
          $("#popupContact").css("display","block");
          
           
      });
  }
  
  function popupStyle(){
      $("#popupContactClose").click(function(){
          $("#popupBack").css("display","none");
          $("#popupContact").css("display","none");
          
      });
      $("#popupContactClose").hover(function(){
          $(this).css("background-color","#FE2400");
          $(this).css("border-color","#2B2B2B");
      }, function(){
          $(this).css("background-color","#2B2B2B");
          $(this).css("border-color","#1DFB04");
      });
  }
      
 
  
  


	var map = null;
    
    var directions = null;
   
    var distance = null; 
    var geocoder;
    var oldDirections = [];
    var currentDirections = null;
	var directionsDisplay;



  function initialize() {
	    geocoder = new google.maps.Geocoder();
     
      var mapOptions = {
        center: new google.maps.LatLng(31.270408, 120.745789),
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 12
      };
      
      map = new google.maps.Map(document.getElementById("map"), mapOptions);
      directionService = new google.maps.DirectionsService();
      directionsRenderer = new google.maps.DirectionsRenderer({ 
	  map: map,
	  
      draggable: true
      });      
    directionsRenderer.setMap(map);
	directionsRenderer.setPanel(document.getElementById("directionsPanel"));
    
    google.maps.event.addListener(directionsRenderer, 'directions_changed',
      function() {
        if (currentDirections) {
          oldDirections.push(currentDirections);
          setUndoDisabled(false);
        }
        currentDirections = directionsRenderer.getDirections();
      });

    setUndoDisabled(true);

    calcRoute();

    }

 function calcRoute() {
    var origin = document.getElementById("start").value;
    var destination = document.getElementById("end").value;
    var request = {
        origin:origin,
        destination:destination,
        travelMode: google.maps.DirectionsTravelMode.DRIVING
    };
    directionsService.route(request, function(response, status) {
      if (status == google.maps.DirectionsStatus.OK) {
        directionsRenderer.setDirections(response);
      }
    });
  }
 function undo() {
    currentDirections = null;
    directionsRenderer.setDirections(oldDirections.pop());
    if (!oldDirections.length) {
      setUndoDisabled(true);
    }
  }
 function setUndoDisabled(value) {
    document.getElementById("undo").disabled = value;
  }
  function route() {
      
      //document.getElementById("hidden").css("display","block");
      var request = {
        origin: document.getElementById("start").value,
        destination: document.getElementById("end").value,
        travelMode: google.maps.DirectionsTravelMode.DRIVING

      }
      directionService.route(request, function(result, status) {
        if (status == google.maps.DirectionsStatus.OK) {
          directionsRenderer.setDirections(result);
          var path = result.routes[1].overview_path;
          var boxes = routeBoxer.box(path, distance);
          drawBoxes(boxes);
        } else {
          alert("Directions query failed: " + status);
        }
      });
    }

function showstart(){
   		if(document.getElementById("start").value == ""){
   			
   		}else {
   		     codeAddress("start","scord");
  		}
   }
    
    function showend(){
   		if((document.getElementById("start").value == "") || (document.getElementById("end").value == "")){
   		
   		}else {
   			codeAddress("end","ecord");
   		 route();
   		}
   		 
   }

 function codeAddress(add1 ,add2) {
  
    var address = document.getElementById(add1).value;
 
    geocoder.geocode( { 'address': address}, function(results, status) {
      if (status == google.maps.GeocoderStatus.OK) {
			//alert(results[0].geometry.location);
		 document.getElementById(add2).value = results[0].geometry.location;
        map.setCenter(results[0].geometry.location);
       /* var marker = new google.maps.Marker({
            map: map, 
			draggable:true,
			animation: google.maps.Animation.DROP,
            position: results[0].geometry.location
            
        });*/
        
      } else {
        alert("Geocode was not successful for the following reason: " + status);
      }
    });
}

$(function(){
		$("#test").mousedown(function(){
			$("#test").css("border-top" ,"1px solid #000000")
						.css("border-left" ,"1px solid #000000")
						.css("border-bottom" ,"1px solid #EEEEEE")
						.css("border-right" ,"1px solid #EEEEEE")
						;
		});
		$("#test").mouseup(function(){
			$("#test").css("border-top" ,"1px solid #EEEEEE")
					.css("border-left" ,"1px solid #EEEEEE")
		
					.css("border-bottom" ,"1px solid #000000")
					.css("border-right" ,"1px solid #000000");
		});
		
	
		$("#end").blur(function(){
			$("#hidden").css("display","block");
		});
		
		$("#hidden").click(function(){
			$("#directionsPanel").animate({right:"-250px"},500,function(){
				$("#directionsPanel").css("display","none");
				$("#hidden").css("display","none");
				$("#show").css("display","block");
			});
			
		});
		
		$("#show").click(function(){
			$("#directionsPanel").animate({right:"0px"},0,function(){
				$("#directionsPanel").css("display","block");
				$("#hidden").css("display","block");
				$("#show").css("display","none");
			});
			
		});

		$("#logo").click(function(){
     			$(this).attr("href","success.jsp");
     		});
	});		
	
	

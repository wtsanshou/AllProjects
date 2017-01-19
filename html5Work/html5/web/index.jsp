

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html >
<html>
<head>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Ireland</title>

<link rel="stylesheet" href="css/style.css" type="text/css" media="all">

<script type="text/javascript" src="js/jquery-1.7.1.min.js" ></script>
   
<link href="http://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />
    <script src="http://maps.google.com/maps/api/js?sensor=true" type="text/javascript"></script>
    <script src="http://google-maps-utility-library-v3.googlecode.com/svn/trunk/routeboxer/src/RouteBoxer.js" type="text/javascript"></script>

<script type="text/javascript" src="js/roundabout.js"></script>
  <script type="text/javascript" src="js/roundabout_shapes.js"></script>

<script type="text/javascript" src="js/ajaxApp.js"></script>
<script type="text/javascript" src="js/map.js"></script>
<script type="text/javascript" src="js/home.js"></script>

<script type="text/javascript" src="js/canvas.js"></script>



</head>

<body >
	

<header>
  
    <img id="logo" src="images/ireland-logo1.jpg">
    <hgroup>
			<h1>Ireland Tours</h1>
			<h2>Welcome to <span class="shadow">Ireland</span></h2>
	</hgroup>
   
</header>
        <section >
        
        <hgroup id="tabs">
        	 <div>
           	<ul>
                	<li id="page1"><a href="#">Home</a></li>
                    <li id="page2"><a href="#" >Information</a></li>
                    <li id="page3"><a href="files/latest.html" onclick="grabFile(this.href); return false;">Latest</a></li>
                </ul>
           	
           </div>
        </hgroup>
        <hgroup id="home">
            
                <canvas id="canvas" width="400" height="300">
                </canvas>
        	<article id="introduce" >
                <h1 class="intro1">Ireland</h1>
                <p class="intro1">
                Ireland has always been considered a land of mystical and often magical happenings. It is a country steeped in myths and legends that live in harmony beside the modern world of today. Most travelers describe Ireland as a stunning land with unsurpassed beauty and one which possesses a history that goes back so far only the fairy folk remember its beginnings.
                </p>
                
                <h1 class="intro2">Blarney Castle</h1>
                <p class="intro2">
                Spend a leisurely three days exploring Blarney Castle, Killarney, the Ring of Kerry and Dingle Peninsula on a rail and coach tour that shows you some of the best of Ireland.
                </p>
                
                <h1 class="intro3">Connemara</h1>
                <p class="intro3">
                Leave Dublin for the day and take a coach and rail tour to Connemara and Galway. You'll travel through breathtaking Irish scenery, pass by stunning Kylemore Abbey and tour the lovely lakes, tains, stone walls and thatched cottages of Connemara. 							
                </p>
               
                <h1 class="intro4">Giant's Causeway</h1>
                <p class="intro4">
                With all your travel arrangements organized for you, you'll see Belfast and visit the famous coastal landscape known as the Giant's Causeway, with its stunning basalt columns. 
                </p>
                
                <h1 class="intro5">Dingle Peninsula</h1>
                <p class="intro5">
                The Dingle Peninsula has the most rugged Atlantic coastline in Kerry and is scattered with forts and pre-historic huts such as the Gallarus Oratory.
                </p>
                
                <div>
                <ul>
                <li><a href="#"><img id="i1" src="images/frameselector_active.png"></a></li>
                <li><a href="#"><img id="i2" src="images/frameselector.png"></a></li>
                <li><a href="#"><img id="i3" src="images/frameselector.png"></a></li>
                <li><a href="#"><img id="i4" src="images/frameselector.png"></a></li>
                <li><a href="#"><img id="i5" src="images/frameselector.png"></a></li>
                <li><a href="#"><img id="i6" src="images/pause.png"></a></li>
                </ul>
          		</div>
            </article>
                
                <img class="intro1" src="images/sights/ireland.jpg" id="homeImg"/>
                <img class="intro2" src="images/sights/Blarney.jpg" id="homeImg"/>
                <img class="intro3" src="images/sights/Connemara.jpg" id="homeImg"/>
                <img class="intro4" src="images/sights/gaint's causeway.jpg" id="homeImg"/>
                <img class="intro5" src="images/sights/dingle.jpg" id="homeImg"/>
              
        </hgroup>
        <hgroup id ="info">
        	
        </hgroup>
         <hgroup id="images">
            
        </hgroup>
          
           
      </section >
<footer>2012, Created by Wei Tao </footer>
	

</body>
</html>

/*
 * 
 * Ajax Application
 * 
 * */

function getHTTPObject() {
  var xhr = false;
  if (window.XMLHttpRequest) {
    xhr = new XMLHttpRequest();
  } else if (window.ActiveXObject) {
    try {
      xhr = new ActiveXObject("Msxml2.XMLHTTP");
    } catch(e) {
      try {
        xhr = new ActiveXObject("Microsoft.XMLHTTP");
      } catch(e) {
        xhr = false;
      }
    }
  }
  return xhr;
}

/*
 * get client information
 * user php file management/business/client.php
 * return the page client.php into home.php page where div's id="clientInfo".
 * */
function getClient()
{
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	    	displayClient(request);
	    };
	    
	    request.open("POST", "client.php", true);
	    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    
	    request.send(null);
	  }
}
	 
function displayClient(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("clientInfo");
		      msg.innerHTML = request.responseText;
		     /*we should load the function buttonStyle() after loading the area id="admin"*/
		      buttonStyle();
		    }
		  }
}

/*
 * goto add client page
 * user php file management/business/addCliPage.php
 * return the page addCliPage.php into home.php page where div's id="clientAdd".
 * */
function toAddCliPage()
{
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	    	displayAddCliPage(request);
	    };
	    
	    request.open("POST", "addCliPage.php", true);
	    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    
	    request.send(null);
	  }
}
	 
function displayAddCliPage(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("clientAdd");
		      msg.innerHTML = request.responseText;
		     /*we should load the function buttonStyle() after loading the area id="admin"*/
		      buttonStyle();
		    }
		  }
}
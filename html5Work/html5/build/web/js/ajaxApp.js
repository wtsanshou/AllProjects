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



//get images.html
function grabFile(file) {
  var request = getHTTPObject();
  if (request) {
    request.onreadystatechange = function() {
      parseResponse(request);
    };
    request.open("GET", file, true);
    request.send(null);
  }
}

function parseResponse(request) {
  if (request.readyState == 4) {
    if (request.status == 200 || request.status == 304) {
      var details = document.getElementById("images");
      details.innerHTML = request.responseText;
      $('#myRoundabout').roundabout({
		 shape: 'figure8',
		 minOpacity: 1
	});
    }
  }
}
//get products.jsp
function ajaxFunction() {
  var request = getHTTPObject();
  if (request) {
    request.onreadystatechange = function() {
      displayResponse(request);
    };
    
    request.open("POST", "files/products.jsp", true);
    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    
    request.send(null);
  }
}


function displayResponse(request) {
  if (request.readyState == 4) {
    if (request.status == 200 || request.status == 304) {
        
        var msg = document.getElementById("info");
      msg.innerHTML = request.responseText;
      initialize();
      infoStyle();
    }
  }
}

//get details.jsp
function getDetails(name) {
  var request = getHTTPObject();
  if (request) {
    request.onreadystatechange = function() {
      getResponse(request);
    };
    
    request.open("POST", "files/details.jsp", true);
    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    
    request.send("name="+name);
  }
}


function getResponse(request) {
  if (request.readyState == 4) {
    if (request.status == 200 || request.status == 304) {
        
        var msg = document.getElementById("popupContact");
      msg.innerHTML = request.responseText;
      popupStyle();
      
    }
  }
}
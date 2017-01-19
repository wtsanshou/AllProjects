


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
 * Clear Session
 * user php file management/Admin/clearSession.php
 * 
 * */

/*public function clearSession()
{
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	      parseResponse(request);
	    };
	    request.open("GET", "clearSession", true);
	    request.send(null);
	  }
	}

	function parseResponse(request) {
	  if (request.readyState == 4) {
	    if (request.status == 200 || request.status == 304) {
	      var details = document.getElementById("admin");
	      details.innerHTML = request.responseText;
	      alert("get it");
	    }
	  }
	}*/

/*
 * get admin user information
 * user php file management/Admin/admin.php
 * return the page admin.php into home.php page where div's id="admin".
 * */
function getAdmin()
{
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	    	displayAdmin(request);
	    };
	    
	    request.open("POST", "admin.php", true);
	    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    
	    request.send(null);
	  }
}
	 
function displayAdmin(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("admin");
		      msg.innerHTML = request.responseText;
		     /*we should load the function buttonStyle() after loading the area id="admin"*/
		      buttonStyle();
		    }
		  }
}

/*
 * get manager user information
 * user php file management/Admin/manager.php
 * return the page manager.php into home.php page where div's id="manager".
 * */
function getManager()
{
	
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	    	displayManager(request);
	    };
	    
	    request.open("POST", "manager.php", true);
	    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    
	    request.send(null);
	  }
}
	 
function displayManager(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("manager");
		      msg.innerHTML = request.responseText;
		     /*we should load the function buttonStyle() after loading the area id="admin"*/
		      buttonStyle();
		    }
		  }
}

/*
 * get manager user information
 * user php file management/Admin/manager.php
 * return the page manager.php into home.php page where div's id="manager".
 * */
function getStaff()
{
	
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	    	displayStaff(request);
	    };
	    
	    request.open("POST", "staff.php", true);
	    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    
	    request.send(null);
	  }
}
	 
function displayStaff(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("staff");
		      msg.innerHTML = request.responseText;
		     /*we should load the function buttonStyle() after loading the area id="admin"*/
		      buttonStyle();
		    }
		  }
}

/*
 * go into page addUser.php
 * user php file management/Admin/addUser.php
 * return the page addUser.php into home.php page where div's id="addUser".
 * */
function toAddUser()
{
	
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	    	displayToAddUser(request);
	    };
	    
	    request.open("POST", "addUserPage.php", true);
	    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    
	    request.send(null);
	  }
}
	 
function displayToAddUser(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("addUser");
		      msg.innerHTML = request.responseText;
		      
		      buttonStyle();
		    }
		  }
}

/*
 * use page addUser.php to add a user.
 * user php file management/Admin/addUser.php
 * 
 * */
function addUser(user_name,password,user_type,remark)
{
	
	var request = getHTTPObject();
	  if (request) {
	    request.onreadystatechange = function() {
	    	displayAddUser(request);
	    };
	    
	    request.open("POST", "addUser.php", true);
	    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
	    
	    var postStr = "user_name="+ user_name +"&password="+ password +"&user_type="+ user_type+"&remark="+ remark;
	    request.send(postStr);
	  }
}
	 
function displayAddUser(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("addState");
		        msg.innerHTML = request.responseText;
		     
		    }
	  }
}

/*
 * use page modUserPage.php to show the modify page.
 * user php file management/Admin/modUserPage.php
 * 
 * */
function modUserPage(user_name)
{
	var request = getHTTPObject();
	if (request) {
		request.onreadystatechange = function() {
			displayModUserPage(request);
		};
    
    request.open("POST", "modUserPage.php", true);
    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    
    var postStr = "user_name="+ user_name;
    request.send(postStr);
  }
}
 
function displayModUserPage(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("modUser");
		        msg.innerHTML = request.responseText;
		     
		        buttonStyle();
		    }
	  }
}

/*
 * use page modUserPage.php to delete a user.
 * user php file management/Admin/modUserPage.php
 * 
 * */
function modUser(user_name,password,user_type,remark)
{
	var request = getHTTPObject();
	if (request) {
		request.onreadystatechange = function() {
			displayModUser(request);
		};
    
    request.open("POST", "modUser.php", true);
    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    
    var postStr = "user_name="+ user_name +"&password="+ password +"&user_type="+ user_type+"&remark="+ remark;
    request.send(postStr);
  }
}
 
function displayModUser(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("modState");
		        msg.innerHTML = request.responseText;
		     
		    }
	  }
}


/*
 * use page delUser.php to delete a user.
 * user php file management/Admin/delUser.php
 * 
 * */
function delUser(user_name)
{
	var request = getHTTPObject();
	if (request) {
    request.onreadystatechange = function() {
    	displayDelUser(request);
    };
    
    request.open("POST", "delUser.php", true);
    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    
    var postStr = "user_name="+ user_name;
    request.send(postStr);
  }
}
 
function displayDelUser(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("delResult");
		        msg.innerHTML = request.responseText;
		     
		    }
	  }
}

function viewLog()
{
	var request = getHTTPObject();
	if (request) {
    request.onreadystatechange = function() {
    	displayViewLog(request);
    };
    
    request.open("POST", "log.php", true);
    request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    
    
    request.send(null);
  }
}
 
function displayViewLog(request) 
{
	  if (request.readyState == 4) {
		    if (request.status == 200 || request.status == 304) {
		        
		        var msg = document.getElementById("modLog");
		        msg.innerHTML = request.responseText;
		     
		    }
	  }
}

//get images.html
/*function grabFile() {
	alert("55555555");
	var request = getHTTPObject();
  if (request) {
    request.onreadystatechange = function() {
      parseResponse(request);
    };
    request.open("GET", "test.html", true);
    request.send(null);
  }
}

function parseResponse(request) {
  if (request.readyState == 4) {
    if (request.status == 200 || request.status == 304) {
      var details = document.getElementById("admin");
      details.innerHTML = request.responseText;
      
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
}*/
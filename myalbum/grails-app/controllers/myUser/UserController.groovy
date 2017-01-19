package myUser

import login.BaseController;
import ourAlbum.Picture

class UserController extends BaseController{

	def beforeInterceptor =
	[action:this.&auth,except:['login', 'logout','register']]
	
    def index = {
			
		 }
	/**
	* User Register
	* **/
	def register = {
		if (request.getMethod().equals("GET")) {
		println 'GET'
		}
		else {
			println 'POST'
			println params
			User user = new User(params)
			def male = "images/system/Male.png"
			def female = "images/system/Female.png"
			if(user.Sex =="Male")
			{
				user.img = male
			}
			else
			{
				user.img = female	
			}
			user.email = ""
			user.interest = ""
			user.grade = 0
			user.empirical_value = 0
			if (user.save()) {
				println 'saved'
				session.user = user;
				redirect(action:home,params:[id:user.id])
			}
			else { 
				println 'Not saved'
				redirect(uri:"/index.gsp")
			}
		}
	}
	/**
	* User log in
	* **/
	def login = {
		if (request.getMethod().equals("GET")) {
			println 'GET'
		}
		else{
			User user = User.findByUsernameAndPassword(params.username,params.password);
			if(user){
				
				user.empirical_value +=5
				if(user.empirical_value > 100)
				{
					user.grade += 1
					user.empirical_value = 0	
				}
				user.save()
				session.user = user; 
				redirect(action:home,params:[id:user.id])

			}
			else{
				println 'not login'
				redirect(uri:"/index.gsp")
			}
		}
	}
	/**
	* User log out
	* **/
	def logout = {
			println 'Logout'
			session.user = null;
			redirect(uri:"/index.gsp")
	}
	
	
	/**
	* Retrun home page
	* **/
	def home = {
		println params
		def picture = Picture.list(sort:"pic_date", order:"desc")
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		//def userlist = picture.album.user
		return [user:User.get(params.id),picture:picture,topPicture:topPicture]
	}
	
	/**
	* Change password
	* **/
	
	def change = {
		println params
		User user = User.get(params.user_id)
		if(params.oldPassword == user.password)
		{
			user.password = params.password
		}
		if(user.save())
		{
			println "change success..."	
			redirect(action:home,params:[id:user.id])
		}
		else
		{
			println "change fail..."
		}
	}
	/**
	* Update user information
	* **/
	
	def updateInfo =
	{
		println params
		User user = User.get(params.user_id)
		def f =request.getFile('upload')
		def fileName=f.getOriginalFilename()
		def path = "images/"+user.username +"/you/"+fileName
		def url= "web-app/"+path
		
		
		if(!f.empty && params.password == user.password)
		{
			
			def file=new File(url)
			file.mkdirs()
			f.transferTo(file)
			user.img = path
			user.email = params.email
			user.interest = params.interest
			user.sex = params.sex
			
			if(user.save())
			{
				println 'update success...'	
				redirect(action:home,params:[id:user.id])
			}
			else
			{
				println "update fail..."
			}
		}
	}
	
	def testAJAX ={
		println params
		render "sddfds =========================================fdfs"
	}
	
}

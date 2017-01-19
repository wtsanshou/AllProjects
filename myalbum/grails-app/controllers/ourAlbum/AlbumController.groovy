package ourAlbum

import bussiness.Search
import login.BaseController
import myUser.User

class AlbumController extends BaseController{

	def beforeInterceptor =
	[action:this.&auth,except:['login', 'logout','register']]
	
    def index = { }
	
/**
 * Add albums
 * **/
	def addAlbum = {
			println params
			Album album = new Album()
			Date date = new Date()
			album.name = params.album_name  
			album.album_date = date
			album.picture_number = 0
			println params.user_id
			User user_fresh = User.findById(params.user_id).addToAlbums(album)
			
			if(album.save()&&user_fresh.save())
			{
				println 'album is saved'	
				redirect(action:showAlbum,params:[id:params.user_id])
			}
			else{
				println 'album not be saved'
				
				redirect(action:showAlbum,params:[id:params.user_id])
			}
		
	}
	def showAlbum = 
	{
		println params
		
		User user = User.get(params.id)
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		
		def albumList = user.albums
		def pictureList = albumList.pictures
		
		return [user:user,albumList:albumList,topPicture:topPicture]
	}
	
	/**
	 * Upload Picture
	 * **/
	def uploadPicture =
	{
		println params
		def f =request.getFile('upload')
		def fileName=f.getOriginalFilename()
		User user = User.get(params.user_id)
		Album album = Album.get(params.album_id)
		def path = "images/"+user.username +"/"+album.name +"/"
		def filePath= path +fileName
		def url= "web-app/"+filePath
		println url
		
		if(!f.empty&&Picture.findByPath(filePath)==null){
			def file=new File(url)
			file.mkdirs()
			f.transferTo(file) 
			Picture picture = new Picture()
			Date date = new Date()
			picture.name = params.picture_name
			picture.pic_date = date
			picture.path = filePath
			picture.prefer_value = 0
			Album album_refresh = album.addToPictures(picture)
			album_refresh.picture_number++
			println album_refresh
			user.empirical_value +=10
			if(user.empirical_value > 100)
			{
				user.empirical_value = 0		
				user.grade +=1
			}
			if(picture.save() && album_refresh.save() && user.save() )
			{
				
				println 'picture is saved!'
				
				def pictureList = album_refresh.pictures
				def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
				[user:user,album:album_refresh,picture_list:pictureList,topPicture:topPicture]
			}
			else
			{
				println "picture is not saved!"
				redirect(action:showAddPicture,params:[id:params.album_id])
			}
		}
		else if(Picture.findByPath(url)!=null)
		{
			println "picture has already existed!"	
			redirect(action:showAddPicture,params:[id:params.album_id])
		}
		else
		{
			println "picture has some error!"
			redirect(action:showAddPicture,params:[id:params.album_id])
		}
	}
	
	def showAddPicture =
	{
		println params
		User user = User.get(session.user.id)
		Album album = Album.get(params.id)
		def pictureList = album.pictures
		println 'dsfs-----------------------------------'
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		println album
		[user:user,album:album,picture_list:pictureList,topPicture:topPicture]
	}
	
	/**
	 * Search pictures
	 * **/
	
	def showSearch ={
		println params
		User user = User.get(params.id)
		def pictureList = Picture.list(max:10)
		def albumList = pictureList.album
		def userList = albumList.user
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		[picture_list:pictureList,album_list:albumList,user_list:userList,user:user,topPicture:topPicture]
	}
	
	def showResult =
	{
		println params
		def pic = params.search
		println pic
		User user = User.get(params.user_id)
		//println user.img
		def seek = new Search()
		def picture_list = Picture.getAll()
		def album_list = Album.getAll()
		println picture_list.name
		
		def pictureList = seek.result(pic,picture_list)
		
		println pictureList.pic_date
		
		def albumList = pictureList.album
		def userList = albumList.user
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		println topPicture
		println userList
		return [picture_list:pictureList,album_list:albumList,user_list:userList,user:user, topPicture:topPicture]
	}
//the same function		
//		def userList = []
//		def albumList = []
//		pictureList.each{Picture p ->
//			albumList.add(p.album)
//		}
//		println albumList  
//		
//		albumList.each{Album a ->
//			userList.add(a.user)
//		}
		
	
	/**
	* Delete pictures
	* **/
   def delete = {
	   println params
	   Picture picture = Picture.findByName(params.id)
	   picture.delete()
   }
   /**
   * Delete album
   * **/
   def deleteAlbum ={
	   println params
	   Album album = Album.findByName(params.id)
	   album.delete()
	   redirect(action:showAlbum,params:[id:session.user.id])
   }
}

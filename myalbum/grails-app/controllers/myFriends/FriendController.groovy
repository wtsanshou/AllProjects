package myFriends

import bussiness.saveBestPicture
import login.BaseController;
import myUser.User
import ourAlbum.Album
import ourAlbum.BestPicture
import ourAlbum.Picture
import ourRemark.Remark

class FriendController extends BaseController{

	def beforeInterceptor =
	[action:this.&auth,except:['login', 'logout','register']]
	
    def index = { }
	
	def friendPicture ={
		println params
		def picture = Picture.findByName(params.id)
		def album = picture.album
		def pictureList = album.pictures
		def user = album.user
		def albumList = user.albums
		println user
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		[user:user,picture_list:pictureList,topPicture:topPicture,album_list:albumList]
	}
	
	def comment={
		println params
		
		Picture picture = Picture.findByName(params.pname)
		picture.prefer_value += params.preferValue.toInteger()
		println picture.prefer_value
		Remark remark = new Remark()
		Date date = new Date()
		remark.content = params.comment
		remark.name = session.user.username
		remark.remark_date = date
		
		
		
		picture.addToRemarks(remark)
		if(picture.save()&&remark.save())
		{
			def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
			redirect (action:friendPicture,id:params.pname,topPicture:topPicture)
		}
	}
	
	def addFriend ={
		println params
		User user = User.get(session.user.id)
		User friend = User.get(params.id)
		user.addToFriends(friend)
		println 'have a friend'
	}
	
	def seeFriend ={
		println params
		User user = User.findByUsername(params.id)
		def albumList = user.albums
		def pictures = user.albums.pictures
		def pictureList
		pictures.each {p->
			pictureList = p
			 }
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		[user:user,picture_list:pictureList,topPicture:topPicture,album_list:pictureList]
		
	}
	
	def showFriendPicture ={
		println params
		Album album = Album.get(params.id)
		println album
		def pictureList = album.pictures
		println pictureList
		User user = album.user
		def album_list = user.albums
		def topPicture = Picture.list(max:10,srot:"prefer_value",order:"desc")
		[user:user,picture_list:pictureList,topPicture:topPicture,album_list:album_list]
		
		//		def pictureList = album.pictures.name
		//		String pName = ""
		//		pictureList.each{p->
		//			pName = p
		//		}
		//
		//		println pName
		//		redirect(action:"friendPicture",params:[id:pName])
	}
	
	

}

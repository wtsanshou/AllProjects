package myUser
import ourAlbum.*
class User {

	String username
	String password
	String Sex
	String interest
	String img
	String email
	
	int grade
	int empirical_value
	
	static hasMany = [friends : User, albums : Album]
	
	static constraints = {
		email(email:true)
	}
	
	public String toString()
	{
		String str = "username:"+ username + "<br/>"		
		return str
	}
	 
	static mapping ={
		albums (sort:"album_date",order:"desc")
//		sort title:
		//order:"desc"
	}
	
}

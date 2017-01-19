package ourAlbum


import myUser.*




class Album {

	String name
	Date album_date
	Integer picture_number
	
	static belongsTo = [user:User]
	
	static hasMany = [pictures : Picture]
	
	static mapping ={
		sort album_date:"desc"
	}
	
    static constraints = {
    }
	
	public String toString()
	{
		return name + " : " + album_date.toString()	+ "<br/>"	
	}
}

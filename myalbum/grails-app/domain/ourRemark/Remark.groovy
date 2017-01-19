package ourRemark







import ourAlbum.Picture

class Remark {

	String name
	String content
	Date remark_date
	
	static belongsTo = [Picture]
	
    static constraints = {
    }
}

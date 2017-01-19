package ourAlbum







import ourRemark.Remark

class Picture {

	String name
	String path
	Date pic_date
	int prefer_value
	
	static belongsTo = [album:Album]
	
	static hasMany = [remarks: Remark]  
	  
	static mapping ={
		sort prefer_value:"desc"
	}
	
	public String toString()
	{
		return	 path	
	}
	
    static constraints = {
    }
}

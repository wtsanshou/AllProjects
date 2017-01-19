package bussiness
import java.util.List
class Search {
	public def result(def picture,def picture_list)
	{
		def found = [];
		
		picture_list.each { g ->
			
			if(g.name == picture)
			{
				println g
				found.add(g)
				println "here"
			}
		}
		
		return found
	}
}

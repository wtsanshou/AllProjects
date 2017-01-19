package bussiness

import ourAlbum.BestPicture



class saveBestPicture {
	def pictureList
 	public saveBestPicture(def pictureList)
	 {
		 this.pictureList = pictureList
	 }
	 
	 public boolean saveBest()
	 {
		 boolean success = true
		 println pictureList.name
		 
		 BestPicture b1 = new BestPicture()
		 BestPicture b2 = new BestPicture()
		 BestPicture b3 = new BestPicture()
		 BestPicture b4 = new BestPicture()
		 BestPicture b5 = new BestPicture()
		 BestPicture b6 = new BestPicture()
		 BestPicture b7 = new BestPicture()
		 BestPicture b8 = new BestPicture()
		 BestPicture b9 = new BestPicture()
		 def BName = [b1,b2,b3,b4,b5,b6,b7,b8,b9]
		 BName.name = pictureList.name
		 BName.path = pictureList.path
		 BName.prefer_value = pictureList.p.prefer_value
		 BName.author = pictureList.album.user.username
		 BName.isNewBest = "new"
		 BName.save()
//		 int i = 0
//		 pictureList.list { p ->
////			 BName[i].name = p.name
//			 println p.name
////			 BName[i].author = p.album.user.username
//			 println p.album.user.username
////			 BName[i].path = p.path
//			 println p.path
////			 BName[i].prefer_value = p.prefer_value
//			 println p.prefer_value
////			 BName[i].isNewBest = "new"
////			 try{
////				 if(BName[i].save())
////				 println 'good-----------------'
////			 }catch(Exception e)
////			 {
////				 success = false
////			 }
////			 i++
//		 }
		 
		 return success
	 }
	 
	 public boolean deleteOld()
	 {
		 boolean success = true
		 def oldBests = BestPicture.findAllByIsNewBest("new")
		 oldBests.each { o->
			 try{
				 o.delete()
			 }catch(Exception e){
			 success = false
			 }
		 }
		 return success
	 }
	 
	
	 
	 public void QuickSort(def a,int left,int right)
	 {
		 int mid;
		 if(left<right)
		 {
			 mid=Partition(a,left,right);
			 QuickSort(a,left,mid-1);
			 QuickSort(a,mid+1,right);
		 }
	 }
	 
	 public int Partition(def a,int first,int last)
	 {
		 int x = a[last]
		 int j = first - 1
		 int key
		 for(int i=first;i<last-1;i++)
		 {
		 	if(a[i]>x)
			 {
				 j++
				 key = a[i]
				 a[i] = a[j]
				 a[j] = key
			 }
		 }
		 key=a[j+1];
		 a[j+1]=a[last];
		 a[last]=key;
		 return j+1;
	 }
	 
}

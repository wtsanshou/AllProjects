
public class Management {

	private IPAddress ipAddr;
	private Media media;
	public Management(){
		
	}
	public IPAddress getIpAddr() {
		return ipAddr;
	}
	public int setIpAddr(int port, String ip) {
		IPAddress ipa = new IPAddress();
		
		ipa.setIp(ip);
		this.ipAddr = ipa;
		return ipa.setPort(port);
	}
	public Media getMedia() {
		return media;
	}
	public void setMedia(String savePath,String name) {
		Media med = new Media();
		med.setSavePath(savePath);
		med.setName(name);
		this.media = med;
		
	}
	
	
	
	
//	File file=new File("D:\\"); 
//	String test[]; 
//	test=file.list(); 
//	for(int i=0;i<test.length;i++) 
//	{ 
//	System.out.println(test[i]); 
//	}
}

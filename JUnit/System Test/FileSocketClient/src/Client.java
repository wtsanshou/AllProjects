
import java.util.Scanner;

public class Client {

	/**
	 * @param args
	 */

	Media media;
	IPAddress ipa;
	SocketServer ss = null;
	Management m = null;
	
	public Client(){
		m = new Management();
    	
	}
	
	public void setIPAddr(int port, String ip){
//		System.out.print(m.setIpAddr(port, ip));
		switch(m.setIpAddr(port, ip)){
			case 2:
				System.out.println("Port can't less than 0");
				break;
			case 3:
				System.out.println("Port can't in range from 0 to 1023");
				break;
			case 4:
				System.out.println("Port can't in range from 49152 to 65535");
				break;
			case 5:
				System.out.println("Port can't more than 65535");
				break;
			case 1:
				System.out.println("Your Port and IP are: " +port+" : " + ip);
				break;
			default:
				System.out.println("Port error");
				break;
		}

	}
	
	public void SetMedia(String savePath,String fileName){
		m.setMedia(savePath, fileName);
		System.out.println("Your save path is: "+savePath+fileName);
	}
	
	public void Connect(int port, String ip){
		
		ss = new SocketServer();
		if(ss.connectServer(port, ip)){
			
    		System.out.println("Connect success...");
    	}else{
    		System.out.println("Connect fail...");

    	}
		
	}
	
	public Boolean GetFile(String savePath,String fileName){
		Boolean result = false;
		if(ss.acceptFile(savePath, fileName)){
			result = true;
			System.out.println("Accept success...");
		}else{
			System.out.println("Accept fail...");
		}
		return result;
	}
	
	public void DisConnect(){
		
		if(ss.disconnectServer())
		{
			
			System.out.println("disconnect success...");
//			System.exit(0);
		}else{
			System.out.println("disconnect fail...");
		}
		
	}
	
	
	
	
    public static void main(String[] args) {
//    	Client c = new Client();
//    	c.m.setIpAddr(-1, "localhost");
//    	c.setIPAddr(8889, "localhost");
//    	c.ipa = c.m.getIpAddr();
//    	c.SetMedia("E:\\ireland\\File\\", "pp.mp4");
//    	c.media = c.m.getMedia();
//    	
//    	c.Connect(c.ipa.getPort(),c.ipa.getIp());
//    	
//    	System.out.println("Welcom into File Transfer Client\n");
//    	System.out.println("Please select servive:\n");
//    	System.out.println("1.Start to get file\n");
//    	System.out.println("2.disconnect\n");
//    	String say = "";
//    	Scanner sc = new Scanner(System.in);
//    	say = sc.next();
//    	int key = Integer.parseInt(say);
//    	switch(key)
//    	{
//    	case 1:
//    		c.GetFile(c.media.getSavePath(), c.media.getName());
//    		say = sc.next();
//        	key = Integer.parseInt(say);
//    		break;
//    	case 2:
//    		c.DisConnect();
//    		break;
//    	
//    	default: System.out.println("Error input!");
//    		break;
//    	}
	}
}

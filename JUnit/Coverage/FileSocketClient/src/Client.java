
import java.util.Scanner;

public class Client {

	/**
	 * @param args
	 */
	static String ip = "localhost";
	static int port = 8888;
	static String savePath ="E:\\ireland\\";
	static String fileName = "Gee.mp4";
	
	public Client(){
		Management m = new Management();
    	SocketServer ss = new SocketServer();
    	
    	m.setIpAddr(8888, "localhost");
    	m.setMedia("E:\\ireland\\File\\", "hi.mp4");
    	
    	IPAddress ipa = m.getIpAddr();
    	ip = ipa.getIp();
    	port = ipa.getPort();
    	
    	Media med = m.getMedia();
    	savePath = med.getSavePath();
    	fileName = med.getName();
    	

    	if(ss.connectServer(port, ip)){
    		System.out.println("Connect success...\n");
    	}else{
    		System.out.println("Connect fail...\n");
    		System.exit(0);
    	}
    	
    	System.out.println("Welcom into File Transfer Client\n");
    	System.out.println("Please select servive:\n");
    	System.out.println("1.Start to get file\n");
    	System.out.println("2.disconnect\n");
    	String say = "";
    	Scanner sc = new Scanner(System.in);
    	say = sc.next();
    	int key = Integer.parseInt(say);
    	switch(key)
    	{
    	case 1:
    		if(ss.acceptFile(savePath, fileName)){
    			System.out.println("Accept success...");
    		}else{
    			System.out.println("Accept fail...");
    		}
    		say = sc.next();
        	key = Integer.parseInt(say);
    		break;
    	case 2:
    		if(ss.disconnectServer())
    		{
    			System.out.println("disconnect success...");
    			System.exit(0);
    		}else{
    			System.out.println("disconnect fail...");
    		}
    		break;
    	
    	default: System.out.println("Error input!");
    		break;
    	}
		
	}
	
    public static void main(String[] args) {

    	Client client = new Client();
	}
}


public class IPAddress {

	private int port;
	private String ip;
	
	public IPAddress(){
		
	}

	public int getPort() {
		return port;
	}

	public int setPort(int port) {
		if(port<0){
			
			System.out.println("Port can't less than 0");
			return 2;
		}else if(port>=0 && port<=1023){
			
			System.out.println("Port can't in range from 0 to 1023");
			return 3;
		}else if(port>=49152 && port<=65535){
			
			System.out.println("Port can't in range from 49152 to 65535");
			return 4;
		}else if (port>65535){
			
			System.out.println("Port can't more than 65535");
			return 5;
		}else {
			this.port = port;
			return 1;
		}
	}

	public String getIp() {
		return ip;
	}

	public void setIp(String ip) {
		this.ip = ip;
	}
	
}

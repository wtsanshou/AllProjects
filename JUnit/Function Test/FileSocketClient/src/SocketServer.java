import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.net.UnknownHostException;


public class SocketServer {
	
	Socket socket = null;
	DataInputStream fileIn = null;
	DataOutputStream fileOut = null;
	
	
    int bufferSize = 8192;
    byte[] buf = null;
    long passedlen = 0;
    long len=0;
	
	public SocketServer(){
		
	}
	
	public Boolean connectServer(int port,String ip){
		Boolean result = false;
		try {
			socket = new Socket(ip,port);
			DataOutputStream dos = new DataOutputStream(socket.getOutputStream());
	        dos.writeUTF("I am sanshou, please connect.....");
	        result = true;
		} catch (UnknownHostException e) {
			// TODO Auto-generated catch block
			System.out.println("The connection is fail....1");
		} catch (IOException e) {
			// TODO Auto-generated catch block
			System.out.println("The connection is fail....2");
		}
		return result;
	}
	public Boolean disconnectServer(){
		Boolean disResult = false;
		try {
			if(socket!= null){
				System.out.println("Socket is disconnected!!!");
				socket.close();
				disResult = true;
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			System.out.println("Socket has already been disconnected!!!");
		}
		return disResult;
	}
	
	public Boolean acceptFile(String savePath,String fileName){
		Boolean accResult = false;
		buf = new byte[bufferSize];
		try{  
	        fileIn = new DataInputStream(new BufferedInputStream(socket.getInputStream()));
	        
	        savePath += fileName;
	        
	        System.out.println("Name of the file:" + fileIn.readUTF() + "\n");
	        fileOut = new DataOutputStream(new BufferedOutputStream(new BufferedOutputStream(new FileOutputStream(savePath))));
	        len = fileIn.readLong();
	        
	        System.out.println("Lenght of the file:" + len + "\n");
	        System.out.println("Start accept....." + "\n");
	                
	        while (true) {
	            int read = 0;
	            if (fileIn != null) {
	                read = fileIn.read(buf);
	            }
	            passedlen += (long)read;
	            System.out.println(passedlen);
	            if (read == -1) {
	                break;
	            }
	            
	            System.out.println("achieve:" +  (passedlen * 100/ len) + "%\n");
	            fileOut.write(buf, 0, read);
	        }
	        
	        fileIn.close();
	        fileOut.close();
	        File file = new File(savePath);
	        
	        if(file.length()!=0){
	        	accResult = true;
	        }
	        System.out.println("Finish! the path:" + savePath + "\n");
	    } catch (IOException e) {
			// TODO Auto-generated catch block
	    	System.out.println("Accept fail...\n");
		}
	    return accResult;
	}
	
}

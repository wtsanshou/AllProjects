import java.io.BufferedInputStream;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.FileInputStream;

import java.io.File;

import java.io.FileNotFoundException;

import java.io.IOException;

import java.net.ServerSocket;
import java.net.Socket;


public class Server {
	public static void main(String arg[])
	{
		new Server().ServerListener();
	}
	int port = 8888;
	
	DataOutputStream fileout = null;
	DataInputStream filein = null;
	DataInputStream order = null;
	File file = null; 
	Socket socket = null;
	byte[] buf = null;
	
	public void ServerListener(){
		try {
			ServerSocket server = new ServerSocket(port);
			while(true)
			{
				System.out.println("Server is starting....");
				socket = server.accept();
				//System.out.println("User come in...");
				CreateThread create = new CreateThread(socket);
				create.start();
				
			}
		}catch (IOException e){
			e.printStackTrace();
		}
	}
	
	class CreateThread extends Thread 
	{
		Socket so = null;
		public CreateThread(Socket socket)
		{
			this.so = socket;
		}
	
	
		public void run()
		{
			String filePath = "F:\\KuGou\\song\\Gee.RM";
			
			try {
				file = new File(filePath);
				System.out.println(file.length());
				order = new DataInputStream(so.getInputStream());
				filein = new DataInputStream(new BufferedInputStream(new FileInputStream(filePath)));
				System.out.println(order.readUTF());
				
				fileout = new DataOutputStream(so.getOutputStream());
				fileout.writeUTF(file.getName());
				fileout.flush();
				fileout.writeLong(file.length());
				fileout.flush();
				
				buf = new byte[8192];
				
				while(true)
				{
					int read = 0;
					if(filein != null)
					{
						read = filein.read(buf);
					}
					if(read == -1)
					{
						break;
					}
					fileout.write(buf,0,read);
					fileout.flush();
				}
				
				fileout.close();
				filein.close();
				so.close();
				
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

}

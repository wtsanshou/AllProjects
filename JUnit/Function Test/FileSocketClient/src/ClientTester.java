import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import junit.framework.TestCase;


public class ClientTester extends TestCase{
	private Client c = new Client();
	
	public void test1(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(-1, "localhost");
		
		assertEquals("Port can't less than 0\r\n", result.toString());
	}
	public void test2(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(0, "localhost");
		
		assertEquals("Port can't in range from 0 to 1023\r\n", result.toString());
	}
	public void test3(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(1023, "localhost");
		
		assertEquals("Port can't in range from 0 to 1023\r\n", result.toString());
	}
	public void test4(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(1024, "localhost");
		
		assertEquals("Your Port and IP are: 1024 : localhost\r\n", result.toString());
	}
	public void test5(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(49151, "localhost");
		
		assertEquals("Your Port and IP are: 49151 : localhost\r\n", result.toString());
	}
	public void test6(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(49152, "localhost");
		
		assertEquals("Port can't in range from 49152 to 65535\r\n", result.toString());
	}
	public void test7(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(65535, "localhost");
		
		assertEquals("Port can't in range from 49152 to 65535\r\n", result.toString());
	}
	public void test8(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.setIPAddr(65536, "localhost");
	
		assertEquals("Port can't more than 65535\r\n", result.toString());
	}
	//SetMedia
	public void test9(){
	
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.SetMedia("E:\\ireland\\File\\","pp.mp4");
		
		assertEquals("Your save path is: E:\\ireland\\File\\pp.mp4\r\n", result.toString());
	}
	
	//Connect
	public void test10(){
		
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.Connect(8888, "localhost");
		
		assertEquals("Connect success...\r\n", result.toString());
	}
	public void test11(){
			
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.Connect(8899, "localhost");
		
		assertEquals("The connection is fail....2\r\nConnect fail...\r\n", result.toString());
	}
	public void test12(){
		
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.Connect(8888, "local");
		
		assertEquals("The connection is fail....1\r\nConnect fail...\r\n", result.toString());
	}
	
	//GetFile
	public void test13(){
		c.Connect(8888, "localhost");
		Boolean result = c.GetFile("E:\\ireland\\File\\", "hi.mp4");
		assertEquals("true",result.toString());	
	}
	public void test14(){
		c.Connect(8888, "localhost");
		Boolean result = c.GetFile("E:\\ireland\\File\\", "");
		assertEquals("false",result.toString());	
	}
	
	//
	public void test15(){
		c.Connect(8888, "localhost");
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.DisConnect();
		
		assertEquals("Socket is disconnected!!!\r\ndisconnect success...\r\n", result.toString());
	}
	public void test16(){
		c.Connect(8899, "localhost");
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.DisConnect();
		
		assertEquals("disconnect fail...\r\n", result.toString());
	}
}

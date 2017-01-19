import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import junit.framework.TestCase;


public class SystemTester extends TestCase {

	Client c = new Client();
	//IP address
	public void test1(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.setIPAddr(1024, "localhost");
		
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Your Port and IP are: 1024 : localhost";
		
		assertEquals(aTargetString, aResultFromScreen);
	}
	
	public void test2(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.setIPAddr(49151, "localhost");
		
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Your Port and IP are: 49151 : localhost";
		
		assertEquals(aTargetString, aResultFromScreen);
	}
	
	//SetMedia
	public void test3(){
	
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
			c.SetMedia("E:\\ireland\\File\\","pp.mp4");
		
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Your save path is: E:\\ireland\\File\\pp.mp4";
		
		assertEquals(aTargetString, aResultFromScreen);

	}
	
	//Connect
	public void test4(){
		
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.Connect(8888, "localhost");
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Connect success...";
		
		assertEquals(aTargetString, aResultFromScreen);

	}
	
	//GetFile
	public void test5(){
		c.Connect(8888, "localhost");
		Boolean result = c.GetFile("E:\\ireland\\File\\", "hi.mp4");
		assertEquals("true",result.toString());	
	}
	
	//-------non-function test
	public void test6(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.setIPAddr(-1, "localhost");
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Port can't less than 0";
		
		assertEquals(aTargetString, aResultFromScreen);

	}
	public void test7(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.setIPAddr(1023, "localhost");
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Port can't in range from 0 to 1023";
		
		assertEquals(aTargetString, aResultFromScreen);
		
	}
	public void test8(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.setIPAddr(49152, "localhost");
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Port can't in range from 49152 to 65535";
		
		assertEquals(aTargetString, aResultFromScreen);
		
	}
	public void test9(){
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.setIPAddr(65536, "localhost");
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "Port can't more than 65535";
		
		assertEquals(aTargetString, aResultFromScreen);
		
	}
	public void test10(){
		
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.Connect(8899, "localhost");
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "The connection is fail....2Connect fail...";
		
		assertEquals(aTargetString, aResultFromScreen);

	}
	public void test11(){
		
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.Connect(8888, "local");
		ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "The connection is fail....1Connect fail...";
		
		assertEquals(aTargetString, aResultFromScreen);

	}
	public void test12(){
		c.Connect(8888, "localhost");
		Boolean result = c.GetFile("E:\\ireland\\File\\", "");
		assertEquals("false",result.toString());	
	}
	public void test13(){
		c.Connect(8899, "localhost");
		ByteArrayOutputStream result = new ByteArrayOutputStream();
		PrintStream ps = new PrintStream(result);
		System.setOut(ps);
		
		c.DisConnect();
ByteArrayInputStream in = new ByteArrayInputStream(result.toByteArray());
		
		int c;
		String aResultFromScreen = "";
		while ((c = in.read()) != -1){
			if((char) c != '\r'){
				if((char) c != '\n'){
					aResultFromScreen += (char) c;
				}
			}
		}
		String aTargetString = "disconnect fail...";
		
		assertEquals(aTargetString, aResultFromScreen);
	
	}
}

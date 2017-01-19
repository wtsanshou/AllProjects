import junit.framework.TestCase;


public class SocketServerTester extends TestCase{
	
	private SocketServer ss = new SocketServer();
	
	
	public void testConnect1(){
		Boolean result = ss.connectServer(8888, "localhost");
		assertEquals("true",result.toString());
	}
	public void testConnect2(){
		Boolean result = ss.connectServer(8888, "ssss");
		assertEquals("false",result.toString());	
	}
	public void testConnect3(){
		Boolean result = ss.connectServer(8889, "localhost");
		assertEquals("false",result.toString());	
	}
	
	public void testDisconnect1(){
		ss.connectServer(8888, "localhost");
		Boolean result = ss.disconnectServer();
		assertEquals("true",result.toString());
	}
	public void testDisconnect2(){
		ss.connectServer(8899, "localhost");
		Boolean result = ss.disconnectServer();
		assertEquals("false",result.toString());	
	}
	
	public void testacceptFile1(){
		ss.connectServer(8888, "localhost");
		Boolean result = ss.acceptFile("E:\\ireland\\", "");
		assertEquals("false",result.toString());	
	}
	public void testacceptFile2(){
		ss.connectServer(8888, "localhost");
		Boolean result = ss.acceptFile("E:\\ireland\\File\\", "hi.mp4");
		assertEquals("true",result.toString());	
	}
	
	
	
}

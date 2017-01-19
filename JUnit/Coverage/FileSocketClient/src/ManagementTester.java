import junit.framework.TestCase;


public class ManagementTester extends TestCase{

	private Management mana = new Management();
	
	public void testIPAddrSetGet(){
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(7777,ipa.getPort());
		assertEquals("192.168.0.23",ipa.getIp());
	}
	
	public void testMediaSetGet(){
		mana.setMedia("E:\\ireland\\", "test.mp4");
		Media med = mana.getMedia();
		assertEquals("E:\\ireland\\",med.getSavePath());
		assertEquals("test.mp4",med.getName());
	}
	
	public void testIpSetGet()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		ipa.setIp("192.168.0.77");
		assertEquals("192.168.0.77",ipa.getIp());
	}
	
	
	public void testSavePathSetGet()
	{
		mana.setMedia("E:\\ireland\\", "test.mp4");
		Media med = mana.getMedia();
		med.setSavePath("E:\\ireland\\File\\");
		assertEquals("E:\\ireland\\File\\",med.getSavePath());
	}
	public void testFileNameSetGet(){
		mana.setMedia("E:\\ireland\\", "test.mp4");
		Media med = mana.getMedia();
		med.setName("testName.mp4");
		assertEquals("testName.mp4",med.getName());
	}
	
	public void testPortSetGet1()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(1,ipa.setPort(1024));
		assertEquals(1024,ipa.getPort());
	}
	public void testPortSetGet2()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(1,ipa.setPort(49151));
		assertEquals(49151,ipa.getPort());
	}
	public void testPortSetGet3()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(2,ipa.setPort(-1));
	}
	public void testPortSetGet4()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(3,ipa.setPort(0));
	}
	public void testPortSetGet5()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(3,ipa.setPort(1023));
	}
	public void testPortSetGet6()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(4,ipa.setPort(49152));
	}
	public void testPortSetGet7()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(4,ipa.setPort(65535));
	}
	public void testPortSetGet8()
	{
		mana.setIpAddr(7777, "192.168.0.23");
		IPAddress ipa = mana.getIpAddr();
		assertEquals(5,ipa.setPort(65536));
	}
	
	
}

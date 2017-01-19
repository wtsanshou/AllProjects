/*
  ���ڽ��ܷ��Ե� Remoting Client
*/

/*
  csc.exe /r:share.dll c:\RemotingChatRoom\Client\Receiver.cs
*/

using System;
using System.Collections;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

//Receiver �������Ҫ�̳� MarshalByRefObject,��������ڷ�����, Receiver ���� ҲҪΪ���ṩԶ�̷���
class Receiver : MarshalByRefObject
{
	ChatRoom x;

	public static void Main()
	{
		Receiver y = new Receiver();
		y.Run();
	}

	public void Run()
	{
		Console.WriteLine("Client Messages Receiver ... ,Press Enter key to exit.");
		BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
		provider.TypeFilterLevel = TypeFilterLevel.Full;
		IDictionary props = new Hashtable();
		//Receiver ���� ҲҪΪ���ṩԶ�̷��� ʹ�ö˿�
		props["port"] = 8180;
		TcpChannel tc = new TcpChannel(props, null, provider);
		ChannelServices.RegisterChannel(tc, false);

		x = (ChatRoom) Activator.GetObject(typeof(ChatRoom), "tcp://127.0.0.1:8080/ChatRoomURL");

		//ChatRoom �������ʱ������ abstract class

		//��Զ�̷������"ע��"���ػص����� this.Message_Receive1
		x.MessageReceive += new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		//Login Logoff �� MessageReceive ǩ��һ��
		x.Login += new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		x.Logoff += new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);



		Console.ReadLine(); //�˳��رս�����

		//ǧ��Ҫ���ǽ���ʱȡ��ί�й�ϵ
		x.MessageReceive -= new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		x.Login -= new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		x.Logoff -= new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
	}

	//������Ǳ���������Զ�̻ص��ĺ���
	public void Message_Receive1(string s)
	{
		Console.WriteLine(s); //�ڱ�����ʾ���յĹ㲥��Ϣ
	}

	public override object InitializeLifetimeService()
	{
		return null;
	}
}
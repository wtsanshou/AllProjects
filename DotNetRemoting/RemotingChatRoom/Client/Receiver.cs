/*
  用于接受发言的 Remoting Client
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

//Receiver 对象必须要继承 MarshalByRefObject,由于相对于服务器, Receiver 对象 也要为其提供远程服务。
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
		//Receiver 对象 也要为其提供远程服务 使用端口
		props["port"] = 8180;
		TcpChannel tc = new TcpChannel(props, null, provider);
		ChannelServices.RegisterChannel(tc, false);

		x = (ChatRoom) Activator.GetObject(typeof(ChatRoom), "tcp://127.0.0.1:8080/ChatRoomURL");

		//ChatRoom 代理类此时不能是 abstract class

		//向远程服务对象"注册"本地回调函数 this.Message_Receive1
		x.MessageReceive += new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		//Login Logoff 与 MessageReceive 签名一样
		x.Login += new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		x.Logoff += new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);



		Console.ReadLine(); //退出关闭接收器

		//千万不要忘记结束时取消委托关系
		x.MessageReceive -= new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		x.Login -= new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
		x.Logoff -= new ChatRoom.ChatRoomEventHandler(this.Message_Receive1);
	}

	//这个就是被服务器端远程回调的函数
	public void Message_Receive1(string s)
	{
		Console.WriteLine(s); //在本地显示接收的广播消息
	}

	public override object InitializeLifetimeService()
	{
		return null;
	}
}
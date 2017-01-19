/*
  用于发言的 Remoting Client
*/

/*
  csc.exe /r:share.dll c:\RemotingChatRoom\Client\Sender.cs
*/

using System;
using System.Collections;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

class Sender
{
	ChatRoom x;

	public static void Main(string[] Args)
	{
		Sender y = new Sender();
		y.Run();
	}

	string User;

	public void Run()
	{
		Console.WriteLine("Client Messages Sender ... ,Press 'q' to exit Chatting.");
		//  以配置方式获取远程服务对象实例
		//  RemotingConfiguration.Configure("c.config");
		//  x = new ChatRoom();

		//以编程方式获取远程服务对象实例
		x = (ChatRoom) Activator.GetObject(typeof (ChatRoom), "tcp://127.0.0.1:8080/ChatRoomURL");

		//先登录
		Console.WriteLine("make a name then Login Please:");
		User = Console.ReadLine();

		//调用此远程方法,通知服务器触发 Receiver 客户端 Login 事件,广播 "登录" 消息
		x.OnLogin(User);

		Console.WriteLine("welcome " + User + ",Send your Message Please:");

		string s; //存储键入的消息文字

		while ((s = Console.ReadLine()) != "q") //如果键入 q 退出循环
		{
			//调用此远程方法,通知服务器触发 Receiver 客户端 MessageReceive 事件,广播你所键入的消息
			x.OnMessageReceive(User + " say: " + s);
		}

		//调用此远程方法,通知服务器触发 Receiver 客户端 Logoff 事件,广播 "退出" 消息
		x.OnLogoff(User);
		Console.WriteLine("bye bye " + User);
	}
}

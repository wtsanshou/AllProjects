/*
  部署在服务器端的提供 Remoting 服务的类
*/

/*
  csc /t:library c:\RemotingChatRoom\Server\Share.cs
*/

using System;

//[Serializable]
public class ChatRoom : MarshalByRefObject
{
	//定义了 1 个名为 "ChatRoomEventHandler 的事件处理委托" 及其参数格式签名
	public delegate void ChatRoomEventHandler(string s);

	//定义了 3 个 "ChatRoomEventHandler 委托类型" 的事件及远程回调函数
	public event ChatRoomEventHandler MessageReceive; //消息接收事件
	public event ChatRoomEventHandler Login; //登录事件
	public event ChatRoomEventHandler Logoff; //退出事件

	// [System.Runtime.Remoting.Messaging.OneWay]
	public void OnMessageReceive(string Message)
	{
		if (MessageReceive != null)
		{
			//触发 Receiver 客户端 MessageReceive 事件,广播所有消息

			RaiseEvents(ref MessageReceive, ref Message);
			//MessageReceive(Message);
		}
		Console.WriteLine("Server: " + Message); //服务器消息监视
	}

	// [System.Runtime.Remoting.Messaging.OneWay]
	public void OnLogin(string User)
	{
		string s = "System say: " + User + " Login!";
		if (Login != null)
		{
			//触发 Receiver 客户端 Login 事件,广播 "登录" 消息
			RaiseEvents(ref Login, ref s);
			//Login("System say: " + User + " Login!");
		}
		Console.WriteLine("Server: " + s);
	}

	// [System.Runtime.Remoting.Messaging.OneWay]
	public void OnLogoff(string User)
	{
		string s = "System say: " + User + " Logoff!";
		if (Logoff != null)
		{
			//触发 Receiver 客户端 Logoff 事件,广播 "退出" 消息
			//Logoff("System say: " + User + " Logoff!");
			RaiseEvents(ref Logoff, ref s);
		}
		Console.WriteLine("Server: " + s);
	}

	public override object InitializeLifetimeService()
	{
		return null;
	}

	// 参阅 Ingo Rammer《Advanced .NET Remoting》
	public void RaiseEvents(ref ChatRoomEventHandler e, ref string Message)
	{
		ChatRoomEventHandler creh = null;
		int i = 1;
		Delegate[] D = e.GetInvocationList();
		foreach (Delegate d in D)
		{
			try
			{
				creh = (ChatRoomEventHandler) d;
				creh(Message);
			}
			catch
			{
				Message += "\n第 " + i.ToString() + " 个订阅者发生错误,系统取消其事件订阅!";
				e -= creh;
			}
			i ++;
		}
	}
}
/*
部署在 Remoting 客户端所需的远程对象的类的客户端本地代理类只要跟真正的
服务器端的远程对象的类的元数据结构一样即可
本地代理类的 dll 程序集文件名, 命名空间 类 方法 等签名要与服务器端一致
*/

/*
  csc /t:library c:\RemotingChatRoom\Client\Share.cs
*/

using System;

//[Serializable]
//部署在 Remoting 客户端所需的远程对象的类的客户端本地代理类
//如果是 abstract class 则不能 new 实例化
public class /* abstract */ ChatRoom : MarshalByRefObject
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
		throw new NotImplementedException();
	}

	// [System.Runtime.Remoting.Messaging.OneWay]
	public void OnLogin(string User)
	{
		throw new NotImplementedException();
	}

	// [System.Runtime.Remoting.Messaging.OneWay]
	public /* abstract */ void OnLogoff(string User)
	{
		throw new NotImplementedException();
	}

}
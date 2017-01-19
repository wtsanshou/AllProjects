/*
  csc.exe /t:library c:\RemotingChatRoom\Server\Receiver.cs
*/

using System;
using System.Runtime.Remoting;
//Receiver 对象必须要继承 MarshalByRefObject,由于相对于服务器, Receiver 对象 也要为其提供远程服务。
//仅仅是签名一致的代理类,是不是 abstarct class 无所谓
abstract class Receiver : MarshalByRefObject
{
	//这个就是被服务器端远程回调的函数
	public abstract void Message_Receive1(string s);
//	{
//		throw new NotImplementedException();
//	}
}
/*
  csc.exe /t:library c:\RemotingChatRoom\Server\Receiver.cs
*/

using System;
using System.Runtime.Remoting;
//Receiver �������Ҫ�̳� MarshalByRefObject,��������ڷ�����, Receiver ���� ҲҪΪ���ṩԶ�̷���
//������ǩ��һ�µĴ�����,�ǲ��� abstarct class ����ν
abstract class Receiver : MarshalByRefObject
{
	//������Ǳ���������Զ�̻ص��ĺ���
	public abstract void Message_Receive1(string s);
//	{
//		throw new NotImplementedException();
//	}
}
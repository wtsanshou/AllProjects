/*
  ���ڷ��Ե� Remoting Client
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
		//  �����÷�ʽ��ȡԶ�̷������ʵ��
		//  RemotingConfiguration.Configure("c.config");
		//  x = new ChatRoom();

		//�Ա�̷�ʽ��ȡԶ�̷������ʵ��
		x = (ChatRoom) Activator.GetObject(typeof (ChatRoom), "tcp://127.0.0.1:8080/ChatRoomURL");

		//�ȵ�¼
		Console.WriteLine("make a name then Login Please:");
		User = Console.ReadLine();

		//���ô�Զ�̷���,֪ͨ���������� Receiver �ͻ��� Login �¼�,�㲥 "��¼" ��Ϣ
		x.OnLogin(User);

		Console.WriteLine("welcome " + User + ",Send your Message Please:");

		string s; //�洢�������Ϣ����

		while ((s = Console.ReadLine()) != "q") //������� q �˳�ѭ��
		{
			//���ô�Զ�̷���,֪ͨ���������� Receiver �ͻ��� MessageReceive �¼�,�㲥�����������Ϣ
			x.OnMessageReceive(User + " say: " + s);
		}

		//���ô�Զ�̷���,֪ͨ���������� Receiver �ͻ��� Logoff �¼�,�㲥 "�˳�" ��Ϣ
		x.OnLogoff(User);
		Console.WriteLine("bye bye " + User);
	}
}

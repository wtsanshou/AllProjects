/*
  �����ڷ������˵��ṩ Remoting �������
*/

/*
  csc /t:library c:\RemotingChatRoom\Server\Share.cs
*/

using System;

//[Serializable]
public class ChatRoom : MarshalByRefObject
{
	//������ 1 ����Ϊ "ChatRoomEventHandler ���¼�����ί��" ���������ʽǩ��
	public delegate void ChatRoomEventHandler(string s);

	//������ 3 �� "ChatRoomEventHandler ί������" ���¼���Զ�̻ص�����
	public event ChatRoomEventHandler MessageReceive; //��Ϣ�����¼�
	public event ChatRoomEventHandler Login; //��¼�¼�
	public event ChatRoomEventHandler Logoff; //�˳��¼�

	// [System.Runtime.Remoting.Messaging.OneWay]
	public void OnMessageReceive(string Message)
	{
		if (MessageReceive != null)
		{
			//���� Receiver �ͻ��� MessageReceive �¼�,�㲥������Ϣ

			RaiseEvents(ref MessageReceive, ref Message);
			//MessageReceive(Message);
		}
		Console.WriteLine("Server: " + Message); //��������Ϣ����
	}

	// [System.Runtime.Remoting.Messaging.OneWay]
	public void OnLogin(string User)
	{
		string s = "System say: " + User + " Login!";
		if (Login != null)
		{
			//���� Receiver �ͻ��� Login �¼�,�㲥 "��¼" ��Ϣ
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
			//���� Receiver �ͻ��� Logoff �¼�,�㲥 "�˳�" ��Ϣ
			//Logoff("System say: " + User + " Logoff!");
			RaiseEvents(ref Logoff, ref s);
		}
		Console.WriteLine("Server: " + s);
	}

	public override object InitializeLifetimeService()
	{
		return null;
	}

	// ���� Ingo Rammer��Advanced .NET Remoting��
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
				Message += "\n�� " + i.ToString() + " �������߷�������,ϵͳȡ�����¼�����!";
				e -= creh;
			}
			i ++;
		}
	}
}
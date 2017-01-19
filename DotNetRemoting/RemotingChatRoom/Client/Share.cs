/*
������ Remoting �ͻ��������Զ�̶������Ŀͻ��˱��ش�����ֻҪ��������
�������˵�Զ�̶�������Ԫ���ݽṹһ������
���ش������ dll �����ļ���, �����ռ� �� ���� ��ǩ��Ҫ���������һ��
*/

/*
  csc /t:library c:\RemotingChatRoom\Client\Share.cs
*/

using System;

//[Serializable]
//������ Remoting �ͻ��������Զ�̶������Ŀͻ��˱��ش�����
//����� abstract class ���� new ʵ����
public class /* abstract */ ChatRoom : MarshalByRefObject
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
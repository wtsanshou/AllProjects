http://chs.gotdotnet.com/quickstart/howto/doc/Remoting/quickstart.aspx
����ʱ������Զ�̶���Ŀͻ������õ����ֲ�ͬ�ķ���Ϊ:
1.
����ͻ���ʱ,������������󲢽� EXE �� DLL ָ��Ϊ�Ա����������á�����ͻ��˺ͷ��������������ͬһ�ص㿪����,���ַ�����ǳ����á�
2.
�ӽӿ�����������������ʹ�ýӿڱ���ͻ��ˡ�����ͻ��˺ͷ��������������ͬһ�ص㿪��ʱ,���ַ�����ǳ����á��ɸ�����Ҫ���ӿڱ���Ϊ DLL �����͵��ͻ��˵ص㡣Ӧ������ѷ����Ľӿڽ��и��ġ��йظ�����Ϣ,��ο� COM ָ�ϡ�
3.
ʹ�� SOAPSUDS ���ߴ����еķ�����������ȡ�����Ԫ���ݡ�����ͻ��˺ͷ���������ڲ�ͬ�ĵص㿪������û�п��õĽӿ���,���ǳ����á��� SOAPSUDS ����ָ��Զ�� URI ��������Ԫ��������ΪԴ�� DLL����ע��,SOAPSUDS ���߽���ȡԪ����,������Զ�̶����Դ��

�������÷��� 2
.Net Remoting ʵ�ּ��׵Ŀ���̨������

���׳���ֱ��� �������˺Ϳͻ��˳������:
Server Side:
1.\RemotingChatRoom\Server\share.cs
   csc.exe /t:library c:\RemotingChatRoom\Server\share.cs
   ���ɵ� share.dll �����ڷ�������,�ṩ�� Client �� Remoting �������
2.\RemotingChatRoom\Server\Receiver.cs
   csc.exe /t:library c:\RemotingChatRoom\Server\Receiver.cs
   ���ɵ� Receiver.dll �����ڷ�������,��ʱ�� server �� Client λ�û�����ɫ:
   Client �� Receiver ����,ҲҪΪ server �ṩԶ�̷���,�����༴�ɡ�
3.\RemotingChatRoom\Server\Server.cs
   csc.exe c:\RemotingChatRoom\Server\Server.cs
   ���ɵ� Server.exe ���� Remoting Server ��������

Client Side:
1.\RemotingChatRoom\Client\share.cs
   csc.exe /t:library c:\RemotingChatRoom\Client\share.cs
   ���ɵ� share.dll �����ڿͻ���,
   �ó��򼯵� ChatRoom ���ֻ��Զ�̵ķ������˵���Ӧ���򼯵� ChatRoom ��Ĵ�����:
   �����ռ� �� ���� �Ƚ���ǩ��Ҫ���������һ�¼��ɡ�
   ��ȻҲ�ɽ���������������Զ�̶�����ĳ���ֱ�Ӳ����ڿͻ��ˡ�
2.\RemotingChatRoom\Client\Sender.cs
   csc.exe /r:share.dll c:\RemotingChatRoom\Client\Sender.cs
   ���ɵ� Sender.exe ������ ���� �� Remoting �ͻ��ˡ�
3.\RemotingChatRoom\Client\Receiver.cs
   csc.exe /r:share.dll c:\RemotingChatRoom\Client\Receiver.cs
   ���ɵ� Receiver.exe ������ �������з��� �� Remoting �ͻ��ˡ�
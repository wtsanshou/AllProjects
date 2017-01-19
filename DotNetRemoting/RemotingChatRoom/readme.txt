http://chs.gotdotnet.com/quickstart/howto/doc/Remoting/quickstart.aspx
编译时解析对远程对象的客户端引用的三种不同的方法为:
1.
编译客户端时,编译服务器对象并将 EXE 或 DLL 指定为对编译器的引用。如果客户端和服务器组件都是在同一地点开发的,这种方法会非常有用。
2.
从接口类派生服务器对象并使用接口编译客户端。如果客户端和服务器组件不是在同一地点开发时,这种方法会非常有用。可根据需要将接口编译为 DLL 并发送到客户端地点。应避免对已发布的接口进行更改。有关更多信息,请参考 COM 指南。
3.
使用 SOAPSUDS 工具从运行的服务器对象提取所需的元数据。如果客户端和服务器组件在不同的地点开发而且没有可用的接口类,这会非常有用。将 SOAPSUDS 工具指向远程 URI 并将所需元数据生成为源或 DLL。请注意,SOAPSUDS 工具仅提取元数据,不生成远程对象的源。

本例采用方法 2
.Net Remoting 实现简易的控制台聊天室

本套程序分别由 服务器端和客户端程序组成:
Server Side:
1.\RemotingChatRoom\Server\share.cs
   csc.exe /t:library c:\RemotingChatRoom\Server\share.cs
   生成的 share.dll 部署在服务器端,提供给 Client 的 Remoting 服务的类
2.\RemotingChatRoom\Server\Receiver.cs
   csc.exe /t:library c:\RemotingChatRoom\Server\Receiver.cs
   生成的 Receiver.dll 部署在服务器端,此时该 server 与 Client 位置互换角色:
   Client 的 Receiver 对象,也要为 server 提供远程服务,代理类即可。
3.\RemotingChatRoom\Server\Server.cs
   csc.exe c:\RemotingChatRoom\Server\Server.cs
   生成的 Server.exe 就是 Remoting Server 的主程序

Client Side:
1.\RemotingChatRoom\Client\share.cs
   csc.exe /t:library c:\RemotingChatRoom\Client\share.cs
   生成的 share.dll 部署在客户端,
   该程序集的 ChatRoom 类的只是远程的服务器端的相应程序集的 ChatRoom 类的代理类:
   命名空间 类 方法 等仅需签名要与服务器端一致即可。
   当然也可将服务器端完整的远程对象类的程序集直接部署在客户端。
2.\RemotingChatRoom\Client\Sender.cs
   csc.exe /r:share.dll c:\RemotingChatRoom\Client\Sender.cs
   生成的 Sender.exe 是用于 发言 的 Remoting 客户端。
3.\RemotingChatRoom\Client\Receiver.cs
   csc.exe /r:share.dll c:\RemotingChatRoom\Client\Receiver.cs
   生成的 Receiver.exe 是用于 接收所有发言 的 Remoting 客户端。
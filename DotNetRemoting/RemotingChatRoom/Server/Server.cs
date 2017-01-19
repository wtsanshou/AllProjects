/*
  csc c:\RemotingChatRoom\Server\Server.cs
*/

using System;
using System.Collections;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;



class Server
{
	public static void Main(string[] Args)
	{
		//RemotingConfiguration.Configure("s.config");
		BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
		provider.TypeFilterLevel = TypeFilterLevel.Full;
		IDictionary props = new Hashtable();
		props["port"] = 8080;
		TcpChannel tc = new TcpChannel(props, null, provider);
		ChannelServices.RegisterChannel(tc, false);
		RemotingConfiguration.RegisterWellKnownServiceType(typeof(ChatRoom), "ChatRoomURL", WellKnownObjectMode.Singleton);
		Console.WriteLine("Server .... , Press Enter key to exit.");
		Console.ReadLine();
	}
}

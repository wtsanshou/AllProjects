using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

public interface ProductInterface
{
    string getDescription();
}



class MyClient
{
    public static void Main()
    {
        TcpChannel tcpChannel = new TcpChannel();
        ChannelServices.RegisterChannel(tcpChannel);
        Type requiredType = typeof(ProductInterface);
        ProductInterface remoteObject = 
               (ProductInterface)Activator.GetObject(requiredType,              
                 "tcp://localhost:9998/MyToaster");
        Console.WriteLine(remoteObject.getDescription());
        Console.ReadLine();
    }
}

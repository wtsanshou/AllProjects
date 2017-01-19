using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class Program
{
    static void Main(string[] args)
    {
        ProductServer();
    }


    static void ProductServer()
    {
        Console.WriteLine("Server started...");
        TcpChannel tcpChannel = new TcpChannel(9998);
        ChannelServices.RegisterChannel(tcpChannel);
        Type commonInterfaceType = Type.GetType("Product");
        
        RemotingConfiguration.RegisterWellKnownServiceType(
                             commonInterfaceType,
                             "MyToaster", WellKnownObjectMode.SingleCall);
        System.Console.WriteLine("Press ENTER to quitnn");
        System.Console.ReadLine();
    }
}

public interface ProductInterface
{
    string getDescription();
}



public class Product : MarshalByRefObject, ProductInterface
{
    string descr = "toaster";

    public string getDescription()
    {
        return descr;
    }

}

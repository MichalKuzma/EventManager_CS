using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace EventManager_CSharp
{
    class Client
    {
        public EventManager eManager;

        public Client(string serverAddress)
        {
            TcpChannel chan = new TcpChannel();
            ChannelServices.RegisterChannel(chan, false);
            //RemotingConfiguration.Configure("..\\..\\..\\config\\Client.config", false);
            eManager = (EventManager)Activator.GetObject(typeof(EventManager), "tcp://"+serverAddress+":8080/EventManager");
        }

        public static Dictionary<string, Client> clientsMap = new Dictionary<string,Client>();
    }
}

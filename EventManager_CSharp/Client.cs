using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization;

namespace EventManager_CSharp
{
    [Serializable()]
    class Client
    {
        public EventManager eManager;

        public Client(string serverAddress)
        {
            //RemotingConfiguration.Configure("..\\..\\..\\config\\Client.config", false);
            eManager = (EventManager)Activator.GetObject(typeof(EventManager), "http://"+serverAddress+":8000/EventManager");
        }

        public static Dictionary<string, Client> clientsMap = new Dictionary<string,Client>();
    }
}

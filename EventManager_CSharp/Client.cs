using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;

namespace EventManager_CSharp
{
    class Client
    {
        public EventManager eManager;

        public Client(string serverAddress)
        {
            eManager = (EventManager)Activator.GetObject(typeof(EventManager), serverAddress);
        }

        public static Dictionary<string, Client> clientsMap = new Dictionary<string,Client>();
    }
}

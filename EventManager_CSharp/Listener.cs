using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;

namespace EventManager_CSharp
{
    class Listener
    {
        private static Listener instance;

        public static Listener Instance
        {
            get
            {
                if (instance == null)
                    instance = new Listener();
                return instance;
            }
        }

        public void Start()
        {
            RemotingConfiguration.Configure("..\\..\\..\\config\\Listener.config", false);
            HttpChannel chan = new HttpChannel(8000);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(EventManager), "EventManager", WellKnownObjectMode.Singleton);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

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
            TcpChannel chan = new TcpChannel(8080);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(EventManager), "EventManager", WellKnownObjectMode.Singleton);
            //RemotingConfiguration.Configure("..\\..\\..\\config\\Listener.config", false);
        }
    }
}

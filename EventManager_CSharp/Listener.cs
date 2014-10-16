using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;

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
        }
    }
}

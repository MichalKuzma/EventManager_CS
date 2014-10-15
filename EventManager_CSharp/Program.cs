using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EventManager_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ToDo: Start XML-RPC Service
            EventManager.Instance.Start();            
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DragonDataSniffer.Utils;
using DragonDataSniffer.Network;

namespace DragonDataSniffer
{
    public static class Parser
    {

        public static bool  Load()
        {
            if(!Config.Load())
            {
                return false;
            }
            if (Reflector.GetInitializerMethods().Any(method => !method.Invoke()))
            {

                return false ;
            }


            if(!GameClientAcceptor.Load(Config.Instance.TunnelPort,ClientType.Login))
            {

            }
     
            return true;
        }
    }
}

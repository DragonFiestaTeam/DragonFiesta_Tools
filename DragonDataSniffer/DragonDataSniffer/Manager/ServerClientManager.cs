using System;
using System.Collections.Generic;
using DragonDataSniffer.Network;

namespace DragonDataSniffer.Manager
{
    [TunnelModule(InitializationStage.Clients)]
    public class ServerClientManager
    {
        private Dictionary<ClientType,ServerClient> ClientList { get; set; }
        public static ServerClientManager Instance { get; private set; }

        public ServerClientManager()
        {
            ClientList = new Dictionary<ClientType, ServerClient>();
        }
        [InitializerMethod]
        public static bool Load()
        {
            try
            {
                Instance = new ServerClientManager();
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLine(LogLevel.Exception, "ServerClientManager exception: {0}", ex.ToString());
                return false;
            }
        }

        public bool GetClientByType(ClientType pType,out ServerClient pClient)
        {
            if (ClientList.TryGetValue(pType, out pClient))
                return true;

            return false;
        }
        public bool AddClient(ServerClient pClient, ClientType pClientType)
        {
            if (!ClientList.ContainsKey(pClientType))
            {
                ClientList.Add(pClientType, pClient);
                return true;
            }
            return false;
        }
        public bool RemovByType(ClientType pType)
        {
            return ClientList.Remove(pType);
        }

        public bool IsConnect(ClientType pType)
        {
            return ClientList.ContainsKey(pType);
        }
    }
}

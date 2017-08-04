using System.Net.Sockets;
using DragonDataSniffer.Manager;

namespace DragonDataSniffer.Network
{
    public sealed class GameClientAcceptor : Listener
    {
     
        public ClientType AcceptorType { get; private set; }

        public GameClientAcceptor(int port, ClientType pType)
                : base(port)
        {
            AcceptorType = pType;
            Start();
            Log.WriteLine(LogLevel.Info, "Accepting GameClients on port {0}", port);
        }

        public override void OnClientConnect(Socket socket)
        {
            GameClient pClient = new GameClient(socket,AcceptorType);
           
            if (GameClientManager.Instance.AddClient(pClient, AcceptorType))
            {
                pClient.sClient = new ServerClient(Config.Instance.ConnectIP, Port, pClient);
                Log.WriteLine(LogLevel.Info, "GameClient connected from {0}", pClient.IP);
            }
            else
            {
                pClient.OnDisconnect();
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;

namespace FiestaTunnel
{
    public class Listener
    {
        public int Port { get; private set; }
        private Socket socket;
        public string IP { get; set; }

        public Listener(int port, string ip = "46.253.147.147")
        {
            Port = port;
            IP = ip;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Any, port));
                socket.Listen(10);
                Console.WriteLine("Tunnel ready at {0} to {1}", port, IP);
            }
            catch (SocketException ex) {
                Console.WriteLine("Error starting tunnel: {0}", ex.Message);
                Console.ReadLine();
                return;
            }
            socket.BeginAccept(new AsyncCallback(OnConnect), null);
        }

        public void Close()
        {
            socket.Close();
        }

        private void OnConnect(IAsyncResult ar)
        {
            if (!Program.isRunning) return;
            try
            {
                Socket newSocket = socket.EndAccept(ar);
                LinkedClient client = new LinkedClient(newSocket, IP, Port);
                Console.WriteLine("New session started.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error accepting connection: {0}@@{1}", ex.Message, ex.StackTrace);
            }
            finally
            {
                socket.BeginAccept(new AsyncCallback(OnConnect), null);
            }
        }
    }
}

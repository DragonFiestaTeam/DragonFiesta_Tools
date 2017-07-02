using System;

namespace FiestaLib.Networking
{
    public sealed class NetworkEventArgs : EventArgs
    {
        public NetworkEventType Type { get; private set; }
        public object Obj { get; private set; }

        public NetworkEventArgs(NetworkEventType type)
        {
            Type = type;
        }

        public NetworkEventArgs(NetworkEventType type, object obj)
        {
            Type = type;
            Obj = obj;
        }
    }

    public enum NetworkEventType : byte
    {
        Disconnected,
        PacketReceived,
        CryptoReceived,
    }
}

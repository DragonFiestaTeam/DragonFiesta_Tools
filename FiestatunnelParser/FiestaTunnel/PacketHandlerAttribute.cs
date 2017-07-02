using System;
using FiestaLib.Networking;

namespace FiestaTunnel
{

    internal delegate void PacketHandlerCallback(LinkedClient pLoginClient, Packet pPacket);

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class PacketHandlerAttribute : Attribute
    {
        public ushort OpCode { get; private set; }

        public PacketHandlerAttribute(ushort opCode)
        {
            OpCode = opCode;
        }
    }

}

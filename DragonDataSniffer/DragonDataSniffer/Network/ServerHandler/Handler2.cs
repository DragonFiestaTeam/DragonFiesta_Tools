namespace DragonDataSniffer.Network.ServerHandler
{
    public sealed class Handler2
    {
        [ServerPacketHandler(Handler2Type._Header, Handler2Type.SetXorKeyPosition)]
        public static void On_ReadXorPos(ServerClient client, FiestaPacket packet)
        {
            if (!packet.TryReadInt16(out short XorPos))
            {
                client.OnDisconnect();
            }

            client.CClient.Crypto = new FiestaCryptoProvider(XorPos);
            client.Crypto = new FiestaCryptoProvider(XorPos);
            client.CClient.SendPacket(packet);
            client.CClient.Start();
        }
    }
}
﻿using System;
using FiestaLib.Networking;
using FiestaLib.Data;

namespace FiestaTunnel.Handler
{
    internal class ChatHandler
    {
        [PacketHandler((ushort)ClienOpcodes.ChatMessage)]
        public static void HandleChatMessage(LinkedClient pClient, Packet pPacket)
        {
            Packet packet = pPacket;

            string chattext = "";
            if (!packet.TryReadByte(out byte chatlen) ||
                !packet.TryReadString(out chattext, chatlen))
            {
                Console.WriteLine("Error reading chat command.");
            }

            if (chattext.StartsWith("!"))
            {
                try
                {
                    ChatCommandHandler.HandleChatCommand(chattext,pClient);
                }
                catch (Exception ex)
                {
                   pClient.DropMessage(ex.Message);
                }
            }
            else
            {
               pClient.outClient.SendPacket(packet);
            }
        }
    }
}

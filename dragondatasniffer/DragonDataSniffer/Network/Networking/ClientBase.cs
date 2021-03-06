﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;
using System.Threading;
using DragonDataSniffer.Object;
using DragonDataSniffer.Network.Networking;

namespace DragonDataSniffer.Network
{
    public class ClientBase : IDisposable
    {
        public Socket Socket { get; internal set; }
        public IPAddress IP { get; private set; }
        public ClientType pType { get; set; }//using to identy client
        public Character pCharacter { get; internal set; }
        public UInt16 Port { get; private set; }

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;
        public event EventHandler<EventArgs> Disconnected;

        public bool IsDisposed
        {
            get { return (isDisposedInt > 0); }
        }
        private Int32 isDisposedInt;
        //receive
        public const Int32 MaxReceiveBuffer = 16384;

        public FiestaCryptoProvider Crypto = null;
        public byte[] receiveBuffer;
        private byte[] remaining;
        private bool headerParsed;
        public bool keyPosSet;
        private byte headerLength;
        private UInt16 packetLength;
        private byte[] packetBuffer;
        private Int32 packetIndex;

        //send
        public ConcurrentQueue<byte[]> sendBuffer;
        private Int32 isSendingInt;

        public ClientBase()
        {
            receiveBuffer = new byte[MaxReceiveBuffer];
            sendBuffer = new ConcurrentQueue<byte[]>();
        }
        public ClientBase(Socket pSocket, ClientType pClientType)
        {
            Socket = pSocket;
            pType = pClientType;
            var addr = (IPEndPoint)pSocket.RemoteEndPoint;
            IP = addr.Address;
            Port = (UInt16)addr.Port;
            receiveBuffer = new byte[MaxReceiveBuffer];
            sendBuffer = new ConcurrentQueue<byte[]>();

        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            if (Socket != null)
            {
                Socket.Close();
                Socket.Dispose();
            }
            Socket = null;

            IP = null;
            receiveBuffer = null;
            sendBuffer = null;
            PacketReceived = null;
            isDisposedInt = 1;
        }
        ~ClientBase()
        {
            Dispose();
        }

        public virtual void Start()
        {
            BeginReceive();
        }

        private void BeginReceive()
        {
            if (IsDisposed)
            {
                return;
            }

            try
            {
                var args = new SocketAsyncEventArgs();
                args.Completed += EndReceive;
                args.SetBuffer(receiveBuffer, 0, MaxReceiveBuffer);

                if (!Socket.ReceiveAsync(args))
                {
                    EndReceive(null, args);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(LogLevel.Exception, ex.ToString());
                OnDisconnect();
            }
        }
        private void EndReceive(object sender, SocketAsyncEventArgs args)
        {
            if (IsDisposed)
            {
                return;
            }

            try
            {
                var transfered = args.BytesTransferred;

                if (transfered < 1)
                {
                    // this throws when i send transfer-details. server-sided disconnect?
                    //socket error
                    OnDisconnect();
                    return;
                }

                //get data to handle
                byte[] data;

                if (remaining != null)
                {
                    data = new byte[(transfered + remaining.Length)];

                    Buffer.BlockCopy(remaining, 0, data, 0, remaining.Length);
                    Buffer.BlockCopy(receiveBuffer, 0, data, remaining.Length, transfered);

                    //reset remaining bytes
                    remaining = null;
                }
                else
                {
                    data = new byte[transfered];
                    Buffer.BlockCopy(receiveBuffer, 0, data, 0, transfered);
                }

                //handle all bytes
                var dataIndex = 0;
                while (data.Length > dataIndex)
                {
                    //get left bytes
                    var bytesLeft = (data.Length - dataIndex);


                    //parse packet length
                    if (!headerParsed)
                    {
                        //at least 3 bytes are required for parsing
                        if (bytesLeft < 3)
                        {
                            if (bytesLeft > 0)
                            {
                                remaining = new byte[bytesLeft];
                                Buffer.BlockCopy(data, dataIndex, remaining, 0, bytesLeft);
                            }

                            return;
                        }


                        headerLength = 1;
                        packetLength = data[dataIndex];

                        if (packetLength == 0)
                        {
                            headerLength = 3;
                            packetLength = BitConverter.ToUInt16(data, (dataIndex + 1));
                        }

                        //update indexes
                        dataIndex += headerLength;
                        bytesLeft -= headerLength;

                        //create packet buffer
                        packetBuffer = new byte[packetLength];
                        packetIndex = 0;

                        //we got our len
                        headerParsed = true;

                        //check if there are any bytes left for handling
                        if (bytesLeft < 1)
                        {
                            break;
                        }
                    }

                    //get missing bytes
                    var missing = (packetLength - packetIndex);

                    //get length of the bytes to copy
                    var copyLen = (bytesLeft <= missing ? bytesLeft : missing);

                    //copy next bytes to packet
                    Buffer.BlockCopy(data, dataIndex, packetBuffer, packetIndex, copyLen);

                    //update indexes again
                    dataIndex += copyLen;
                    bytesLeft -= copyLen;
                    packetIndex += copyLen;

                    //check if packet is finished
                    if (packetIndex >= packetLength)
                    {
                        // we are client. we decrypt outgoing shit.
                        //decrypt the shit
                        // DecryptPacket(ref packetBuffer);

                        if (this is GameClient)
                        {
                            Crypto.Crypt(packetBuffer, 0, packetBuffer.Length);
                        }
                        if (PacketReceived != null)
                        {
                            PacketReceived.Invoke(this, new PacketReceivedEventArgs(new FiestaPacket(packetBuffer)));
                        }

                        //and reset this packet
                        headerParsed = false;
                    }
                }

                //clean up
                data = null;

                //all done =)
            }
            catch (Exception ex)
            {
                Log.WriteLine(LogLevel.Exception, ex.ToString());
                OnDisconnect();
            }
            finally
            {
                args.Dispose();

                BeginReceive();
            }
        }
        private void BeginSend()
        {
            if (IsDisposed)
            {
                return;
            }

            try
            {
                if (sendBuffer.TryPeek(out byte[] buffer))
                {
                    var args = new SocketAsyncEventArgs();
                    args.Completed += EndSend;
                    args.SetBuffer(buffer, 0, buffer.Length);


                    if (!Socket.SendAsync(args))
                    {
                        EndSend(null, args);
                    }
                }
                else
                {
                    Interlocked.Exchange(ref isSendingInt, 0);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(LogLevel.Exception, ex.ToString());
                OnDisconnect();
            }
        }
        private void EndSend(object sender, SocketAsyncEventArgs args)
        {
            if (IsDisposed)
            {
                return;
            }

            try
            {
                var transfered = args.BytesTransferred;

                if (transfered < 1)
                {
                    OnDisconnect();
                    return;
                }


                // can be sent as state object to args
                if (sendBuffer.TryPeek(out byte[] buffer))
                {
                    //check if all bytes were send
                    if (buffer.Length == transfered)
                    {
                        sendBuffer.TryDequeue(out buffer);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(LogLevel.Exception, ex.ToString());
                OnDisconnect();
            }
            finally
            {
                if (!IsDisposed)
                {
                    if (sendBuffer != null &&
                        sendBuffer.Count > 0)
                    {
                        BeginSend();
                    }
                    else
                    {
                        Interlocked.Exchange(ref isSendingInt, 0);
                    }
                }
            }
        }

        public void Send(byte[] data)
        {
            if (IsDisposed)
            {
                return;
            }

            if (data.Length > UInt16.MaxValue)
            {
                throw new OutOfMemoryException();
            }

            try
            {
                sendBuffer.Enqueue(data);

                if (Interlocked.CompareExchange(ref isSendingInt, 1, 0) == 0)
                {
                    BeginSend();
                }
            }
            catch (Exception)
            {
                OnDisconnect();
            }
        }

        public void SendPacket(FiestaPacket pPacket)
        {
            if (this is GameClient)
            {
                Send(pPacket.ToArray());
            }
            else
            {
                Send(pPacket.ToArray(Crypto));
            }
        }

        public void OnDisconnect()
        {

            if (Disconnected != null && !IsDisposed)
            {
                Disconnected(this, new EventArgs());
            }
            Dispose();
        }
    }
}
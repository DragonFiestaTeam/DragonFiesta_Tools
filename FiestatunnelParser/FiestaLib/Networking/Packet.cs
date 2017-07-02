using System;
using System.Text;
using System.IO;
using FiestaLib.Util;
using FiestaLib.Encryption;
using FiestaLib.Data;

namespace FiestaLib.Networking
{
    public sealed class Packet : IDisposable
    {

        private MemoryStream memoryStream;
        private BinaryReader reader;
        private BinaryWriter writer;

        public ushort OpCode { get; private set; }
        public int Length { get { return (int)memoryStream.Length; } }
        public int Cursor { get { return (int)memoryStream.Position; } }
        public int Remaining { get { return (int)(memoryStream.Length - memoryStream.Position); } }

        public Packet(ushort pOpCode)
        {
            memoryStream = new MemoryStream();
            writer = new BinaryWriter(memoryStream);
            OpCode = pOpCode;
            WriteUShort(pOpCode);
        }
        public Packet(ServerOpCode pOpCode)
        {
            memoryStream = new MemoryStream();
            writer = new BinaryWriter(memoryStream);
            OpCode = (ushort)pOpCode;
            WriteUShort((ushort)pOpCode);
        }
        public Packet()
        {
            memoryStream = new MemoryStream();
            writer = new BinaryWriter(memoryStream);
        }

        internal Packet(byte[] pData)
        {
            memoryStream = new MemoryStream(pData);
            reader = new BinaryReader(memoryStream);
            writer = new BinaryWriter(memoryStream);

            TryReadUShort(out ushort opCode);
            OpCode = opCode;
        }

        public void Dispose()
        {
            if (writer != null) writer.Close();
            if (reader != null) reader.Close();
            memoryStream = null;
            writer = null;
            reader = null;
        }

        ~Packet()
        {
            Dispose();
        }

        public void SetOffset(int offset)
        {
            if (offset > Length) throw new IndexOutOfRangeException("Cannot go to packet offset.");
            memoryStream.Seek(offset, SeekOrigin.Begin);
        }

        #region Write methods

        public void WriteHexAsBytes(string hexString)
        {
            byte[] bytes = ByteUtils.HexToBytes(hexString);
            WriteBytes(bytes);
        }

        public void SetByte(long pOffset, byte pValue)
        {
            long oldoffset = memoryStream.Position;
            memoryStream.Seek(pOffset, SeekOrigin.Begin);
            writer.Write(pValue);
            memoryStream.Seek(oldoffset, SeekOrigin.Begin);
        }

        public void Fill(int pLenght, byte pValue)
        {
            for (int i = 0; i < pLenght; ++i)
            {
                WriteByte(pValue);
            }
        }

        public void WriteDouble(double pValue)
        {
            writer.Write(pValue);
        }

        public void WriteBool(bool pValue)
        {
            writer.Write(pValue);
        }

        public void WriteByte(byte pValue)
        {
            writer.Write(pValue);
        }

        public void WriteSByte(sbyte pValue)
        {
            writer.Write(pValue);
        }

        public void WriteBytes(byte[] pBytes)
        {
            writer.Write(pBytes);
        }

        public void WriteUShort(ushort pValue)
        {
            writer.Write(pValue);
        }

        public void WriteShort(short pValue)
        {
            writer.Write(pValue);
        }

        public void WriteUInt(uint pValue)
        {
            writer.Write(pValue);
        }

        public void WriteInt(int pValue)
        {
            writer.Write(pValue);
        }

        public void WriteFloat(float pValue)
        {
            writer.Write(pValue);
        }

        public void WriteULong(ulong pValue)
        {
            writer.Write(pValue);
        }

        public void WriteLong(long pValue)
        {
            writer.Write(pValue);
        }

        public void WriteString(string pValue)
        {
            WriteBytes(Encoding.ASCII.GetBytes(pValue));
        }

        public void WriteString(string pValue, int pLen)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(pValue);
            if (buffer.Length > pLen)
            {
                throw new OutOfMemoryException();
            }
            else
            {
                WriteBytes(buffer);
                for (int i = 0; i < pLen - buffer.Length; i++)
                {
                    WriteByte(0);
                }
            }
        }

        #endregion

        #region Read methods

        public bool ReadSkip(int pLength)
        {
            if (Remaining < pLength) return false;

            memoryStream.Seek(pLength, SeekOrigin.Current);
            return true;
        }

        public bool TryReadBool(out bool pValue)
        {
            pValue = false;
            if (Remaining < 1) return false;
            pValue = reader.ReadBoolean();
            return true;
        }

        public bool TryReadByte(out byte pValue)
        {
            pValue = 0;
            if (Remaining < 1) return false;
            pValue = reader.ReadByte();
            return true;
        }

        public bool TryReadSByte(out sbyte pValue)
        {
            pValue = 0;
            if (Remaining < 1) return false;
            pValue = reader.ReadSByte();
            return true;
        }

        // UInt16 is more conventional
        public bool TryReadUShort(out ushort pValue)
        {
            pValue = 0;
            if (Remaining < 2) return false;
            pValue = reader.ReadUInt16();
            return true;
        }

        // Int16 is more conventional
        public bool TryReadShort(out short pValue)
        {
            pValue = 0;
            if (Remaining < 2) return false;
            pValue = reader.ReadInt16();
            return true;
        }

        public bool TryReadFloat(out float pValue)
        {
            pValue = 0;
            if (Remaining < 2) return false;
            pValue = reader.ReadSingle();
            return true;
        }

        // UInt32 is better
        public bool TryReadUInt(out uint pValue)
        {
            pValue = 0;
            if (Remaining < 4) return false;
            pValue = reader.ReadUInt32();
            return true;
        }

        // Int32
        public bool TryReadInt(out int pValue)
        {
            pValue = 0;
            if (Remaining < 4) return false;
            pValue = reader.ReadInt32();
            return true;
        }

        // UInt64
        public bool TryReadULong(out ulong pValue)
        {
            pValue = 0;
            if (Remaining < 8) return false;
            pValue = reader.ReadUInt64();
            return true;
        }

        // UInt64
        public bool TryReadLong(out long pValue)
        {
            pValue = 0;
            if (Remaining < 8) return false;
            pValue = reader.ReadInt64();
            return true;
        }

        public bool TryReadString(out string pValue)
        {
            pValue = "";
            if (Remaining < 1) return false;
            TryReadByte(out byte len);
            if (Remaining < len) return false;
            return TryReadString(out pValue, len);
        }

        public bool TryReadString(out string pValue, int pLen)
        {
            pValue = "";
            if (Remaining < pLen) return false;

            byte[] buffer = new byte[pLen];
            ReadBytes(buffer);
            int length = 0;
            if (buffer[pLen - 1] != 0)
            {
                length = pLen;
            }
            else
            {
                while (buffer[length] != 0x00 && length < pLen)
                {
                    length++;
                }
            }
            if (length > 0)
            {
                pValue = Encoding.ASCII.GetString(buffer, 0, length);
            }

            return true;
        }

        public bool ReadBytes(byte[] pBuffer)
        {
            if (Remaining < pBuffer.Length) return false;
            memoryStream.Read(pBuffer, 0, pBuffer.Length);
            return true;
        }

        #endregion

        public byte[] ToArray(NetCrypto crypt = null)
        {
            byte[] buffer;
            byte[] encbuffer = memoryStream.ToArray();
            if (crypt != null)
            {
                crypt.Crypt(encbuffer);
            }
            if (encbuffer.Length <= 0xff)
            {
                buffer = new byte[encbuffer.Length + 1];
                Buffer.BlockCopy(encbuffer, 0, buffer, 1, encbuffer.Length);
                buffer[0] = (byte)encbuffer.Length;
            }
            else
            {
                buffer = new byte[encbuffer.Length + 3];
                Buffer.BlockCopy(encbuffer, 0, buffer, 3, encbuffer.Length);
                Buffer.BlockCopy(BitConverter.GetBytes((ushort)encbuffer.Length), 0, buffer, 1, 2);
            }
            return buffer;
        }

        public string Dump()
        {
            return ByteUtils.BytesToHex(memoryStream.ToArray(), string.Format("Packet (0x{0} - {1}): ", OpCode.ToString("X4"), Length));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace iQuest
{
    public class SHNColumn
    {
        public string name;
        public uint Type;
        public int Lenght;
    }

    public class SHNFile
    {
        public byte[] CryptHeader;
        public uint Header;
        public string Path;
        public DataTable table = new DataTable();
        public Dictionary<int, int> displayToReal = new Dictionary<int, int>();

        public List<SHNColumn> columns = new List<SHNColumn>();

        uint RecordCount;
        uint DefaultRecordLength;
        uint ColumnCount;
        string[] ColumnNames; 
        uint[] ColumnTypes;
        int[] ColumnLengths;
        public byte[] data;

        public SHNFile()
        {

        }

        public void LoadMe(string path)
        {
                BinaryReaderEx r = new BinaryReaderEx(File.OpenRead(path));
                if (path.EndsWith(".shn"))
                {
                CryptHeader = r.ReadBytes(0x20);
                    data = r.ReadBytes(r.ReadInt32() - 0x24);
                }
                else
                {
                    data = r.ReadBytes((int)r.Length);
                }
                r.Close();
            Decrypt(data, 0, data.Length);
        }

        public void CreateDefaultLayout()
        {
            CryptHeader = new byte[32];
            SetCryptHeader("3B 02 00 00 32 30 30 35 2D 30 38 2D 32 36 20 BF C0 C8 C4 20 32 3A 33 00 6A 7F 00 00 01 00 00 00");
            Header = 0;
            CreateColumn("Empty Column", byte.MaxValue, 24, "");
            Path = "New File.shn";
        }

        public void SetCryptHeader(string hexString)
        {
            string[] tempArray = hexString.Split(' ');
            if (tempArray.Length != 32) throw new Exception("Incorrect header lenght!");
            for (int i = 0; i < tempArray.Length; ++i)
               CryptHeader[i] =  byte.Parse(tempArray[i], System.Globalization.NumberStyles.HexNumber);
        }

        public string GetCryptString()
        {
            string toret = "";
            for (int i = 0; i < CryptHeader.Length; ++i)
            {
                if (i != CryptHeader.Length - 1)
                    toret += CryptHeader[i].ToString("X2") + " ";
                else
                    toret += CryptHeader[i].ToString("X2");
            }
            return toret;
        }

        public SHNFile(string path, int x)
        {
            try
            {
                columns.Clear();
                Path = path;
                BinaryReaderEx r = new BinaryReaderEx(File.OpenRead(path));
                if (path.EndsWith(".shn"))
                {
                    CryptHeader = r.ReadBytes(32);                  
                    data = r.ReadBytes(r.ReadInt32() - 36);
                }
                else
                {
                    data = r.ReadBytes((int)r.Length);
                }
                r.Close();

                switch(x)
                {
                    case 0:
                        DecryptNormal(data, 0, data.Length);
                        break;
                    case 1:
                        Decrypt(data, 0, data.Length);
                        break;
                    default:
                        Decrypt(data, 0, data.Length);
                        break;
                }
                r = new BinaryReaderEx(new MemoryStream(data));
                Header = r.ReadUInt32();
                if (Header == 3452816845 | Header == 0)
                {
                    //bleh, why check, unk.dat is useless
                }
                RecordCount = r.ReadUInt32();
                DefaultRecordLength = r.ReadUInt32();
                ColumnCount = r.ReadUInt32();
                ColumnNames = new string[ColumnCount];
                ColumnTypes = new uint[ColumnCount];
                ColumnLengths = new int[ColumnCount];

                int num2 = 2;
                int unkCols = 0;
                for (uint i = 0; i < ColumnCount; ++i)
                {
                    string str = r.ReadString(48);
                    uint num4 = r.ReadUInt32();
                    int num5 = r.ReadInt32();

                    SHNColumn col = new SHNColumn();
                    if (str.Length == 0)
                    {
                        str = "UnkCol" + unkCols.ToString();
                        unkCols++;
                    }
                    col.name = str;
                    col.Type = num4;
                    col.Lenght = num5;
                    columns.Add(col);
                    ColumnNames[i] = str;
                    ColumnTypes[i] = num4;
                    ColumnLengths[i] = num5;
                    num2 += num5;
                }
                if (num2 != DefaultRecordLength)
                {
                    throw new Exception("Wrong record lenght!");
                }
                GenerateColumns(table, columns);
                ReadRows(r, table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Decryption Error");
            }
        }

        public void Dispose()
        {
            table = null;
            CryptHeader = null;
        }

        public uint GetDefaultRecLen()
        {
            uint start = 2;
            foreach (DataColumn colz in table.Columns)
            {
                SHNColumn col = GetColByName(colz.ColumnName);
                start += (uint)col.Lenght;
            }
            return start;
        }

        public bool Save(string file)
        {
                MemoryStream output = new MemoryStream();
                BinaryWriter w = new BinaryWriter(output);
                try
                {
                w.Write(Header);
                w.Write(table.Rows.Count); //rowcount
                w.Write(GetDefaultRecLen());
                w.Write(table.Columns.Count);
                for (int i = 0; i < table.Columns.Count; ++i)
                {
                    SHNColumn colz = GetColByName(table.Columns[displayToReal[i]].ColumnName); //converts the display to the row order
                    if (colz.name.Contains("UnkCol"))
                    {
                        w.Write(new byte[0x30]); //empty name
                    }
                    else
                    {
                        WriteString(w, colz.name, 0x30);
                    }
                    w.Write(colz.Type);
                    w.Write(colz.Lenght);
                }
                WriteRows(w);
                byte[] sourceArray = output.GetBuffer();
                long length = output.Length;
                byte[] destinationArray = new byte[length];
                Array.Copy(sourceArray, destinationArray, length);
                Decrypt(destinationArray, 0, destinationArray.Length);
                w.Close();
                w = new BinaryWriter(File.Create(file));
                w.Write(CryptHeader);
                w.Write(destinationArray.Length + 0x24);
                w.Write(destinationArray);
                w.Close();
                Path = file;
                return true;
            }
            catch(Exception ex)
            {
                w.Close();
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void DeleteColumn(string name)
        {
            columns.Remove( GetColByName(name));
            table.Columns.Remove(GetDataColByName(name));
        }

        public void CreateColumn(string name, int len, uint type, string defaultval)
        {
            SHNColumn newCol = new SHNColumn();
            newCol.name = name;
            newCol.Lenght = len;
            newCol.Type = type;
            columns.Add(newCol);

            DataColumn column = new DataColumn();
            column.ColumnName = name;
            column.DefaultValue = defaultval;
            column.DataType = GetType(newCol);
            table.Columns.Add(column);
        }

        public void EditColumnName(string from, string to)
        {
            DataColumn olddat = GetDataColByName(from);
            olddat.ColumnName = to;

            SHNColumn colz = GetColByName(from);
            colz.name = to;
        }

        public DataColumn GetDataColByName(string name)
        {
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                if (table.Columns[i].ColumnName == name) return table.Columns[i];
            }
            return null;
        }

        public SHNColumn GetColByName(string name)
        {
            foreach (SHNColumn col in columns)
            {
                if (col.name.ToLower() == name.ToLower()) return col;
            }
            return null;
        }

        public int GetRowByIndex(int ColIndex, string RowInput)
        {
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                if (table.Rows[i][ColIndex].ToString().ToLower() == RowInput.ToLower()) return i;
            }
            return -1;
        }

        public void SetPrimaryKey(string columnName)
        {

        }

        private void WriteRows(BinaryWriter w)
        {
            for (int iz = 0; iz < table.Rows.Count; iz++)
            {
                DataRow row = table.Rows[iz];
                long position = w.BaseStream.Position;
                w.Write((ushort)0);

                for (int i = 0; i < table.Columns.Count; ++i)
                {
                    object obj2 = row.ItemArray[displayToReal[i]];
                    if (obj2 == null) obj2 = "0";
                    SHNColumn col = GetColByName(table.Columns[displayToReal[i]].ColumnName);
                    switch (col.Type)
                    {
                        case 1:
                            if (obj2 is string)
                            {
                                obj2 = byte.Parse((string)obj2);
                            }
                            w.Write((byte)obj2);
                            break;

                        case 2:
                            if (obj2 is string)
                            {
                                obj2 = ushort.Parse((string)obj2);
                            }
                            w.Write((ushort)obj2);
                            break;

                        case 3:
                            if (obj2 is string)
                            {
                                obj2 = uint.Parse((string)obj2);
                            }
                            w.Write((uint)obj2);
                            break;

                        case 5:
                            if (obj2 is string)
                            {
                                obj2 = float.Parse((string)obj2);
                            }
                            w.Write((float)obj2);
                            break;

                        case 9:
                            WriteString(w, (string)obj2, col.Lenght);
                            break;

                        case 11:
                            if (obj2 is string)
                            {
                                obj2 = uint.Parse((string)obj2);
                            }
                            w.Write((uint)obj2);
                            break;

                        case 12:
                            if (obj2 is string)
                            {
                                obj2 = byte.Parse((string)obj2);
                            }
                            w.Write((byte)obj2);
                            break;

                        case 13:
                            if (obj2 is string)
                            {
                                obj2 = short.Parse((string)obj2);
                            }
                            w.Write((short)obj2);
                            break;

                        case 0x10:
                            if (obj2 is string)
                            {
                                obj2 = byte.Parse((string)obj2);
                            }
                            w.Write((byte)obj2);
                            break;

                        case 0x12:
                            if (obj2 is string)
                            {
                                obj2 = uint.Parse((string)obj2);
                            }
                            w.Write((uint)obj2);
                            break;

                        case 20:
                            if (obj2 is string)
                            {
                                obj2 = sbyte.Parse((string)obj2);
                            }
                            w.Write((sbyte)obj2);
                            break;

                        case 0x15:
                            if (obj2 is string)
                            {
                                obj2 = short.Parse((string)obj2);
                            }
                            w.Write((short)obj2);
                            break;

                        case 0x16:
                            if (obj2 is string)
                            {
                                obj2 = int.Parse((string)obj2);
                            }
                            w.Write((int)obj2);
                            break;

                        case 0x18:
                            WriteString(w, (string)obj2, col.Lenght);
                            break;

                        case 0x1a:
                            WriteString(w, (string)obj2, -1);
                            break;

                        case 0x1b:
                            if (obj2 is string)
                            {
                                obj2 = uint.Parse((string)obj2);
                            }
                            w.Write((uint)obj2);
                            break;
                    }
                }
                long num3 = w.BaseStream.Position - position;
                long offset = w.BaseStream.Position;
                w.BaseStream.Seek(position, SeekOrigin.Begin);
                w.Write((ushort)num3);
                w.BaseStream.Seek(offset, SeekOrigin.Begin);
            }
        }

        public int getColIndex(string name)
        {
            for (int i = 0; i < table.Columns.Count; ++i)
            {
                if (table.Columns[i].ColumnName == name) return i;
            }
            return -1;
        }

        private void WriteString(BinaryWriter w, string s, int length)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            if (length == -1)
            {
                length = bytes.Length + 1;
            }
            byte[] destinationArray = new byte[length];
            Array.Copy(bytes, destinationArray, Math.Min(length, bytes.Length));
            w.Write(destinationArray);
        }

        private void ReadRows(BinaryReaderEx r, DataTable table)
        {
            object[] values = new object[columns.Count];
            for (uint i = 0; i < RecordCount; ++i)
            {
                r.ReadUInt16();
                for (int j = 0; j < columns.Count; j++)
                {
                    switch (columns[j].Type)
                    {
                        case 1:
                            values[j] = r.ReadByte();
                            break;

                        case 2:
                            values[j] = r.ReadUInt16();
                            break;

                        case 3:
                            values[j] = r.ReadUInt32();
                            break;

                        case 5:
                            values[j] = r.ReadSingle();
                            break;

                        case 9:
                            values[j] = r.ReadString(ColumnLengths[j]);
                            break;

                        case 11:
                            values[j] = r.ReadUInt32();
                            break;

                        case 12:
                            values[j] = r.ReadByte();
                            break;

                        case 13:
                            values[j] = r.ReadInt16();
                            break;

                        case 0x10:
                            values[j] = r.ReadByte();
                            break;

                        case 0x12:
                            values[j] = r.ReadUInt32();
                            break;

                        case 20:
                            values[j] = r.ReadSByte();
                            break;

                        case 0x15:
                            values[j] = r.ReadInt16();
                            break;

                        case 0x16:
                            values[j] = r.ReadInt32();
                            break;

                        case 0x18:
                            values[j] = r.ReadString(ColumnLengths[j]);
                            break;

                        case 0x1a:
                            values[j] = r.ReadString();
                            break;

                        case 0x1b:
                            values[j] = r.ReadUInt32();
                            break;
                    }
                }
                table.Rows.Add(values);
            }
        }

        private void GenerateColumns(DataTable table, List<SHNColumn> cols)
        {
            for (int i = 0; i < cols.Count; ++i)
            {
                DataColumn column = new DataColumn();
                column.ColumnName = cols[i].name;
                column.DataType = GetType(cols[i]);
                table.Columns.Add(column);
            }
        }

        public Type GetType(SHNColumn col)
        {
            switch (col.Type)
            {
                default:
                    return typeof(object);
                case 1:
                case 12:
                    return typeof(byte);   
                case 2:
                    return typeof(UInt16);
                case 3:
                case 11:
                    return typeof(UInt32); 
                case 5:
                    return typeof(Single);
                case 0x15:   
                case 13:
                    return typeof(Int16); 
                case 0x10:
                    return typeof(byte);   
                case 0x12:
                case 0x1b:
                    return typeof(UInt32);   
                case 20:
                    return typeof(SByte);
                case 0x16:
                    return typeof(Int32); 
                case 0x18:
                case 0x1a:
                case 9:
                    return typeof(string);
                    
            }
        }

        private void Decrypt(byte[] data, int index, int length)
        {
            if (((index < 0) | (length < 1)) | ((index + length) > data.Length))
            {
                throw new IndexOutOfRangeException();
            }
            byte num = (byte)length;
            for (int i = length - 1; i >= 0; --i)
            {
                data[i] = (byte)(data[i] ^ num);
                byte num3 = (byte)i;
                num3 = (byte)(num3 & 15);
                num3 = (byte)(num3 + 0x55);
                num3 = (byte)(num3 ^ ((byte)(((byte)i) * 11)));
                num3 = (byte)(num3 ^ num);
                num3 = (byte)(num3 ^ 170);
                num = num3;
            }
        }

        private void DecryptNormal(byte[] data, int index, int length)
        {
            if (((index < 0) | (length < 1)) | ((index + length) > data.Length))
            {
                throw new IndexOutOfRangeException();
            }
            byte num = (byte)length;
            for (int i = length - 1; i >= 0; --i)
            {
                data[i] = (byte)(data[i] ^ num);
                byte num3 = (byte)i;
                num3 = (byte)(num3 & 15);
                num3 = (byte)(num3 + 0x55);
                num3 = (byte)(num3 ^ ((byte)(((byte)i) * 11)));
                num3 = (byte)(num3 ^ num);
                num3 = (byte)(num3 ^ 170);
                num = num3;
            }
        }
    }

    internal class BinaryReaderEx : BinaryReader
    {
        private static byte[] Buffer = new byte[0x100];
        private const int BufferLength = 0x100;

        public BinaryReaderEx(Stream imput)
            : base(imput)
        {
        }

        public BinaryReaderEx(Stream imput, Encoding encoding)
            : base(imput, encoding)
        {
        }

        private string _ReadString(uint bytes)
        {
            string str;
            if (bytes > 0x100)
            {
                str = ReadString(bytes - 0x100);
            }
            else
            {
                str = string.Empty;
            }
            Read(Buffer, 0, (int)bytes);
            return (str + Encoding.UTF7.GetString(Buffer, 0, (int)bytes));
        }

        public byte[] ReadByteArray(uint count)
        {
            byte[] buffer = new byte[count];
            for (uint i = 0; i < count; ++i)
            {
                buffer[i] = ReadByte();
            }
            return buffer;
        }

        public int ReadInt(int bytes)
        {
            switch (bytes)
            {
                case 1:
                    return ReadSByte();

                case 2:
                    return ReadInt16();

                case 4:
                    return ReadInt32();
            }
            throw new ArgumentException();
        }

        public short[] ReadInt16Array(uint count)
        {
            short[] numArray = new short[count];
            for (uint i = 0; i < count; ++i)
            {
                numArray[i] = ReadInt16();
            }
            return numArray;
        }

        public int[] ReadInt32Array(uint count)
        {
            int[] numArray = new int[count];
            for (uint i = 0; i < count; ++i)
            {
                numArray[i] = ReadInt32();
            }
            return numArray;
        }

        public sbyte[] ReadSByteArray(uint count)
        {
            sbyte[] numArray = new sbyte[count];
            for (uint i = 0; i < count; ++i)
            {
                numArray[i] = ReadSByte();
            }
            return numArray;
        }

        public override string ReadString()
        {
            int count = 0;
            for (byte i = ReadByte(); i != 0; i = ReadByte())
            {
                Buffer[count++] = i;
                if (count >= 0x100)
                {
                    break;
                }
            }
            string str = Encoding.UTF7.GetString(Buffer, 0, count);
            if (count == 0x100)
            {
                str = str + ReadString();
            }
            return str;
        }

        public string ReadString(int bytes)
        {
            if (bytes > 0)
            {
                return ReadString((uint)bytes);
            }
            return string.Empty;
        }

        public string ReadString(uint bytes)
        {
            return _ReadString(bytes).TrimEnd(new char[1]);
        }

        public uint ReadUInt(int bytes)
        {
            switch (bytes)
            {
                case 1:
                    return ReadByte();

                case 2:
                    return ReadUInt16();

                case 4:
                    return ReadUInt32();
            }
            throw new ArgumentException();
        }

        public ushort[] ReadUInt16Array(uint count)
        {
            ushort[] numArray = new ushort[count];
            for (uint i = 0; i < count; ++i)
            {
                numArray[i] = ReadUInt16();
            }
            return numArray;
        }

        public uint[] ReadUInt32Array(uint count)
        {
            uint[] numArray = new uint[count];
            for (uint i = 0; i < count; ++i)
            {
                numArray[i] = ReadUInt32();
            }
            return numArray;
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        public long Skip(long offset)
        {
            return Seek(offset, SeekOrigin.Current);
        }

        public long Length
        {
            get
            {
                return BaseStream.Length;
            }
        }

        public long Position
        {
            get
            {
                return BaseStream.Position;
            }
        }
    }
}

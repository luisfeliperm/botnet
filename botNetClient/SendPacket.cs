using System;
using System.IO;
using System.Text;

namespace botNetClient
{
    public class SendPacket
    {
        public MemoryStream mstream = new MemoryStream();
        protected internal void writeB(byte[] value)
        {
            mstream.Write(value, 0, value.Length);
        }
        protected internal void writeD(int value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeD(uint value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeH(short val)
        {
            writeB(BitConverter.GetBytes(val));
        }
        protected internal void writeH(ushort val)
        {
            writeB(BitConverter.GetBytes(val));
        }
        protected internal void writeC(byte value)
        {
            mstream.WriteByte(value);
        }
        protected internal void writeF(double value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeT(float value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeQ(long value)
        {
            writeB(BitConverter.GetBytes(value));
        }
        protected internal void writeS(string value)
        {
            if (value != null)
                writeB(Encoding.Unicode.GetBytes(value));
            writeH(0);
        }
        protected internal void writeS(string name, int count)
        {
            if (name == null)
                return;
            writeB(Encoding.GetEncoding(1251).GetBytes(name));
            writeB(new byte[count - name.Length]);
        }
        protected internal void writeS2(string name, int count)
        {
            if (name == null)
                return;
            writeB(Encoding.Default.GetBytes(name));
            writeB(new byte[count - name.Length]);
        }
    }
}
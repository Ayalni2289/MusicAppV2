using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Net.IO
{
    internal class PacketBuilder
    {
        private MemoryStream memoryStream;

        public PacketBuilder()
        {
            memoryStream = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            memoryStream.WriteByte(opcode);
        }

        public void WriteMessage(string message)
        {
            var messageLength = message.Length;
            memoryStream.Write(BitConverter.GetBytes(messageLength));
            memoryStream.Write(Encoding.ASCII.GetBytes(message));
        }

        public byte[] GetPacketBytes()
        {
            return memoryStream.ToArray();
        }
    }
}

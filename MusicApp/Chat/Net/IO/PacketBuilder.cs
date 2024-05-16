using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Chat.Net.IO
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

        public void WriteString(string message)
        {
            var msgLength = message.Length;
            memoryStream.Write(BitConverter.GetBytes(msgLength));
            memoryStream.Write(Encoding.ASCII.GetBytes(message));
        }

        public byte[] GetPacketBytes()
        {
            return memoryStream.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Chat.Net.IO
{
    class PacketBuilder
    {
        MemoryStream _memoryStream;

        public PacketBuilder()
        {
            _memoryStream = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            _memoryStream.WriteByte(opcode);
        }

        public void WriteString(string message) 
        {
            var msgLength = message.Length;
            _memoryStream.Write(BitConverter.GetBytes(msgLength));
            _memoryStream.Write(Encoding.ASCII.GetBytes(message));
        }

        public byte[] GetPacketBytes()
        {
            return _memoryStream.ToArray();
        }
    }
}

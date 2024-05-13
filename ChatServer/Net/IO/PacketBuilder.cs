using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Net.IO
{
    class PacketBuilder
    {
        MemoryStream memoryStream;

        public PacketBuilder() {
            memoryStream = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            memoryStream.WriteByte(opcode);
        }

        public void WriteMessage (string message) 
        {
            var msgLength = message.Length;
            memoryStream.Write(BitConverter.GetBytes(msgLength));
            memoryStream.Write(Encoding.ASCII.GetBytes(message));
        }

        public byte[] getPacketBytes()
        {
            return memoryStream.ToArray();
        }

        internal byte[] GetPacketBytes()
        {
            throw new NotImplementedException();
        }
    }
}

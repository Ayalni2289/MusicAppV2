using System.Text;

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
            var msgLength = message.Length;
            memoryStream.Write(BitConverter.GetBytes(msgLength), 0, sizeof(int));
            memoryStream.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
        }

        public byte[] GetPacketBytes()
        {
            return memoryStream.ToArray();
        }
    }
}


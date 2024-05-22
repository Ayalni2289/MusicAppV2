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

        public void WriteOperationCode(byte operationcode)
        {
            memoryStream.WriteByte(operationcode);
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


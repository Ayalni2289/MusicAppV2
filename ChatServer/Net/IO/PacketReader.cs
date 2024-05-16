using System.Net.Sockets;
using System.Text;

namespace ChatServer.Net.IO
{
    internal class PacketReader : BinaryReader
    {
        private NetworkStream networkStream;
        public PacketReader(NetworkStream networkSystem) : base(networkSystem)
        {
            networkStream = networkSystem;
        }

        public string ReadMessage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            networkStream.Read(msgBuffer, 0, length);

            var message = Encoding.ASCII.GetString(msgBuffer);

            return message;
        }
    }
}

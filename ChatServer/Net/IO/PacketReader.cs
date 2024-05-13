using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Net.IO
{
    class PacketReader : BinaryReader
    {
        private NetworkStream networkStream;
        public PacketReader(NetworkStream _networkSystem) : base(_networkSystem)
        {
            networkStream = _networkSystem;
        }

        public String ReadMessage()
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

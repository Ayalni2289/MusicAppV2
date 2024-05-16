using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Chat.Net.IO
{
    internal class PacketReader : BinaryReader
        {
        private NetworkStream networkStream;
        public PacketReader(NetworkStream network) : base(network)
        {
            networkStream = network;
        }

        public string ReadMessage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            networkStream.Read(msgBuffer, 0, length);

            var msg = Encoding.ASCII.GetString(msgBuffer);

            return msg;
        }
    }
}


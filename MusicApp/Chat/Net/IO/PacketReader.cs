﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Chat.Net.IO
{
    class PacketReader : BinaryReader
        {
        private NetworkStream _networkStream;
        public PacketReader(NetworkStream network) : base(network)
        {
            _networkStream = network;
        }

        public String ReadMessage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            _networkStream.Read(msgBuffer, 0, length);

            var msg = Encoding.ASCII.GetString(msgBuffer);

            return msg;
        }
    }
}


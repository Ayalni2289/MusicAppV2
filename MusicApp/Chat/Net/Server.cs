﻿using MusicApp.Chat.Net.IO;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Windows;

namespace MusicApp.Chat.Net
{
    class Server
    {
        TcpClient _client;
        public PacketReader PacketReader;

        public event Action connectedEvent;
        public event Action msgReceivedEvent;
        public event Action disconnectedEvent;

        public Server()
        {
            _client = new TcpClient();
        }

        public void ConnectToServer(String username)
        {

            if (!_client.Connected)
            {
                _client.Connect("127.0.0.1", 7891);
                PacketReader = new PacketReader(_client.GetStream());
                if (!string.IsNullOrEmpty(username))
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteString(username);
                    _client.Client.Send(connectPacket.GetPacketBytes());
                }
                ReadPackets();

            }
        }
        public enum OpCode
        {
            Connect = 1,
            Message = 2,
            Disconnect = 3
        }

        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var opcode = PacketReader.ReadByte();
                    switch (opcode)
                    {
                        case (byte)OpCode.Connect:
                            connectedEvent?.Invoke();
                            break;
                        case (byte)OpCode.Message:
                            msgReceivedEvent?.Invoke();
                            break;
                        case (byte)OpCode.Disconnect:
                            disconnectedEvent?.Invoke();
                            break;
                        default:
                            break;
                    }
                }
            });
        }

        public void SendMessageToServer(String messsage)
        {
            try
            {
                var messagePacket = new PacketBuilder();
                messagePacket.WriteOpCode(2);
                messagePacket.WriteString(messsage);
                _client.Client.Send(messagePacket.GetPacketBytes());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please log in before sending a message", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

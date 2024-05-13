using System.Net.Sockets;
using System.Windows;
using MusicApp.Chat.Net.IO;

namespace MusicApp.Chat.Net
{
    internal class Server
    {
        public TcpClient Client;
        public PacketReader PacketReader;

        public event Action ConnectedEvent;
        public event Action MsgReceivedEvent;
        public event Action DisconnectedEvent;

        public Server()
        {
            Client = new TcpClient();
        }

        public void ConnectToServer(string username)
        {
            if (!Client.Connected)
            {
                Client.Connect("127.0.0.1", 7891);
                PacketReader = new PacketReader(Client.GetStream());
                if (!string.IsNullOrEmpty(username))
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteString(username);
                    Client.Client.Send(connectPacket.GetPacketBytes());
                }
                ReadPackets();
            }
        }

        private void ReadPackets()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    byte opcode = PacketReader.ReadByte();
                    ProcessOpcode(opcode);
                }
            });
        }

        private void ProcessOpcode(byte opcode)
        {
            switch (opcode)
            {
                case OpCode.Connected:
                    OnConnected();
                    break;
                case OpCode.MessageReceived:
                    OnMessageReceived();
                    break;
                case OpCode.Disconnected:
                    OnDisconnected();
                    break;
                default:
                    // Handle unknown opcode
                    break;
            }
        }

        private void OnConnected()
        {
            ConnectedEvent?.Invoke();
        }

        private void OnMessageReceived()
        {
            MsgReceivedEvent?.Invoke();
        }

        private void OnDisconnected()
        {
            DisconnectedEvent?.Invoke();
        }


        public void SendMessageToServer(string messsage)
        {
            try
            {
                var messagePacket = new PacketBuilder();
                messagePacket.WriteOpCode(2);
                messagePacket.WriteString(messsage);
                Client.Client.Send(messagePacket.GetPacketBytes());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please log in before sending a message", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

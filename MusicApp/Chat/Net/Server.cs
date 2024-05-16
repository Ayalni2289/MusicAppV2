using System.Net.Sockets;
using System.Windows;
using MusicApp.Chat.Net.IO;

namespace MusicApp.Chat.Net
{
    internal class Server : IDisposable
    {
        private TcpClient client;
        public PacketReader PacketReader;

        public event Action ConnectedEvent;
        public event Action MsgReceivedEvent;
        public event Action DisconnectedEvent;

        public Server()
        {
            client = new TcpClient();
        }

        public void ConnectToServer(string username)
        {
            if (!client.Connected)
            {
                client.Connect("127.0.0.1", 7891);
                PacketReader = new PacketReader(client.GetStream());
                if (!string.IsNullOrEmpty(username))
                {
                    var connectPacket = new PacketBuilder();
                    connectPacket.WriteOpCode(0);
                    connectPacket.WriteString(username);
                    client.Client.Send(connectPacket.GetPacketBytes());
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
                            ConnectedEvent?.Invoke();
                            break;
                        case (byte)OpCode.Message:
                            MsgReceivedEvent?.Invoke();
                            break;
                        case (byte)OpCode.Disconnect:
                            DisconnectedEvent?.Invoke();
                            break;
                        default:
                            break;
                    }
                }
            });
        }

        public void SendMessageToServer(string messsage)
        {
            try
            {
                var messagePacket = new PacketBuilder();
                messagePacket.WriteOpCode(2);
                messagePacket.WriteString(messsage);
                client.Client.Send(messagePacket.GetPacketBytes());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please log in before sending a message", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

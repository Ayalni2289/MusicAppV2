using System.Net.Sockets;
using ChatServer.Net.IO;

namespace ChatServer
{
    internal class Client
    {
        public string Username { get; set; }
        public Guid UID { get; set; }
        public TcpClient ClientSocket { get; set; }

        private PacketReader packetReader;
        public Client(TcpClient client)
        {
            ClientSocket = client;
            UID = Guid.NewGuid();
            packetReader = new PacketReader(ClientSocket.GetStream());
            var opcode = packetReader.ReadByte();
            // This operation code correspond to the receiving of the packet in the server
            if (opcode == 0)
            {
                Username = packetReader.ReadMessage();
                Console.WriteLine($"[{DateTime.Now}]: Client has connected with the username: {Username}");
            }
            else
            {
                Console.WriteLine("Quitting conection");
                ClientSocket.Close();
            }
            Task.Run(() => Process());
        }
        private void Process()
        {
            while (true)
            {
                try
                {
                    var opcode = packetReader.ReadByte();
                    switch (opcode)
                    {
                        case 2:
                            var msg = packetReader.ReadMessage();
                            Console.WriteLine($"[{DateTime.Now}]: Message Received! {msg}");
                            Program.BroadcastMessage($"[{DateTime.Now}]: [{Username}]: {msg}");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"[{UID.ToString()}]: Has disconnected!");
                    Program.BroadcastDisconnect(UID.ToString());
                    ClientSocket.Close();
                }
            }
        }
    }
}

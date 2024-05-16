using System.Net;
using System.Net.Sockets;
using ChatServer.Net.IO;

namespace ChatServer
{
    internal class Program
    {
        private static List<Client>? users;
        private static TcpListener? listener;
        private static void Main(string[] args)
        {
            users = new List<Client>();
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            listener.Start();
            while (true)
            {
                var client = new Client(listener.AcceptTcpClient());
                users.Add(client);

                /* Broadcast the conection to everyone on the server */
                BroadcastConnection();
            }
        }

        private static void BroadcastConnection()
        {
            foreach (var user in users)
            {
                foreach (var usr in users)
                {
                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1);
                    broadcastPacket.WriteMessage(usr.Username);
                    broadcastPacket.WriteMessage(usr.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }

        public static void BroadcastMessage(string message)
        {
            foreach (var user in users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(2);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }
        public static void BroadcastDisconnect(string uid)
        {
            var disconnectedUser = users.Where(x => x.UID.ToString() == uid).FirstOrDefault();
            users.Remove(disconnectedUser);
            foreach (var user in users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(3);
                broadcastPacket.WriteMessage(uid);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }

            BroadcastMessage($"[{disconnectedUser.Username}] Disconnected");
        }
    }
}

using ChatServer;
using ChatServer.Net.IO;

internal static class ChatServerLogicHelpers
{
    private static List<Client> users;

    public static void AddUser(Client user)
    {
        users.Add(user);
        BroadcastConnection();
    }

    public static void BroadcastConnection()
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

        BroadcastMessage($"[{disconnectedUser.Username}] Connection Error!");
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

    public static void InitializeUsersList()
    {
        users = new List<Client>();
    }
}
using System.Collections.ObjectModel;
using System.Windows;
using MusicApp.Chat.MVVM.Model;
using MusicApp.Chat.Net;
using MusicApp.Chat.MVVM.Core;

namespace MusicApp.Chat.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public RelayCommand ConnectToServerCommand { get; set; }
        public RelayCommand SendMessageCommand { get; set; }

        private Server server;
        public string Username { get; set; }
        public string Message { get; set; }
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            server = new Server();
            server.ConnectedEvent += UserConnected;
            server.MsgReceivedEvent += MessageReceived;
            server.DisconnectedEvent += RemoveUser;

            ConnectToServerCommand = new RelayCommand(o => server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
            SendMessageCommand = new RelayCommand(o => server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }

        private void MessageReceived()
        {
            var msg = server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
        }
        private void UserConnected()
        {
            var user = new UserModel
            {
                UserName = server.PacketReader.ReadMessage(),
                UID = server.PacketReader.ReadMessage(),
            };

            if (!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }

        private void RemoveUser()
        {
            var uid = server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace LetsTalk
{
    //[Xamarin.Forms.ContentProperty("Content")]
    public partial class ChatPage
    {
        private ChatHistory history;

        public ChatPage()
        {
            InitializeComponent();

             history = new ChatHistory();

            history.StartListening();
            
            BindingContext = history;

        }

        private void SendBtn_Clicked(object sender, EventArgs ea)
        {
            string chatMsg = entryTextBox.Text.Trim();
            history.SendMessage(chatMsg);
            entryTextBox.Text = String.Empty;
        }

    }

    public class ChatHistory :INotifyPropertyChanged
    {
        private string _content = "Thanx Nicholas";
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private IHubProxy _chatHubProxy;
        readonly HubConnection _hubConnection = new HubConnection("http://laughatme.azurewebsites.net/");

        public async void StartListening()
        {
            // Create a proxy to the 'ChatHub' SignalR Hub
            _chatHubProxy = _hubConnection.CreateHubProxy("MessengerHub");

            //Wire up a handler for the 'UpdateChatMessage' for the server to be called on our client
            _chatHubProxy.On<string>("newMessage", message =>
            {
                Debug.WriteLine("{0}\r\n", message);
                Content = Content + Environment.NewLine + message;
            });

            try
            {
                // Start the connection
                await _hubConnection.Start();
            }
            catch (Exception ex)
            {

            }
        }

        public async void SendMessage(string message)
        {
            await _hubConnection.Start();
            await _chatHubProxy.Invoke("Send", message);  
        }
    }
}

using System;
using DevExpress.XtraEditors;
using Microsoft.AspNet.SignalR.Client;

namespace MessengeRClient
{
    public partial class Form1 : XtraForm
    {
        private IHubProxy _chatHubProxy;
        readonly HubConnection _hubConnection = new HubConnection("http://laughatme.azurewebsites.net/");
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _hubConnection.Start();
            await _chatHubProxy.Invoke("Send", memoEditEnterMsg.Text);
            memoEditEnterMsg.Text = String.Empty;
        }

        private async void StartListening()
        {
            // Create a proxy to the 'ChatHub' SignalR Hub
            _chatHubProxy = _hubConnection.CreateHubProxy("MessengerHub");

             //Wire up a handler for the 'UpdateChatMessage' for the server to be called on our client
            _chatHubProxy.On<string>("newMessage", message =>
                memoEditLog.EditValue += string.Format("{0}\r\n", message));
            try
            {
                // Start the connection
                await _hubConnection.Start();
            }
            catch (Exception ex)
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartListening();
            memoEditEnterMsg.Select();
        }
    }
}

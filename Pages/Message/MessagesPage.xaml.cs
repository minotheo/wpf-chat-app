using ChatApp.Database;
using ChatApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp.Pages.Message
{
    public partial class MessagesPage : Page
    {
        private Timer timer;
        private DateTime lastUpdateTime; // Store the timestamp of the last update

        public MessagesPage()
        {
            InitializeComponent();
            LoadMessage();

            lastUpdateTime = DateTime.MinValue;

            // Create a timer with a specified interval (e.g., 5 seconds)
            timer = new Timer(5000);
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // Update the UI on the main thread
                LoadMessage();
            });
        }

        private void LoadMessage()
        {
            List<Messages> messages = DB.entities.Messages.ToList();

            messageListView.ItemsSource = messages;

            // Scroll to the last added message
            if (messages.Count > 0)
            {
                var lastMessage = messages[messages.Count - 1];
                messageListView.ScrollIntoView(lastMessage);
            }
        }

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                addMessage(messageInput.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addMessage(messageInput.Text);
        }

        private void addMessage(string message)
        {
            if (message.Length > 0 || message.Length <= 256)
            {
                Messages newMessage = new Messages
                {
                    Message = message,
                    User = PageManager.CurrentUser
                };

                DB.entities.Messages.Add(newMessage);
                DB.entities.SaveChanges();

                LoadMessage();

                messageInput.Text = "";
            }
        }
    }
}

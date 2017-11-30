using System.Windows;
using TelegramBot.BotLogics;

namespace TelegramBot
{
    public partial class MainWindow : Window
    {
        private TelegramBotClass _bot;

        public MainWindow()
        {
            InitializeComponent();
            CustomHttpClient.SetMonitorControls(MyRequestTextBlock, ResponseTextBox);
            _bot = new TelegramBotClass();
            _bot.Start();
        }
    }
}

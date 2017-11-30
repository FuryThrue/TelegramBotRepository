using Newtonsoft.Json.Linq;
using System;
using System.Windows.Threading;

namespace TelegramBot.BotLogics
{
    class UpdatesReciever
    {
        private DispatcherTimer _dispatcherTimer;
        private int _offset;
        private string _parameters;
        public delegate void _updatesDelegate(JToken updates);
        public event _updatesDelegate OnUpdatesRecieved;

        public UpdatesReciever(string parameters)
        {
            _parameters = parameters;
            _dispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            _dispatcherTimer.Tick += (sender, e) =>
            {
                GetUpdatesAsync();
            };
        }

        public void Start()
        {
            GetUpdatesAsync();
            _dispatcherTimer.Start();
        }

        public void Stop()
        {
            _dispatcherTimer.Stop();
            CustomHttpClient.CancelRequest();
        }

        private async void GetUpdatesAsync()
        {
            var parameters = $@"offset={_offset}&allowed_updates={_parameters}";
            var updatesJToken = await CustomHttpClient.MakeRequest("getUpdates", parameters);

            var lastUpdateIdJToken = updatesJToken["result"].Last?["update_id"];
            if (lastUpdateIdJToken != null)
            {
                var lastUpdateId = int.Parse(lastUpdateIdJToken.ToString());
                _offset = lastUpdateId + 1;
            }
            OnUpdatesRecieved(updatesJToken);
        }
    }
}

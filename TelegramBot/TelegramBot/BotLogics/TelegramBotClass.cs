using TelegramBot.BotLogics.UpdatesHandling;

namespace TelegramBot.BotLogics
{
    class TelegramBotClass
    {
        private UpdatesReciever _updateReciever;
        private UpdatesHandler _updateHandler;

        public TelegramBotClass()
        {
            _updateHandler = new UpdatesHandler();
            var parameters = _updateHandler.GetAvailableParameters();

            _updateReciever = new UpdatesReciever(parameters);
            _updateReciever.OnUpdatesRecieved += _updateHandler.UpdatesHandle;
        }

        public void Start()
        {
            _updateReciever.Start();
        }

        public void Stop()
        {
            _updateReciever.Stop();
        }
    }
}

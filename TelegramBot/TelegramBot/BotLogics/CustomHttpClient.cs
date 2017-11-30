using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TelegramBot
{
    static class CustomHttpClient
    {
        private static string _botAccessToken;
        private static string _baseBotApiUrl = $"https://api.telegram.org/bot{_botAccessToken}/";
        private static HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri(_baseBotApiUrl) };
        private static TextBlock _requestTextBlock;
        private static TextBox _responseTextBox;

        public static async Task<JToken> MakeRequest(string requestMethod, string parameters)
        {
            var requestUri = $"{requestMethod}?{parameters}";
            _requestTextBlock.Text = requestUri;
            var response = await GetResponse(requestUri);
            _responseTextBox.Text = response;
            return JToken.Parse(response);
        }

        public static async Task<string> GetResponse(string requestUri)
        {
            return await _httpClient.GetStringAsync(requestUri);
        }

        public static void CancelRequest()
        {
            _httpClient.CancelPendingRequests();
        }

        public static void SetMonitorControls(TextBlock myRequest, TextBox response)
        {
            _requestTextBlock = myRequest;
            _responseTextBox = response;
        }
    }
}

using System.Web;

namespace SingleSignOn.Backend.Services
{
    public class SteamVerifyService
    {
        private readonly HttpClient http_client_;

        public SteamVerifyService()
        {
            http_client_ = new HttpClient();
        }

        public async Task<bool> Verify(string query_string)
        {
            var query_parameters = HttpUtility.ParseQueryString(query_string);
            query_parameters.Set("openid.mode", "check_authentication");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "https://steamcommunity.com/openid/login" + "?" + query_parameters.ToString()
            );
            var response = await http_client_.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return content.Contains("is_valid:true");
        }
    }
}

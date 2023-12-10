using WebAplicationTestMVC.Interface;
using WebAplicationTestMVC.Models;

namespace WebAplicationTestMVC.Services
{
    public class ApiService : IApiService
    {
        HttpClient client = new HttpClient();

        public ApiService()
        {
            //Uri uri = new Uri("http://localhost:44303/");
            Uri uri = new Uri("https://localhost:7106/");

            client.BaseAddress = uri;
        }

        public Task AddAttempt(int time, int correctsAnswers, int wrongAnswers)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAttempts(string setName)
        {
            throw new NotImplementedException();
        }

        public async Task<StatsViewModel?> GetStats(string setName)
        {

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress + "Attempt/GetStats/" + setName);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<StatsViewModel>();
            }

            return null;
        }
    }
}

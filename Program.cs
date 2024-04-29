using System.Net.Http.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ThreadPool.QueueUserWorkItem(GetUrl);
        Console.ReadLine();
    }

    private async static void GetUrl(object state){
        HttpClient client = new HttpClient();
        int number = 3;
        client.BaseAddress = new Uri("https://api.thecatapi.com/v1/");
        var response = await client.GetAsync("images/search?limit="+ number +"&breed_ids=beng&api_key=live_eCz0IGWhoAhklyg10ShgXKAq9Tqh8QHotBXqaxLAaRfBdIg5ZnFjmF448mCQwVMF");
        
        Console.WriteLine($"Status Code: {response.StatusCode} Content: {await response.Content.ReadAsStringAsync()}");
        var content = await response.Content.ReadFromJsonAsync<List<Cat>>();
        foreach (var cat in content)
                {
                    Console.WriteLine($"Cat URL: {cat.Url}");
                }
        Console.ReadLine();

    }

    public class Cat
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

    }
}
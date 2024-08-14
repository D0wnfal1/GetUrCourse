using System.Text.Json;
using System.Text.Json.Serialization;

namespace GetUrCourse.Services.CourseAPI;

public class DopplerClient
{
    [JsonPropertyName("DOPPLER_PROJECT")]
    public string DopplerProject { get; set; }

    [JsonPropertyName("DOPPLER_ENVIRONMENT")]
    public string DopplerEnvironment { get; set; }

    [JsonPropertyName("DOPPLER_CONFIG")]
    public string DopplerConfig { get; set; }
    
    [JsonPropertyName("DB_CONNECTION")]
    public string DbConnection { get; set; }

    private static HttpClient client = new ();

    public static async Task<DopplerClient> FetchSecretsAsync(IConfiguration configuration)
    {
        var dopplerToken = configuration.GetSection("DOPPLER_TOKEN").Value;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", dopplerToken);
        var streamTask = client.GetStreamAsync("https://api.doppler.com/v3/configs/config/secrets/download?format=json");
        var secrets = await JsonSerializer.DeserializeAsync<DopplerClient>(await streamTask);

        return secrets;
    }

}
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public static class AviationStackAPI
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = "c67231b0c180dea07d0ee68bc941e0af";
    private static Dictionary<string, (string origin, string destination)> cache = new();

    public static async Task<(string origin, string destination)> GetFlightRouteAsync(string callsign)
    {
        callsign = callsign.Trim();

        if (string.IsNullOrEmpty(callsign))
        {
            return ("NA", "NA");
        }

        if (cache.ContainsKey(callsign))
        {
            return cache[callsign];
        }           

        string requestUrl = $"http://api.aviationstack.com/v1/flights?access_key={apiKey}&flight_icao={callsign}";

        try
        {
            var response = await client.GetAsync(requestUrl).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            var data = json["data"]?.First;
            if (data != null)
            {
                string origin = data["departure"]?["airport"]?.ToString() ?? "NA";
                string destination = data["arrival"]?["airport"]?.ToString() ?? "NA";

                cache[callsign] = (origin, destination);
                return (origin, destination);
            }
        }
        catch
        {
            // Optional: log error
        }

        return ("", "");
    }
}
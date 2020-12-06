using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using DTO;
using Newtonsoft.Json;

namespace UKTidalAPISerwis
{
    public class Requests
    {
        public async Task<Station> GetStation(string primaryKey, string stationId)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", primaryKey);

            var uri = "https://admiraltyapi.azure-api.net/uktidalapi/api/V1/Stations/"+ stationId + "?" + queryString;

            var response = await client.GetAsync(uri);
            var res = await response.Content.ReadAsStringAsync();
            Station station = JsonConvert.DeserializeObject<Station>(res);
            return station;
        }

        public async Task<List<Events>> GetTidalEvents(string primaryKey, string stationId, int duration)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", primaryKey);

            // Request parameters
            queryString["duration"] = duration.ToString();
            var uri = "https://admiraltyapi.azure-api.net/uktidalapi/api/V1/Stations/" +stationId + "/TidalEvents?" + queryString;

            var response = await client.GetAsync(uri);
            var res = await response.Content.ReadAsStringAsync();
            List<Events> events = JsonConvert.DeserializeObject<List<Events>>(res);
            return events;
        }

        public async Task<StationsList> GetStationsList(string primaryKey)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", primaryKey);

            // Request parameters
            var uri = "https://admiraltyapi.azure-api.net/uktidalapi/api/V1/Stations?";

            var response = await client.GetAsync(uri);
            var res = await response.Content.ReadAsStringAsync();
            StationsList list = JsonConvert.DeserializeObject<StationsList>(res);
            foreach (Station item in list.Features)
                item.Events = await GetTidalEvents(primaryKey, item.Properties.Id, 6);
            return list;
        }
    }
}

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
        /// <summary>
        /// Gets station from UK Tidal API
        /// </summary>
        /// <param name="primaryKey">Primary key to get access to api</param>
        /// <param name="stationId">Defines which stations data to be downloaded</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets events assigned to specific station
        /// </summary>
        /// <param name="primaryKey">Primary key to get access to api</param>
        /// <param name="stationId">Defines which stations data to be downloaded</param>
        /// <param name="duration">Defines range of days</param>
        /// <returns></returns>
        public async Task<List<Events>> GetTidalEvents(string primaryKey, string stationId, int duration)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", primaryKey);

            // Request parameters
            queryString["duration"] = duration.ToString();
            var uri = "https://admiraltyapi.azure-api.net/uktidalapi/api/V1/Stations/" + stationId + "/TidalEvents?" + queryString;

            var response = await client.GetAsync(uri);
            var res = await response.Content.ReadAsStringAsync();
            List<Events> events = JsonConvert.DeserializeObject<List<Events>>(res);
            return events;
        }

        /// <summary>
        /// Gets all stations list
        /// </summary>
        /// <param name="primaryKey">Primary key to get access to api</param>
        /// <returns></returns>
        public async Task<StationsList> GetStationsList(string primaryKey)
        {
            var client = new HttpClient();

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

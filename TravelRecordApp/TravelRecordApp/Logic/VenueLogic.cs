﻿using TravelRecordApp.Model;

namespace TravelRecordApp.Logic
{
    public class VenueLogic
    {
        public static async Task<List<Venue>> GetVenues(double latitute, double longitude)
        {
            var venues = new List<Venue>();

            var url = VenueRoot.GenerateUrl(latitute, longitude);

            //using (var client = new HttpClient())
            //{
            //    var response = await client.GetAsync(url);
            //    var json = await response.Content.ReadAsStringAsync();

            //    var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

            //    venues = venueRoot.Response.Venues as List<Venue>;
            //}

            return venues;
        }
    }
}

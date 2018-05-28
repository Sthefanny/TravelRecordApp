﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{

    public class Location
    {
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Distance { get; set; }
        public string PostalCode { get; set; }
        public string Cc { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public IList<string> FormattedAddress { get; set; }
        public string CrossStreet { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public string ShortName { get; set; }
        public bool Primary { get; set; }
    }

    public class Venue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public IList<Category> Categories { get; set; }

        public static async Task<List<Venue>> GetVenues(double latitute, double longitude)
        {
            var venues = new List<Venue>();

            var url = VenueRoot.GenerateUrl(latitute, longitude);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venueRoot.Response.Venues as List<Venue>;
            }

            return venues;
        }
    }

    public class Response
    {
        public IList<Venue> Venues { get; set; }
    }

    public class VenueRoot
    {
        public Response Response { get; set; }

        public static string GenerateUrl(double latitude, double longitude)
        {
            var date = DateTime.Now.ToString("yyyyMMdd");
            return string.Format(Constants.VenueSearch, latitude, longitude, Constants.ClientId, Constants.ClientSecret, date);
        }
    }
}

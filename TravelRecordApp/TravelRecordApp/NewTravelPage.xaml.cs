using Plugin.Geolocator;
using System;
using System.Linq;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await Venue.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var post = GetPost();
                Post.Insert(post);

                await DisplayAlert("Success", "Experience succesfully inserted", "Ok");
            }
            catch (NullReferenceException nre)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
            }
        }

        private Post GetPost()
        {
            var selectedVenue = venueListView.SelectedItem as Venue;
            var firstCategory = selectedVenue?.Categories.FirstOrDefault();
            var post = new Post
            {
                Experience = experienceEntry.Text,
                CategoryId = firstCategory?.Id,
                CategoryName = firstCategory?.Name,
                Address = selectedVenue.Location.Address,
                Distance = selectedVenue.Location.Distance,
                Latitude = selectedVenue.Location.Lat,
                Longitude = selectedVenue.Location.Lng,
                VenueName = selectedVenue.Name,
                UserId = App.User.Id
            };
            return post;
        }
    }
}
using Plugin.Geolocator;
using System;
using System.Linq;
using TravelRecordApp.Logic;
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

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
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

                //using (var conn = new SQLiteConnection(App.DatabaseLocation))
                //{
                //    conn.CreateTable<Post>();
                //    var rows = conn.Insert(post);

                //    if (rows > 0)
                //        DisplayAlert("Success", "Experience succesfully inserted", "Ok");
                //    else
                //        DisplayAlert("Failure", "Experience failed to be inserted", "Ok");
                //}

                await App.MobileService.GetTable<Post>().InsertAsync(post);
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
    }
}
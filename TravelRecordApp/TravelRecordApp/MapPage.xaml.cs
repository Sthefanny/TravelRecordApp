
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Xamarin.Forms.Maps.Position;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            locator.PositionChanged += Locator_PositionChanged;
            await locator.StartListeningAsync(TimeSpan.Zero, 100);

            var position = await locator.GetPositionAsync();
            SetMapPosition(position);
        }

        private void Locator_PositionChanged(object sender, PositionEventArgs positionEventArgs)
        {
            SetMapPosition(positionEventArgs.Position);
        }

        private void SetMapPosition(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Position(position.Latitude, position.Longitude);
            var span = new MapSpan(center, 2, 2);
            locationsMap.MoveToRegion(span);
        }
    }
}
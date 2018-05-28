﻿using System;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var assembly = typeof(MainPage);

            iconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", assembly);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (await User.TryLogin(emailEntry.Text, passwordEntry.Text))
                await Navigation.PushAsync(new HomePage());
            else
                await DisplayAlert("Error", "There was an error logging you in", "Ok");
        }

        private void registerUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}

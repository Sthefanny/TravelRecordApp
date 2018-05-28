using System;
using System.Linq;
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
            var isEmailEmpty = string.IsNullOrWhiteSpace(emailEntry.Text);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {

            }
            else
            {
                var user = (await App.MobileService.GetTable<User>().Where(u => u.Email == emailEntry.Text).ToListAsync()).FirstOrDefault();

                if (user != null)
                {
                    App.User = user;
                    if (user.Password == passwordEntry.Text)
                        await Navigation.PushAsync(new HomePage());
                    else
                        await DisplayAlert("Error", "Email or password are incorrect", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "There was an error logging you in", "Ok");
                }
            }
        }

        private void registerUserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}

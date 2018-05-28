using System.Collections.Generic;
using System.Linq;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using (var conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            var postTable = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.User.Id).ToListAsync();

            var categories = postTable.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

            var categoriesCount = new Dictionary<string, int>();

            foreach (var category in categories)
            {
                var count = postTable.Where(p => p.CategoryName == category).ToList().Count;

                if (!string.IsNullOrWhiteSpace(category))
                    categoriesCount.Add(category, count);
            }

            categoriesListView.ItemsSource = categoriesCount;

            postCountLabel.Text = postTable.Count.ToString();
            //}
        }
    }
}
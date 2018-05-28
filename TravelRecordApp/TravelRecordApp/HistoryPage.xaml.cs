using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //using (var conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Post>();
            //    var posts = conn.Table<Post>().ToList();
            //    PostListView.ItemsSource = posts;
            //}

            //TODO: Pass to a separate class
            var posts = await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.User.Id).ToListAsync();
            PostListView.ItemsSource = posts;
        }
    }
}
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private RegisterViewModel viewModel;

        public RegisterPage()
        {
            InitializeComponent();

            viewModel = new RegisterViewModel();
            BindingContext = viewModel;
        }
    }
}
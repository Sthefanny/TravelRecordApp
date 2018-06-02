using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TravelRecordApp.Annotations;

namespace TravelRecordApp.Model
{
    public class User : INotifyPropertyChanged
    {
        private string _id;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }


        public static async Task<bool> TryLogin(string email, string password)
        {
            var isEmailEmpty = string.IsNullOrWhiteSpace(email);
            var isPasswordEmpty = string.IsNullOrWhiteSpace(password);

            if (isEmailEmpty || isPasswordEmpty)
                return false;

            var user = await GetUserByEmail(email);

            if (user != null)
            {
                App.User = user;
                if (user.Password == password)
                    return true;

                return false;
            }

            return false;
        }

        public static async Task<User> GetUserByEmail(string email)
        {
            return (await App.MobileService.GetTable<User>().Where(u => u.Email == email).ToListAsync()).FirstOrDefault();
        }

        public static async void Register(User user)
        {
            await App.MobileService.GetTable<User>().InsertAsync(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

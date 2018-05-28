using System.Linq;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

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
    }
}

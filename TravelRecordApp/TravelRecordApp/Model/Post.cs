using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TravelRecordApp.Annotations;

namespace TravelRecordApp.Model
{
    public class Post : INotifyPropertyChanged
    {

        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string experience;

        public string Experience
        {
            get { return experience; }
            set
            {
                experience = value;
                OnPropertyChanged("Experience");
            }
        }

        private string venueName;

        public string VenueName
        {
            get { return venueName; }
            set
            {
                venueName = value;
                OnPropertyChanged("VenueName");
            }
        }
        private string categoryId;

        public string CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                OnPropertyChanged("CategoryId");
            }
        }
        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged("Longitude");
            }
        }
        private int distance;

        public int Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }
        private string userId;

        public string UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public static async void Insert(Post post)
        {
            await App.MobileService.GetTable<Post>().InsertAsync(post);
        }

        public static async Task<List<Post>> Read()
        {
            return await App.MobileService.GetTable<Post>().Where(p => p.UserId == App.User.Id).ToListAsync();
        }

        public static Dictionary<string, int> PostCategories(List<Post> posts)
        {
            var categories = posts.OrderBy(p => p.CategoryId).Select(p => p.CategoryName).Distinct().ToList();

            var categoriesCount = new Dictionary<string, int>();

            foreach (var category in categories)
            {
                var count = posts.Where(p => p.CategoryName == category).ToList().Count;

                if (!string.IsNullOrWhiteSpace(category))
                    categoriesCount.Add(category, count);
            }

            return categoriesCount;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        [MaxLength(250)]
        public string Experience { get; set; }
        public string VenueName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
        public string UserId { get; set; }

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
    }
}

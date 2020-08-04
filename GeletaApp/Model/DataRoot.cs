using GeletaApp.Helpers;
using System.Collections.Generic;

namespace GeletaApp.Model
{
    public class DataRoot
    {
        public IList<Bouquet> bouquets { get; set; }
        public IList<Flower> flowers { get; set; }
        public IList<Home_Stuff> home_stuff { get; set; }
        public string product_image { get; set; }
        public IList<Flower_Type> flowers_type { get; set; }
        public IList<Occassion_Type> occasions_type { get; set; }
        public IList<Home_Stuff_Type> home_stuff_type { get; set; }
        public IList<BouquetConsist> bouquets_consist { get; set; }
        public IList<AssignedOccasions> assigned_occasions { get; set; }
        public User user_data { get; set; }
        public IList<UserAddress> user_addresses { get; set; }
        public string userToken { get; set; }
        public string message { get; set; }
        public int last_id { get; set; }

        public static string GenerateURL(string function)
        {
            return string.Format(Constants.API_LINK, Constants.APP_LOGIN, Constants.APP_PASSWORD, function);
        }
    }
}

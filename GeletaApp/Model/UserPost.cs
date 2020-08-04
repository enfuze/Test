using SQLite;

namespace GeletaApp.Model
{
    public class UserPost
    {

        public string Token { get; set; }
        [PrimaryKey]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Phone_number { get; set; }
        public int Postal_code { get; set; }
        public string Discount_code { get; set; }
    }
}

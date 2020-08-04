using SQLite;

namespace GeletaApp.Model
{
    public class UserAddressPost
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Postal_code { get; set; }
        public string Name { get; set; }
        public int Phone_number { get; set; }
    }
}

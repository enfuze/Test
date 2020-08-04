using SQLite;

namespace GeletaApp.Model
{
    public class Home_Stuff_TypePost
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Type_name { get; set; }

    }
}

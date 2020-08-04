namespace GeletaApp.Model
{
    public class Home_Stuff
    {
        // atributai iš mažųjų raidžių nes DUOMBAZĖJE tokie pat yra.
        public int id { get; set; }
        public string home_stuff_name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string home_stuff_image { get; set; }
        public string home_stuff_image_sh { get; set; }
        public int discount { get; set; }
        public int gender { get; set; }
        public int home_stuff_type_id { get; set; }
    }
}

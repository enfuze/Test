namespace GeletaApp.Model
{
    public class Bouquet
    {
        // atributai iš mažųjų raidžių nes DUOMBAZĖJE tokie pat yra.
        public int id { get; set; }
        public string bouquet_name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string bouquet_image { get; set; }
        public string bouquet_image_sh { get; set; }
        public int discount { get; set; }
        public int gender { get; set; }
    }

}

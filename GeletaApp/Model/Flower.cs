using System;
using System.Collections.Generic;
using System.Text;

namespace GeletaApp.Model
{
    public class Flower
    {
        // atributai iš mažųjų raidžių nes DUOMBAZĖJE tokie pat yra. [objektas]
        public int id { get; set; }
        public string flower_name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string flower_image { get; set; }
        public string flower_image_sh { get; set; }
        public int discount { get; set; }
        public int gender { get; set; }
        public int flower_type_id { get; set; }
    }
}

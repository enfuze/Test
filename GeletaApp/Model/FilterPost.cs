using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeletaApp.Model
{
    public class FilterPost
    {
        //Lentelė reikalinga filtrų saugojimui
        [PrimaryKey]
        public int Id { get; set; }
        public int OrderBy { get; set; }
        public string Occassion { get; set; }
        public string Flower { get; set; }
        public string SheMale { get; set; }
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }
        public string ProductType { get; set; }
    }
}

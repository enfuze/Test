using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeletaApp.Model
{
    public class ShoppingCartPost
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int User_id { get; set; }
        public double Final_price { get; set; }
        public string Pickup_address { get; set; }
        public string Pickup_address_type { get; set; }
        public bool Active_cart { get; set; }
    }
    public class CartItemPost
    {
        [PrimaryKey][AutoIncrement]
        public int Id { get; set; }
        public int Flower_id { get; set; }
        public int Bouquet_id { get; set; }
        public int Home_stuff_id { get; set; }
        public int Amount { get; set; }
        public bool Postcard_required { get; set; }
        public string Buy_comment { get; set; }
        public string Pickup_address { get; set; }
        public string Pickup_address_type { get; set; }
        public int ShoppingCart_id { get; set; }
        public double Price { get; set; }
        public string Product_name { get; set; }
        public string Image { get; set; }
    }
}
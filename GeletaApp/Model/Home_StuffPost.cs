﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeletaApp.Model
{
    public class Home_StuffPost
    {
        // atributai iš didžiųjų raidžių kad eitų atskirt.
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Image_sh { get; set; }
        public int Discount { get; set; }
        public int Gender { get; set; }
        public int Home_stuff_type_id { get; set; }
        public bool Favorite { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace GeletaApp.Model
{
    public class UserAddress
    {
        public int id { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string name { get; set; }
        public string phone_number { get; set; }
        public bool isDefault { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GeletaApp.Model
{
    class Flower_TypePost
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Type_name { get; set; }
    }
    class Occasion_TypePost
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

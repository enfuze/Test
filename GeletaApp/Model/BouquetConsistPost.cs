using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace GeletaApp.Model
{
    public class BouquetConsistPost
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Flower_type_id { get; set; }
        public int Bouquet_id { get; set; }
    }
    public class AssignedOccasionsPost
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Flower_id { get; set; }
        public int Bouquet_id { get; set; }
        public int Occasion_id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GeletaApp.Model
{
    public class BouquetConsist
    {
        public int id { get; set; }
        public int flower_type_id { get; set; }
        public int bouquet_id { get; set; }
    }
    public class AssignedOccasions
    {
        public int id { get; set; }
        public string flower_id { get; set; }
        public string bouquet_id { get; set; }
        public int occasion_id { get; set; }
    }
}

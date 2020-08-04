namespace GeletaApp.Model
{
    public class ProductObject
    {
        public string Product_image { get; set; }
        public string Product_name { get; set; }
        public double Product_price { get; set; }
        public int Product_id { get; set; }
        public string Product_type { get; set; }
        public ProductObject()
        {

        }
        public ProductObject(string product_image, string product_name, double product_price, int product_id, string product_type)
        {
            Product_image = product_image;
            Product_name = product_name;
            Product_price = product_price;
            Product_id = product_id;
            Product_type = product_type;
        }
    }
}

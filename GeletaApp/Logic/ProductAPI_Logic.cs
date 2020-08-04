using GeletaApp.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GeletaApp.Logic
{
    public class ProductAPI_Logic
    {
        public async static Task<List<Bouquet>> GetBouquets(string productType)
        {
            List<Bouquet> products = new List<Bouquet>();

            var url = DataRoot.GenerateURL("getProducts");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    product_type = productType
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var bouquetRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                products = bouquetRoot.bouquets as List<Bouquet>;
            }
            return products;
        }
        public async static Task<List<Flower>> GetFlowers(string productType)
        {
            List<Flower> products = new List<Flower>();

            var url = DataRoot.GenerateURL("getProducts");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    product_type = productType
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var flowerRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                products = flowerRoot.flowers as List<Flower>;
            }
            return products;
        }
        public async static Task<List<Home_Stuff>> GetOtherGoods(string productType)
        {
            List<Home_Stuff> products = new List<Home_Stuff>();

            var url = DataRoot.GenerateURL("getProducts");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    product_type = productType
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var home_stuffRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                products = home_stuffRoot.home_stuff as List<Home_Stuff>;
            }
            return products;
        }
        public async static Task<string> GetProductImage(int productId, string productType)
        {
            string productImage;
            var url = DataRoot.GenerateURL("getProductImage");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    id = productId.ToString(),
                    product_type = productType
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var productRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                productImage = productRoot.product_image;
            }
            return productImage;
        }

        //----- Prideta filtracijai
        public async static Task<List<Flower_Type>> GetFlowerTypes()
        {
            List<Flower_Type> flowerTypes = new List<Flower_Type>();

            var url = DataRoot.GenerateURL("getTypes");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    flower_type = 1
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var dataRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                flowerTypes = dataRoot.flowers_type as List<Flower_Type>;
            }
            return flowerTypes;
        }

        public async static Task<List<Occassion_Type>> GetOccasionTypes()
        {
            List<Occassion_Type> products = new List<Occassion_Type>();

            var url = DataRoot.GenerateURL("getTypes");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    occasion = 1
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var dataRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                products = dataRoot.occasions_type as List<Occassion_Type>;
            }
            return products;
        }

        public async static Task<List<Home_Stuff_Type>> GetHomeStuffTypes()
        {
            List<Home_Stuff_Type> products = new List<Home_Stuff_Type>();

            var url = DataRoot.GenerateURL("getTypes");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    home_stuff_type = 1
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var dataRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                products = dataRoot.home_stuff_type as List<Home_Stuff_Type>;
            }
            return products;
        }

        public async static Task<List<BouquetConsist>> GetBouquetConsist()
        {
            List<BouquetConsist> products = new List<BouquetConsist>();

            var url = DataRoot.GenerateURL("getBouquetConsist");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var dataRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                products = dataRoot.bouquets_consist as List<BouquetConsist>;
            }
            return products;
        }
        public async static Task<List<AssignedOccasions>> GetAssignedOccasions()
        {
            List<AssignedOccasions> products = new List<AssignedOccasions>();

            var url = DataRoot.GenerateURL("getAssignedOccasions");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {

                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var dataRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                products = dataRoot.assigned_occasions as List<AssignedOccasions>;
            }
            return products;
        }
    }
}
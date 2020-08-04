using GeletaApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeletaApp.Logic
{
    public class DataManipulation_Logic
    {
        public static async void ReadBouquets()
        {
            var bouquets = await ProductAPI_Logic.GetBouquets("bouquets");
            int count = bouquets.Count;

            for (int i = 0; i < count; i++)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {

                    BouquetPost post = new BouquetPost()
                    {
                        Id = bouquets[i].id,
                        Name = bouquets[i].bouquet_name,
                        Quantity = bouquets[i].quantity,
                        Price = bouquets[i].price,
                        Description = bouquets[i].description,
                        Discount = bouquets[i].discount,
                        Gender = bouquets[i].gender
                    };
                    conn.CreateTable<BouquetPost>();
                    try
                    {
                        var imageFromDatabase = conn.Find<BouquetPost>(post.Id); //Nuskaitoma puokste is sqlite
                        post.Favorite = imageFromDatabase.Favorite;
                        if (imageFromDatabase.Image_sh != bouquets[i].bouquet_image_sh || imageFromDatabase.Image_sh == null) // tikrinamas gautas sh is api su turimu db
                        {
                            //nuskaitomas ir priskiriamas naujas paveikslelis bei sh kodas
                            string newImage = await ProductAPI_Logic.GetProductImage(bouquets[i].id, "bouquets");
                            post.Image_sh = bouquets[i].bouquet_image_sh;
                            post.Image = newImage;
                        }
                        else
                        {
                            //Paliekamas senas paveikslelis ir sh kodas
                            post.Image_sh = imageFromDatabase.Image_sh;
                            post.Image = imageFromDatabase.Image;
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        string newImage = await ProductAPI_Logic.GetProductImage(bouquets[i].id, "bouquets");
                        post.Image_sh = bouquets[i].bouquet_image_sh;
                        post.Image = newImage;
                        post.Favorite = false;
                    }

                    conn.CreateTable<BouquetPost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<BouquetPost>();
                var apiLast = bouquets.Last().id;
                var apiCount = bouquets.Count;
                var posts = conn.Table<BouquetPost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!bouquets.Any(n => n.id == a.Id))
                        {
                            conn.Delete<BouquetPost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        //---
        public static async void ReadFlowers()
        {
            var flowers = await ProductAPI_Logic.GetFlowers("flowers");
            int count = flowers.Count;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                for (int i = 0; i < count; i++)
                {
                    conn.CreateTable<FlowerPost>();
                    FlowerPost post = new FlowerPost()
                    {
                        Id = flowers[i].id,
                        Name = flowers[i].flower_name,
                        Quantity = flowers[i].quantity,
                        Price = flowers[i].price,
                        Description = flowers[i].description,
                        Discount = flowers[i].discount,
                        Gender = flowers[i].gender,
                        Flower_type_id = flowers[i].flower_type_id
                    };
                    try
                    {
                        var imageFromDatabase = conn.Find<FlowerPost>(post.Id); //Nuskaitoma puokste is sqlite
                        post.Favorite = imageFromDatabase.Favorite;
                        if (imageFromDatabase.Image_sh != flowers[i].flower_image_sh || imageFromDatabase.Image_sh == null) // tikrinamas gautas sh is api su turimu db
                        {
                            //nuskaitomas ir priskiriamas naujas paveikslelis bei sh kodas
                            string newImage = await ProductAPI_Logic.GetProductImage(flowers[i].id, "flowers");
                            post.Image_sh = flowers[i].flower_image_sh;
                            post.Image = newImage;
                        }
                        else
                        {
                            //Paliekamas senas paveikslelis ir sh kodas
                            post.Image_sh = imageFromDatabase.Image_sh;
                            post.Image = imageFromDatabase.Image;
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        string newImage = await ProductAPI_Logic.GetProductImage(flowers[i].id, "flowers");
                        post.Image_sh = flowers[i].flower_image_sh;
                        post.Image = newImage;
                        post.Favorite = false;
                    }

                    conn.CreateTable<FlowerPost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
                conn.CreateTable<FlowerPost>();
                var apiLast = flowers.Last().id;
                var apiCount = flowers.Count;
                var posts = conn.Table<FlowerPost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!flowers.Any(n => n.id == a.Id))
                        {
                            conn.Delete<FlowerPost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        //---------
        public static async void ReadOtherGoods()
        {
            var home_stuff = await ProductAPI_Logic.GetOtherGoods("home_stuff");
            int count = home_stuff.Count;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                for (int i = 0; i < count; i++)
                {
                    conn.CreateTable<Home_StuffPost>();
                    //var kita = conn.Find<Home_StuffPost>(home_stuff[i].id);
                    Home_StuffPost post = new Home_StuffPost()
                    {
                        Id = home_stuff[i].id,
                        Name = home_stuff[i].home_stuff_name,
                        Quantity = home_stuff[i].quantity,
                        Price = home_stuff[i].price,
                        Description = home_stuff[i].description,
                        Discount = home_stuff[i].discount,
                        Gender = home_stuff[i].gender,
                        Home_stuff_type_id = home_stuff[i].home_stuff_type_id

                    };
                    try
                    {
                        var imageFromDatabase = conn.Find<Home_StuffPost>(post.Id); //Nuskaitoma puokste is sqlite
                        post.Favorite = imageFromDatabase.Favorite;
                        if (imageFromDatabase.Image_sh != home_stuff[i].home_stuff_image_sh || imageFromDatabase.Image_sh == null) // tikrinamas gautas sh is api su turimu db
                        {
                            //nuskaitomas ir priskiriamas naujas paveikslelis bei sh kodas
                            string newImage = await ProductAPI_Logic.GetProductImage(home_stuff[i].id, "home_stuff");
                            post.Image_sh = home_stuff[i].home_stuff_image_sh;
                            post.Image = newImage;
                        }
                        else
                        {
                            //Paliekamas senas paveikslelis ir sh kodas
                            post.Image_sh = imageFromDatabase.Image_sh;
                            post.Image = imageFromDatabase.Image;
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        string newImage = await ProductAPI_Logic.GetProductImage(home_stuff[i].id, "home_stuff");
                        post.Image_sh = home_stuff[i].home_stuff_image_sh;
                        post.Image = newImage;
                        post.Favorite = false;
                    }

                    conn.CreateTable<Home_StuffPost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
                conn.CreateTable<Home_StuffPost>();
                var apiLast = home_stuff.Last().id;
                var apiCount = home_stuff.Count;
                var posts = conn.Table<Home_StuffPost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!home_stuff.Any(n => n.id == a.Id))
                        {
                            conn.Delete<Home_StuffPost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        // Filtracijai
        public static async void ReadFlowerTypes()
        {
            var flower_types = await ProductAPI_Logic.GetFlowerTypes();
            int count = flower_types.Count;

            for (int i = 0; i < count; i++)
            {
                Flower_TypePost post = new Flower_TypePost()
                {
                    Id = flower_types[i].id,
                    Type_name = flower_types[i].type_name
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Flower_TypePost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
            }
            //Tikrinimas ar yra pašalintų įrašų duombazėje
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Flower_TypePost>();
                var apiLast = flower_types.Last().id;
                var apiCount = flower_types.Count;
                var posts = conn.Table<Flower_TypePost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!flower_types.Any(n => n.id == a.Id))
                        {
                            conn.Delete<Flower_TypePost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        public static async void ReadHomeStuffTypes()
        {
            var home_stuff_types = await ProductAPI_Logic.GetHomeStuffTypes();
            int count = home_stuff_types.Count;

            for (int i = 0; i < count; i++)
            {
                Home_Stuff_TypePost post = new Home_Stuff_TypePost()
                {
                    Id = home_stuff_types[i].id,
                    Type_name = home_stuff_types[i].type_name
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Home_Stuff_TypePost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
            }
            //Tikrinimas ar yra pašalintų įrašų duombazėje
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Home_Stuff_TypePost>();
                var apiLast = home_stuff_types.Last().id;
                var apiCount = home_stuff_types.Count;
                var posts = conn.Table<Home_Stuff_TypePost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!home_stuff_types.Any(n => n.id == a.Id))
                        {
                            conn.Delete<Home_Stuff_TypePost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        public static async void ReadOccasionTypes()
        {
            var occasionTypes = await ProductAPI_Logic.GetOccasionTypes();
            int count = occasionTypes.Count;

            for (int i = 0; i < count; i++)
            {
                Occasion_TypePost post = new Occasion_TypePost()
                {
                    Id = occasionTypes[i].id,
                    Name = occasionTypes[i].name
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Occasion_TypePost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
            }
            //Tikrinimas ar yra pašalintų įrašų duombazėje
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Occasion_TypePost>();
                var apiLast = occasionTypes.Last().id;
                var apiCount = occasionTypes.Count;
                var posts = conn.Table<Occasion_TypePost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!occasionTypes.Any(n => n.id == a.Id))
                        {
                            conn.Delete<Occasion_TypePost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        public static async void ReadBouquetConsist()
        {
            var bouquetConsist = await ProductAPI_Logic.GetBouquetConsist();
            int count = bouquetConsist.Count;

            for (int i = 0; i < count; i++)
            {
                BouquetConsistPost post = new BouquetConsistPost()
                {
                    Id = bouquetConsist[i].id,
                    Flower_type_id = bouquetConsist[i].flower_type_id,
                    Bouquet_id = bouquetConsist[i].bouquet_id
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<BouquetConsistPost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
            }
            //Tikrinimas ar yra pašalintų įrašų duombazėje
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<BouquetConsistPost>();
                var apiLast = bouquetConsist.Last().id;
                var apiCount = bouquetConsist.Count;
                var posts = conn.Table<BouquetConsistPost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!bouquetConsist.Any(n => n.id == a.Id))
                        {
                            conn.Delete<BouquetConsistPost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        public static async void ReadAssignedOccasion()
        {
            var assignedOccasions = await ProductAPI_Logic.GetAssignedOccasions();
            int count = assignedOccasions.Count;

            for (int i = 0; i < count; i++)
            {
                int gelyte = 0;
                int puokstyte = 0;

                if (assignedOccasions[i].flower_id != null)
                {
                    gelyte = Convert.ToInt32(assignedOccasions[i].flower_id);
                }
                if (assignedOccasions[i].bouquet_id != null)
                {
                    puokstyte = Convert.ToInt32(assignedOccasions[i].bouquet_id);
                }
                AssignedOccasionsPost post = new AssignedOccasionsPost()
                {
                    Id = assignedOccasions[i].id,
                    Bouquet_id = puokstyte,
                    Flower_id = gelyte,
                    Occasion_id = assignedOccasions[i].occasion_id
                };


                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<AssignedOccasionsPost>();
                    int rows = conn.InsertOrReplace(post);
                    if (rows > 0)
                    {
                        //success++;
                    }
                    else
                    {
                        //fail++;
                    }
                }
            }
            //Tikrinimas ar yra pašalintų įrašų duombazėje
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<AssignedOccasionsPost>();
                var apiLast = assignedOccasions.Last().id;
                var apiCount = assignedOccasions.Count;
                var posts = conn.Table<AssignedOccasionsPost>().ToList();
                var dbLast = posts.Last().Id;
                int dbCount = posts.Count;
                if (apiLast != dbLast && dbCount != apiCount)
                {
                    foreach (var a in posts)
                    {
                        if (!assignedOccasions.Any(n => n.id == a.Id))
                        {
                            conn.Delete<AssignedOccasionsPost>(a.Id);
                        }
                    }
                }
                conn.Close();
            }
        }
        public static List<CartItemPost> ReadCart()
        {
            List<CartItemPost> prekes = new List<CartItemPost>();
            //suziuri koks krepselio id pagal user, ir jeigu neaktyvus tai nerodyt, bet jeigu aktyvus 
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                // get items in the active shopping cart
                //var active_id_obj = conn.Query<ShoppingCartPost>("SELECT Id FROM ShoppingCartPost WHERE Active_cart LIKE true");
                //int active_id = active_id_obj[0].Id;
                conn.CreateTable<CartItemPost>();
                prekes = conn.Query<CartItemPost>("SELECT * FROM CartItemPost");
            }
            return prekes;
        }
        public static List<ProductObject> ReadFavourite()
        {
            List<ProductObject> favorites = new List<ProductObject>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //puokste
                string selectas = "SELECT * FROM BouquetPost WHERE Favorite LIKE true ";
                var puok = conn.Query<BouquetPost>(selectas);
                if (puok.Count > 0)
                {
                    for (int i = 0; i < puok.Count; i++)
                    {
                        ProductObject objektas = new ProductObject()
                        {
                            Product_image = puok[i].Image,
                            Product_name = puok[i].Name,
                            Product_price = puok[i].Price,
                            Product_id = puok[i].Id,
                            Product_type = "puokste"
                        };
                        favorites.Add(objektas);
                    }
                }
                //gele
                string selectas1 = "SELECT * FROM FlowerPost WHERE Favorite LIKE true";
                var gel = conn.Query<FlowerPost>(selectas1);
                if (gel.Count > 0)
                {
                    for (int i = 0; i < gel.Count; i++)
                    {
                        ProductObject objektas = new ProductObject()
                        {
                            Product_image = gel[i].Image,
                            Product_name = gel[i].Name,
                            Product_price = gel[i].Price,
                            Product_id = gel[i].Id,
                            Product_type = "gele"
                        };
                        favorites.Add(objektas);
                    }
                }
                //kitas
                string selectas2 = "SELECT * FROM Home_StuffPost WHERE Favorite LIKE true";
                var kitos = conn.Query<Home_StuffPost>(selectas2);
                if (kitos.Count > 0)
                {
                    for (int i = 0; i < kitos.Count; i++)
                    {
                        ProductObject objektas = new ProductObject()
                        {
                            Product_image = kitos[i].Image,
                            Product_name = kitos[i].Name,
                            Product_price = kitos[i].Price,
                            Product_id = kitos[i].Id,
                            Product_type = "kitas"
                        };
                        favorites.Add(objektas);
                    }
                }
                conn.Close();
            }
            return favorites;
        }
    }
}

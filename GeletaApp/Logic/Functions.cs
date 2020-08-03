using GeletaApp.Model;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace GeletaApp.Logic
{
    public class Functions
    {
        public static void SyncDatabase()
        {
            DataManipulation_Logic.ReadBouquets();
            DataManipulation_Logic.ReadFlowers();
            DataManipulation_Logic.ReadOtherGoods();
            DataManipulation_Logic.ReadFlowerTypes();
            DataManipulation_Logic.ReadOccasionTypes();
            DataManipulation_Logic.ReadAssignedOccasion();
            DataManipulation_Logic.ReadBouquetConsist();
            DataManipulation_Logic.ReadHomeStuffTypes();





        }
        public static double GetPriceNumber(string kainos_tekstas)
        {
            double kaina_vieneto = 0.0;
            string kainaText = kainos_tekstas;
            string kainaNumbers = string.Empty;

            for (int i = 0; i < kainaText.Length - 2; i++)
            {
                kainaNumbers += kainaText[i];
            }
            if (kainaNumbers.Length > 0)
            {
                kaina_vieneto = double.Parse(kainaNumbers);
            }
            return kaina_vieneto;
        }
        public static List<ProductObject> Search(string test)
        {
            /*string searchKey = "";
            for (int i = 0; i < test.Length; i++)
            {
                if (test[i].ToString() == "a" || test[i].ToString() == "ą")
                {
                    searchKey += "[aą]";
                }
                else if (test[i].ToString() == "c" || test[i].ToString() == "č")
                {
                    searchKey += "[cč]";
                }
                else if (test[i].ToString() == "e" || test[i].ToString() == "ę" || test[i].ToString() == "ė")
                {
                    searchKey += "[eęė]";
                }
                else if (test[i].ToString() == "i" || test[i].ToString() == "į")
                {
                    searchKey += "[iį]";
                }
                else if (test[i].ToString() == "s" || test[i].ToString() == "š")
                {
                    searchKey += "[sš]";
                }
                else if (test[i].ToString() == "u" || test[i].ToString() == "ų" || test[i].ToString() == "ū")
                {
                    searchKey += "[uųū]";
                }
                else if (test[i].ToString() == "z" || test[i].ToString() == "ž")
                {
                    searchKey += "[zž]";
                }
                else
                {
                    searchKey += test[i].ToString();
                }*/

            string searchKey = test.ToString().Replace("a", "_")
                           .Replace("ą", "_")
                           .Replace("c", "_")
                           .Replace("č", "_")
                           .Replace("e", "_")
                           .Replace("ę", "_")
                           .Replace("ė", "_")
                           .Replace("i", "_")
                           .Replace("į", "_")
                           .Replace("s", "_")
                           .Replace("š", "_")
                           .Replace("ū", "_")
                           .Replace("ų", "_")
                           .Replace("u", "_")
                           .Replace("z", "_")
                           .Replace("ž", "_");

            /*}*/
            //string searchKey = 
            if (searchKey.Length > 4)
            {
                searchKey = searchKey.Remove(searchKey.Length - 1);
            }

            List<int> puoksciuId = new List<int>();
            List<int> geliuId = new List<int>();
            List<int> kituPrekiuId = new List<int>();

            List<ProductObject> listas = new List<ProductObject>();

            string filtras = "LIKE '%" + searchKey + "%'";
            //string filtras = "REGEXP '^R'";
            // Reikai pradet ras6yt sql :(
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //Susirenkam visus imanomus tipus
                string eile1 = "SELECT Id FROM Flower_TypePost WHERE Type_name " + filtras;
                var geliuTipai = conn.Query<Flower_TypePost>(eile1);
                var prekiuTipai = conn.Query<Home_Stuff_TypePost>("SELECT Id FROM Home_Stuff_TypePost WHERE Type_name " + filtras);
                var proguTipai = conn.Query<Occasion_TypePost>("SELECT Id FROM Occasion_TypePost WHERE Name " + filtras);

                //jei progu tipu atitiko kažkas
                if (proguTipai.Count > 0)
                {
                    for (int i = 0; i < proguTipai.Count; i++)
                    {
                        //geles ir puokstes
                        var proguId = conn.Query<AssignedOccasionsPost>("SELECT * FROM AssignedOccasionsPost WHERE Occasion_id = " + proguTipai[i].Id);
                        for (int j = 0; j < proguId.Count; j++)
                        {
                            if (proguId[j].Bouquet_id != 0)
                            {
                                puoksciuId.Add(proguId[j].Bouquet_id);
                            }
                            if (proguId[j].Flower_id != 0)
                            {
                                geliuId.Add(proguId[j].Bouquet_id);
                            }
                        }
                    }
                }

                //Jei geliu tipu atitiko kazkas
                if (geliuTipai.Count > 0)
                {
                    for (int i = 0; i < geliuTipai.Count; i++)
                    {
                        //puokstes
                        string uzklausa = "SELECT * FROM BouquetConsistPost WHERE Flower_type_id = " + geliuTipai[i].Id;
                        var puokstes = conn.Query<BouquetConsistPost>(uzklausa);
                        if (puokstes.Count > 0)
                        {
                            for (int j = 0; j < puokstes.Count; j++)
                            {
                                if (!puoksciuId.Contains(puokstes[j].Id))
                                {
                                    puoksciuId.Add(puokstes[j].Bouquet_id);
                                }
                            }
                        }
                        //geles
                        var geles = conn.Query<FlowerPost>("SELECT * FROM FlowerPost WHERE Flower_type_id = " + geliuTipai[0].Id);
                        if (geles.Count > 0)
                        {
                            for (int j = 0; j < geles.Count; j++)
                            {
                                if (!geliuId.Contains(geles[j].Id))
                                {
                                    geliuId.Add(geles[j].Id);
                                }
                            }
                        }
                    }
                }
                if (prekiuTipai.Count > 0)
                {
                    for (int i = 0; i < prekiuTipai.Count; i++)
                    {
                        var prekes = conn.Query<Home_StuffPost>("SELECT * FROM Home_StuffPost WHERE Home_stuff_type_id = " + prekiuTipai[0].Id);
                        if (prekes.Count > 0)
                        {
                            for (int j = 0; j < prekes.Count; j++)
                            {
                                if (!kituPrekiuId.Contains(prekes[j].Id))
                                {
                                    kituPrekiuId.Add(prekes[j].Id);
                                }
                            }
                        }
                    }
                }

                var gelesx = conn.Query<FlowerPost>("SELECT * FROM FlowerPost WHERE Name " + filtras);
                for (int i = 0; i < gelesx.Count; i++)
                {
                    if (!geliuId.Contains(gelesx[i].Id))
                    {
                        geliuId.Add(gelesx[i].Id);
                    }

                }
                var puokstesx = conn.Query<BouquetPost>("SELECT * FROM BouquetPost WHERE Name " + filtras);
                for (int i = 0; i < puokstesx.Count; i++)
                {
                    if (!puoksciuId.Contains(puokstesx[i].Id))
                    {
                        puoksciuId.Add(puokstesx[i].Id);
                    }

                }
                var kitosx = conn.Query<Home_StuffPost>("SELECT * FROM Home_StuffPost WHERE Name " + filtras);
                for (int i = 0; i < kitosx.Count; i++)
                {
                    if (!kituPrekiuId.Contains(kitosx[i].Id))
                    {
                        kituPrekiuId.Add(kitosx[i].Id);
                    }
                }

                //---------------------------------------------------------------------
                if (puoksciuId.Count > 0)
                {
                    string selectas = "SELECT * FROM BouquetPost WHERE ";
                    for (int i = 0; i < puoksciuId.Count; i++)
                    {
                        if (i > 0)
                        {
                            selectas += " OR ";
                        }
                        selectas += "Id = " + puoksciuId[i];

                    }
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
                            listas.Add(objektas);
                        }
                    }
                }

                if (geliuId.Count > 0)
                {
                    string selectas = "SELECT * FROM FlowerPost WHERE ";
                    for (int i = 0; i < geliuId.Count; i++)
                    {
                        if (i > 0)
                        {
                            selectas += " OR ";
                        }
                        selectas += "Id = " + geliuId[i];
                    }
                    var gel = conn.Query<FlowerPost>(selectas);
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
                            listas.Add(objektas);
                        }
                    }
                    //gele
                }

                if (kituPrekiuId.Count > 0)
                {
                    string selectas = "SELECT * FROM Home_StuffPost WHERE ";
                    for (int i = 0; i < kituPrekiuId.Count; i++)
                    {
                        if (i > 0)
                        {
                            selectas += " OR ";
                        }
                        selectas += "Id = " + kituPrekiuId[i];
                    }
                    var kitos = conn.Query<Home_StuffPost>(selectas);
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
                            listas.Add(objektas);
                        }
                    }
                    //kitas
                }
                conn.Close();
            }


            return listas;

        }
        public string option = "";
        public string option2 = "";
        public void BackButtonWithExit(string path, string detail_atvejis = "")
        {

            if (App._clickCount < 1)
            {
                TimeSpan laikas_tarp_spaudimu = new TimeSpan(0, 0, 0, 0, 200);
                Device.StartTimer(laikas_tarp_spaudimu, ClickHandle);
            }
            App._clickCount++;
            option = path;
            option2 = detail_atvejis;
        }
        bool ClickHandle()
        {
            if (App._clickCount > 1)
            { // double click
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await App.Current.MainPage.DisplayAlert("", "Ar tikrai norite išeiti?", "Taip", "Ne");
                    if (result)
                    {
                        Process.GetCurrentProcess().CloseMainWindow();
                    }
                });
            }
            else
            { // single click
                switch (option)
                {
                    case "pop":
                        PopupNavigation.Instance.PopAsync();
                        break;
                    case "push":
                        App.Current.MainPage.Navigation.PushAsync(new MainPage());
                        break;
                    case "detail":
                        switch (option2)
                        {
                            case "GeletaApp.FlowersPage":
                                App.Current.MainPage.Navigation.PushAsync(new FlowersPage(), true);
                                break;
                            case "GeletaApp.BouquetsPage":
                                App.Current.MainPage.Navigation.PushAsync(new BouquetsPage(), true);
                                break;
                            case "GeletaApp.OtherGoodsPage":
                                App.Current.MainPage.Navigation.PushAsync(new OtherGoodsPage(), true);
                                break;
                            case "GeletaApp.BuyPage":
                                App.Current.MainPage.Navigation.PushAsync(new BuyPage(), true);
                                break;
                            default:
                                App.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
                                break;
                        }
                        break;
                    case "buy":
                        switch (option2)
                        {
                            case "GeletaApp.FlowersPage":
                                App.Current.MainPage.Navigation.PushAsync(new FlowersPage(), true);
                                break;
                            case "GeletaApp.BouquetsPage":
                                App.Current.MainPage.Navigation.PushAsync(new BouquetsPage(), true);
                                break;
                            case "GeletaApp.OtherGoodsPage":
                                App.Current.MainPage.Navigation.PushAsync(new OtherGoodsPage(), true);
                                break;
                            default:
                                App.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
                                break;
                        }
                        break;
                    case "fav":
                        App.Current.MainPage.Navigation.PushAsync(new ProfileMenu());
                        break;
                    case "pop_page":
                        App.Current.MainPage.Navigation.PopAsync();
                        break;
                    case "main":
                        break;
                }
            }
            App._clickCount = 0;
            return false;
        }
    }
                        
}

using Expandable;
using GeletaApp.Logic;
using GeletaApp.Model;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RikiavimasPage
    {
        public List<string> progos = new List<string>();
        public List<string> geles = new List<string>();
        public List<string> prekes = new List<string>();
        public string jisJiFiltras = "";
        public int rikiavimas = 0;
        public double priceMax = 0;
        public double priceMin = 0;
        public RikiavimasPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            expandableView.Status = ExpandStatus.Collapsed;
            expandableView2.Status = ExpandStatus.Collapsed;
            expandableView3.Status = ExpandStatus.Collapsed;
            expandableView4.Status = ExpandStatus.Collapsed;
            expandableView5.Status = ExpandStatus.Collapsed;
            expandableView6.Status = ExpandStatus.Collapsed;
            /*
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;

            BackImgButton.WidthRequest = xamarinWidth * 6.85185 / 100;
            rikiavimo_st.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885416 / 100);
            */
            /* frame1.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
             frame2.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
             frame3.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
             frame4.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
             frame5.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
             frame6.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);*/




            SetFilters();

        }
        public void SetFilters(bool boolas = true)
        {
            // isaugotu filtru uzmetimas
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FilterPost>();
                var filters = conn.Table<FilterPost>().ToList();
                char atskyrimas = ',';
                if (filters.Count != 0)
                {
                    if (filters[0].Occassion != "")
                    {
                        string[] progaList = filters[0].Occassion.Split(atskyrimas);
                        for (int i = 0; i < progaList.Count(); i++)
                        {//3
                            string checkbox = "";
                            if (progaList[i] == "Vestuvės")
                            {
                                checkbox = "vestuves_cb";
                            }
                            if (progaList[i] == "Krikštynos")
                            {
                                checkbox = "krikstynos";
                            }
                            if (progaList[i] == "Tobula diena")
                            {
                                checkbox = "diena_cb";
                            }
                            if (progaList[i] == "Gimtadienis")
                            {
                                checkbox = "gimtadienis_cb";
                            }
                            if (progaList[i] == "Krikštynos")
                            {
                                checkbox = "krikstynos";
                            }
                            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>(checkbox);
                            checkBox.IsChecked = boolas;
                        }
                    }
                    if (filters[0].Flower != "")
                    {
                        string[] geliuList = filters[0].Flower.Split(atskyrimas);
                        for (int i = 0; i < geliuList.Count(); i++)
                        {//4
                            string checkbox = "";
                            if (geliuList[i] == "Rožė")
                            {
                                checkbox = "rozes_cb";
                            }
                            if (geliuList[i] == "Tulpė")
                            {
                                checkbox = "tulpes_cb";
                            }
                            if (geliuList[i] == "Vaškagėlė")
                            {
                                checkbox = "vaskagele_cb";
                            }
                            if (geliuList[i] == "Gvaizdikas")
                            {
                                checkbox = "gvazdikai_cb";
                            }
                            if (geliuList[i] == "Vėdrynas")
                            {
                                checkbox = "vedrynai_cb";
                            }
                            if (geliuList[i] == "Eustoma")
                            {
                                checkbox = "eustomos_cb";
                            }
                            if (geliuList[i] == "Gerbera")
                            {
                                checkbox = "gerbenos_cb";
                            }
                            if (geliuList[i] == "Hortenzija")
                            {
                                checkbox = "hortenzijos_cb";
                            }
                            CheckBox checkBox = expandableView4.SecondaryView.FindByName<CheckBox>(checkbox);
                            checkBox.IsChecked = boolas;
                        }
                    }
                    if (filters[0].ProductType != "")
                    {
                        string[] prekesList = filters[0].ProductType.Split(atskyrimas);
                        for (int i = 0; i < prekesList.Count(); i++)
                        {//6
                            string checkbox = "";
                            if (prekesList[i] == "Vazonas")
                            {
                                checkbox = "vazonai_cb";
                            }
                            if (prekesList[i] == "Vazoninis augalas")
                            {
                                checkbox = "vazaugalai_cb";
                            }
                            if (prekesList[i] == "Interjero detalės")
                            {
                                checkbox = "intdetales_cb";
                            }
                            if (prekesList[i] == "Namų kvapas")
                            {
                                checkbox = "kvapai_cb";
                            }
                            CheckBox checkBox = expandableView6.SecondaryView.FindByName<CheckBox>(checkbox);
                            checkBox.IsChecked = boolas;
                        }
                    }
                    if (filters[0].PriceMax != 0)
                    {
                        var asd = expandableView5.SecondaryView.FindByName<Entry>("kaina_iki");
                        asd.Text = filters[0].PriceMax.ToString();
                    }
                    if (filters[0].PriceMin != 0)
                    {
                        var asd = expandableView5.SecondaryView.FindByName<Entry>("kaina_nuo");
                        asd.Text = filters[0].PriceMin.ToString();
                    }
                    if (filters[0].OrderBy != 0)
                    {
                        if (filters[0].OrderBy == 1)
                        {
                            var radioB = expandableView.SecondaryView.FindByName<RadioButton>("orderBy1");
                            radioB.IsChecked = boolas;
                        }
                        if (filters[0].OrderBy == 2)
                        {
                            var radioB = expandableView.SecondaryView.FindByName<RadioButton>("orderBy2");
                            radioB.IsChecked = boolas;
                        }
                    }
                    if (filters[0].SheMale != "")
                    {
                        if (filters[0].SheMale == "Jam")
                        {
                            var radioB = expandableView4.SecondaryView.FindByName<RadioButton>("Jam");
                            radioB.IsChecked = boolas;
                        }
                        if (filters[0].SheMale == "Jai")
                        {
                            var radioB = expandableView4.SecondaryView.FindByName<RadioButton>("Jai");
                            radioB.IsChecked = boolas;
                        }
                    }
                }
            }
        }

        protected override void OnAppearingAnimationBegin()
        {


            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            rikiavimo_st.HeightRequest = xamarinHeight;
            rikiavimo_st.WidthRequest = xamarinWidth;
            mygtuku_st.WidthRequest = xamarinWidth - (xamarinWidth * 6.85 / 100);
            filtru_st.Padding = new Thickness(0, xamarinHeight * 8.333 / 100, 0, 0);
            customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, xamarinHeight * 0.8854 / 100);
            expandableView.PrimaryView.FindByName<Label>("rikiavimasL").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, 0, 0);
            expandableView.PrimaryView.FindByName<Image>("arrow").Margin = new Thickness(0, 0, xamarinWidth * 3.425 / 100, 0);
            expandableView.SecondaryView.FindByName<StackLayout>("secStack1").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, xamarinWidth * 3.425 / 100, 0);

            expandableView5.PrimaryView.FindByName<Label>("kainaL").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, 0, 0);
            expandableView5.PrimaryView.FindByName<Image>("arrow5").Margin = new Thickness(0, 0, xamarinWidth * 3.425 / 100, 0);

            arrow.WidthRequest = xamarinWidth * 4.814 / 100;

            //Prideti
            BackImgButton.WidthRequest = xamarinWidth * 6.85185 / 100;
            rikiavimo_st.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885416 / 100);
            //-------------------

            arrow5.WidthRequest = xamarinWidth * 4.814 / 100;

            frame1.HeightRequest = xamarinHeight * 6.51 / 100;

            frame5.HeightRequest = xamarinHeight * 6.51 / 100;


            base.OnAppearingAnimationBegin();




            List<Page> li = Navigation.NavigationStack.ToList();
            Page last = li.ElementAt(li.Count - 1);
            var page = last.ToString();

            if (page.Equals("GeletaApp.OtherGoodsPage"))
            {
                expandableView6.PrimaryView.FindByName<Label>("prekesL").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, 0, 0);
                expandableView6.PrimaryView.FindByName<Image>("arrow5").Margin = new Thickness(0, 0, xamarinWidth * 3.425 / 100, 0);
                expandableView6.SecondaryView.FindByName<StackLayout>("secStack6").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, xamarinWidth * 3.425 / 100, 0);

                arrow6.WidthRequest = xamarinWidth * 4.814 / 100;
                frame6.HeightRequest = xamarinHeight * 6.51 / 100;

                expandableView2.IsVisible = false;
                expandableView3.IsVisible = false;
                expandableView4.IsVisible = false;
                expandableView6.IsVisible = true;

            }
            else
            {
                expandableView2.PrimaryView.FindByName<Label>("progaL").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, 0, 0);
                expandableView2.PrimaryView.FindByName<Image>("arrow2").Margin = new Thickness(0, 0, xamarinWidth * 3.425 / 100, 0);
                expandableView2.SecondaryView.FindByName<StackLayout>("secStack2").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, xamarinWidth * 3.425 / 100, 0);

                expandableView3.PrimaryView.FindByName<Label>("gelesL").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, 0, 0);
                expandableView3.PrimaryView.FindByName<Image>("arrow3").Margin = new Thickness(0, 0, xamarinWidth * 3.425 / 100, 0);
                expandableView3.SecondaryView.FindByName<StackLayout>("secStack3").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, xamarinWidth * 3.425 / 100, 0);

                expandableView4.PrimaryView.FindByName<Label>("jaiJamL").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, 0, 0);
                expandableView4.PrimaryView.FindByName<Image>("arrow4").Margin = new Thickness(0, 0, xamarinWidth * 3.425 / 100, 0);
                expandableView4.SecondaryView.FindByName<StackLayout>("secStack4").Margin = new Thickness(xamarinWidth * 13.055 / 100, 0, xamarinWidth * 3.425 / 100, 0);
                arrow2.WidthRequest = xamarinWidth * 4.814 / 100;
                arrow3.WidthRequest = xamarinWidth * 4.814 / 100;
                arrow4.WidthRequest = xamarinWidth * 4.814 / 100;
                frame2.HeightRequest = xamarinHeight * 6.51 / 100;
                frame3.HeightRequest = xamarinHeight * 6.51 / 100;
                frame4.HeightRequest = xamarinHeight * 6.51 / 100;
                expandableView2.IsVisible = true;
                expandableView3.IsVisible = true;
                expandableView4.IsVisible = true;
                expandableView6.IsVisible = false;
            }
        }
        private async void BackImgButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("pop");
            return true;
        }

        //----------------------------------------------------------------------------------
        //Reikia peržiūrėt, veikia kažkodėl du kartus.
        public void RadioButtonCheckJisJi(object sender, CheckedChangedEventArgs e)
        {
            RadioButton jisJi = sender as RadioButton;
            jisJiFiltras = jisJi.ClassId.ToString();
        }
        public void RadioButtonCheckRikiavimas(object sender, CheckedChangedEventArgs e)
        {
            RadioButton tarpinis = sender as RadioButton;
            rikiavimas = Convert.ToInt32(tarpinis.ClassId);
        }

        //-----------------------------------------------------------------------------------------------------
        public void CheckBoxCheckPrekes(object sender, CheckedChangedEventArgs e)
        {
            CheckBox prek = sender as CheckBox;
            var prekyte = prek.ClassId.ToString();

            prekes.Add(prekyte);
        }
        public void CheckBoxCheckProgos(object sender, CheckedChangedEventArgs e)
        {
            CheckBox proga = sender as CheckBox;
            var svente = proga.ClassId.ToString();

            progos.Add(svente);
        }
        public void CheckBoxCheckGeles(object sender, CheckedChangedEventArgs e)
        {
            CheckBox gele = sender as CheckBox;
            var gelyte = gele.ClassId.ToString();

            geles.Add(gelyte);
        }

        public string SqlFormavimas(List<string> progos, List<string> geles, List<string> prekes, string page)
        {

            //Pasiruosimas kintamuju ir tikrinimas kokia filtracija reikia atlikti
            string filtras = "";
            bool arGeles = false;
            bool arPuokste = false;
            if (page.Equals("GeletaApp.BouquetsPage"))
            {
                arPuokste = true;
                arGeles = true;
                filtras = "Bouquet_id";
            }
            else if (page.Equals("GeletaApp.FlowersPage"))
            {
                arGeles = true;
                filtras = "Flower_id";
            }
            else if (page.Equals("GeletaApp.OtherGoodsPage"))
            {
                //filtras = "";
            }
            var kainaNuo = "0";
            var kainaIki = "0";
            try
            {
                kainaNuo = expandableView5.SecondaryView.FindByName<Entry>("kaina_nuo").Text;
            }
            catch (Exception ex)
            {

            }
            try
            {
                kainaIki = expandableView5.SecondaryView.FindByName<Entry>("kaina_iki").Text;
            }
            catch (Exception ex)
            {

            }
            double nuo_kaina = Convert.ToDouble(kainaNuo);
            double iki_kaina = Convert.ToDouble(kainaIki);
            priceMax = iki_kaina;
            priceMin = nuo_kaina;

            int proguKiekis = progos.Count;
            int geliuKiekis = geles.Count;
            int prekiuKiekis = prekes.Count();
            List<int> isfiltruotiProguId = new List<int>();
            List<int> isfiltruotiTipuId = new List<int>();
            List<int> isfiltruotiPrekiuId = new List<int>();

            if (geliuKiekis != 0 || proguKiekis != 0 || nuo_kaina != 0 || iki_kaina != 0 || rikiavimas > 0 ||
                    jisJiFiltras != "" || prekiuKiekis != 0)
            {
                string query = "WHERE ";

                //Filtravimas pagal sventes
                //------------------------------------------------------------------------------------------------
                if (proguKiekis != 0 && arGeles)
                {
                    List<int> sventes = new List<int>();
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Occasion_TypePost>();
                        for (int i = 0; i < proguKiekis; i++)
                        {
                            string proga = progos[i];
                            string query1 = string.Format("SELECT Id FROM Occasion_TypePost WHERE Name = '{0}'", proga);
                            var posts = conn.Query<Occasion_TypePost>(query1);
                            sventes.Add(posts[0].Id);
                        }

                        for (int i = 0; i < sventes.Count; i++)
                        {
                            string query2 = string.Format("SELECT {0} FROM AssignedOccasionsPost WHERE Occasion_id = '{1}'", filtras, sventes[i]);
                            var post = conn.Query<AssignedOccasionsPost>(query2);
                            for (int j = 0; j < post.Count; j++)
                            {
                                if (arPuokste)
                                {
                                    if (!isfiltruotiProguId.Contains(post[j].Bouquet_id))
                                    {
                                        isfiltruotiProguId.Add(post[j].Bouquet_id);
                                    }
                                }
                                else
                                {
                                    if (!isfiltruotiProguId.Contains(post[j].Flower_id) && post[j].Flower_id > 0)
                                    {
                                        isfiltruotiProguId.Add(post[j].Flower_id);
                                    }
                                }
                            }
                        }
                        conn.Close();
                    }
                }

                //Filtravimas pagal puokstes
                if (geliuKiekis != 0 && arGeles)
                {
                    List<int> geliuTipai = new List<int>();
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Flower_TypePost>();
                        for (int i = 0; i < geliuKiekis; i++)
                        {
                            string gelesTipas = geles[i];
                            string query1 = string.Format("SELECT Id FROM Flower_TypePost WHERE Type_name = '{0}'", gelesTipas);
                            var posts = conn.Query<Flower_TypePost>(query1);
                            geliuTipai.Add(posts[0].Id);
                        }

                        for (int i = 0; i < geliuTipai.Count; i++)
                        {
                            if (arPuokste)
                            {
                                string queryy = string.Format("SELECT Bouquet_id FROM BouquetConsistPost WHERE Flower_type_id = '{0}'", geliuTipai[i]);
                                var posty = conn.Query<BouquetConsistPost>(queryy);
                                for (int j = 0; j < posty.Count; j++)
                                {
                                    if (!isfiltruotiTipuId.Contains(posty[j].Bouquet_id))
                                    {
                                        isfiltruotiTipuId.Add(posty[j].Bouquet_id);
                                    }
                                }
                            }
                            //jei geles tai imama grynai is geliu Table
                            else
                            {
                                string queryx = string.Format("SELECT Id FROM FlowerPost WHERE Flower_type_id = '{0}'", geliuTipai[i]);
                                var postx = conn.Query<FlowerPost>(queryx);
                                for (int j = 0; j < postx.Count; j++)
                                {
                                    if (!isfiltruotiTipuId.Contains(postx[j].Id))
                                    {
                                        isfiltruotiTipuId.Add(postx[j].Id);
                                    }
                                }
                            }
                        }
                        conn.Close();
                    }
                }
                //---- prekems
                if (!arGeles && prekiuKiekis > 0)
                {
                    List<int> prekiuTipai = new List<int>();
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Home_Stuff_TypePost>();
                        for (int i = 0; i < prekiuKiekis; i++)
                        {
                            string prekesTipas = prekes[i];
                            string query1 = string.Format("SELECT Id FROM Home_Stuff_TypePost WHERE Type_name = '{0}'", prekesTipas);
                            var posts = conn.Query<Home_Stuff_TypePost>(query1);
                            prekiuTipai.Add(posts[0].Id);
                        }

                        for (int i = 0; i < prekiuTipai.Count; i++)
                        {
                            string queryx = string.Format("SELECT Id FROM Home_StuffPost WHERE Home_stuff_type_id = '{0}'", prekiuTipai[i]);
                            var postx = conn.Query<Home_StuffPost>(queryx);
                            for (int j = 0; j < postx.Count; j++)
                            {
                                if (!isfiltruotiPrekiuId.Contains(postx[j].Id))
                                {
                                    isfiltruotiPrekiuId.Add(postx[j].Id);
                                }
                            }
                        }
                        conn.Close();
                    }
                }


                if (isfiltruotiPrekiuId.Count > 0 && !arGeles)
                {
                    query += "( ";
                    for (int i = 0; i < isfiltruotiPrekiuId.Count; i++)
                    {
                        if (!query.Equals("WHERE ( "))
                        {
                            query += " OR ";
                        }

                        query += string.Format("Id = '{0}'", isfiltruotiPrekiuId[i]);
                    }
                    query += " ) ";
                }//---- prekiu pabaiga

                //kai pasirinkta tik proga
                if (isfiltruotiProguId.Count > 0 && isfiltruotiTipuId.Count == 0)
                {
                    query += "( ";
                    for (int i = 0; i < isfiltruotiProguId.Count; i++)
                    {
                        if (!query.Equals("WHERE ( "))
                        {
                            query += " OR ";
                        }

                        query += string.Format("Id = '{0}'", isfiltruotiProguId[i]);
                    }
                    query += " ) ";
                }
                //kai pasiriktas tipas
                else if (isfiltruotiProguId.Count == 0 && isfiltruotiTipuId.Count > 0)
                {
                    query += "( ";
                    for (int i = 0; i < isfiltruotiTipuId.Count; i++)
                    {
                        if (!query.Equals("WHERE ( "))
                        {
                            query += " OR ";
                        }
                        query += string.Format("Id = '{0}'", isfiltruotiTipuId[i]);
                    }
                    query += " ) ";
                }

                //kai pasirinkta ir proga ir tipas
                else if (isfiltruotiProguId.Count > 0 && isfiltruotiTipuId.Count > 0)
                {
                    List<int> sutampaProgosTipai = new List<int>();
                    for (int i = 0; i < isfiltruotiProguId.Count; i++)
                    {
                        if (isfiltruotiTipuId.Contains(isfiltruotiProguId[i]))
                        {
                            sutampaProgosTipai.Add(isfiltruotiProguId[i]);
                        }
                    }
                    query += "( ";
                    for (int i = 0; i < sutampaProgosTipai.Count; i++)
                    {
                        if (!query.Equals("WHERE ( "))
                        {
                            query += " OR ";
                        }
                        query += string.Format("Id = '{0}'", sutampaProgosTipai[i]);
                    }
                    query += " ) ";
                }

                //Filtravimas nuo tam tikros kainos
                if (nuo_kaina > 0)
                {
                    if (!query.Equals("WHERE "))
                    {
                        query += " AND ";
                    }
                    query += string.Format("Price >= '{0}'", nuo_kaina);
                }

                //Filtravimas iki tam tikos kainos
                if (iki_kaina > 0)
                {
                    if (!query.Equals("WHERE "))
                    {
                        query += " AND ";
                    }
                    query += string.Format("Price <='{0}'", iki_kaina);
                }

                //Jis ji radiobutton filtracija
                if (jisJiFiltras != "")
                {
                    if (!query.Equals("WHERE "))
                    {
                        query += " AND ";
                    }
                    if (jisJiFiltras == "Jam")
                    {
                        query += "(Gender = '1' OR Gender= '2')";
                    }
                    else if (jisJiFiltras == "Jai")
                    {
                        query += "(Gender = '0' OR Gender= '2')";
                    }
                }

                //Rikiavimas pagal pasirinkta filtra
                if (rikiavimas > 0)
                {
                    if (query == "WHERE ")
                    {
                        query = "";
                    }
                    if (rikiavimas == 1)
                    {
                        query += string.Format(" ORDER BY Price ASC");
                    }
                    else if (rikiavimas == 2)
                    {
                        query += string.Format(" ORDER BY Price DESC");
                    }
                }
                return query;
            }
            else
            {
                return " ";
            }
        }
        private async Task filtracijaAsync()
        {
            List<Page> li = Navigation.NavigationStack.ToList();
            Page last = li.ElementAt(li.Count - 1);
            var page = last.ToString();
            bool tuscias = false;
            string halfSql = SqlFormavimas(progos, geles, prekes, page);
            string query = "tuscia";
            string proguStringas = "";
            string prekiuStringas = "";
            string geliuStringas = "";

            if (progos.Count > 0)
            {
                for (int i = 0; i < progos.Count; i++)
                {
                    if (i != 0)
                    {
                        proguStringas += String.Format(",");
                    }
                    proguStringas += String.Format(progos[i]);
                }
            }
            if (geles.Count > 0)
            {
                for (int i = 0; i < geles.Count; i++)
                {
                    if (i != 0)
                    {
                        geliuStringas += String.Format(",");
                    }
                    geliuStringas += String.Format(geles[i]);
                }
            }
            if (prekes.Count > 0)
            {
                for (int i = 0; i < prekes.Count; i++)
                {
                    if (i != 0)
                    {
                        prekiuStringas += String.Format(",");
                    }
                    prekiuStringas += String.Format(prekes[i]);
                }
            }
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FilterPost>();
                FilterPost filters = new FilterPost()
                {
                    Id = 1,
                    Occassion = proguStringas,
                    Flower = geliuStringas,
                    ProductType = prekiuStringas,
                    PriceMin = priceMin,
                    PriceMax = priceMax,
                    SheMale = jisJiFiltras,
                    OrderBy = rikiavimas
                };
                conn.CreateTable<FilterPost>();
                int rows = conn.InsertOrReplace(filters);
                if (rows > 0)
                {
                    //success++;
                }
                else
                {
                    //fail++;
                }
            }

            if (halfSql == "WHERE ")
            {
                tuscias = true;
            }
            if (halfSql == "")
            {
                tuscias = true;
            }

            if (page.Equals("GeletaApp.BouquetsPage"))
            {
                if (!tuscias)
                {
                    query = "SELECT * FROM BouquetPost " + halfSql;
                }
                await App.Current.MainPage.Navigation.PushAsync(new BouquetsPage(query));
            }
            else if (page.Equals("GeletaApp.FlowersPage"))
            {
                if (!tuscias)
                {
                    query = "SELECT * FROM FlowerPost " + halfSql;
                }
                await App.Current.MainPage.Navigation.PushAsync(new FlowersPage(query));
            }
            else if (page.Equals("GeletaApp.OtherGoodsPage"))
            {
                if (!tuscias)
                {
                    query = "SELECT * FROM Home_StuffPost " + halfSql;
                }
                await App.Current.MainPage.Navigation.PushAsync(new OtherGoodsPage(query));
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            expandableView.StatusChanged += OnStatusChanged;

            expandableView2.StatusChanged += OnStatusChanged;
            expandableView3.StatusChanged += OnStatusChanged;
            expandableView4.StatusChanged += OnStatusChanged;
            expandableView5.StatusChanged += OnStatusChanged;

        }
        protected override void OnDisappearing()
        {
            expandableView.StatusChanged -= OnStatusChanged;
            expandableView2.StatusChanged -= OnStatusChanged;
            expandableView3.StatusChanged -= OnStatusChanged;
            expandableView4.StatusChanged -= OnStatusChanged;
            expandableView5.StatusChanged -= OnStatusChanged;
        }

        private async void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            if(expandableView.Status == ExpandStatus.Expanding)
            {
                expandableView.IsExpanded = true;
                expandableView2.IsExpanded = false;
                expandableView3.IsExpanded = false;
                expandableView4.IsExpanded = false;
                expandableView5.IsExpanded = false;
                expandableView6.IsExpanded = false;
            }
            else if (expandableView2.Status == ExpandStatus.Expanding)
            {
                expandableView2.IsExpanded = true;
                expandableView.IsExpanded = false;
                expandableView3.IsExpanded = false;
                expandableView4.IsExpanded = false;
                expandableView5.IsExpanded = false;
                expandableView6.IsExpanded = false;
            }
            else if (expandableView3.Status == ExpandStatus.Expanding)
            {
                expandableView3.IsExpanded = true;
                expandableView.IsExpanded = false;
                expandableView2.IsExpanded = false;
                expandableView4.IsExpanded = false;
                expandableView5.IsExpanded = false;
                expandableView6.IsExpanded = false;
            }
            else if (expandableView4.Status == ExpandStatus.Expanding)
            {
                expandableView4.IsExpanded = true;
                expandableView.IsExpanded = false;
                expandableView3.IsExpanded = false;
                expandableView2.IsExpanded = false;
                expandableView5.IsExpanded = false;
                expandableView6.IsExpanded = false;
            }
            else if (expandableView5.Status == ExpandStatus.Expanding)
            {
                expandableView5.IsExpanded = true;
                expandableView.IsExpanded = false;
                expandableView3.IsExpanded = false;
                expandableView4.IsExpanded = false;
                expandableView2.IsExpanded = false;
                expandableView6.IsExpanded = false;
            }
            else if (expandableView6.Status == ExpandStatus.Expanding)
            {
                expandableView6.IsExpanded = true;
                expandableView.IsExpanded = false;
                expandableView3.IsExpanded = false;
                expandableView4.IsExpanded = false;
                expandableView2.IsExpanded = false;
                expandableView5.IsExpanded = false;
            }
            if (expandableView.IsExpanded)
            {
                await arrow.RotateTo(180, 200, Easing.CubicInOut);
            }
            if (expandableView2.IsExpanded)
            {
                await arrow2.RotateTo(180, 200, Easing.CubicInOut);
            }
            if (!expandableView.IsExpanded)
            {
                await arrow.RotateTo(0, 200, Easing.CubicInOut);
            }
            if (!expandableView2.IsExpanded)
            {
                await arrow2.RotateTo(0, 200, Easing.CubicInOut);
            }

            if (expandableView3.IsExpanded)
            {
                await arrow3.RotateTo(180, 200, Easing.CubicInOut);
            }
            if (expandableView4.IsExpanded)
            {
                await arrow4.RotateTo(180, 200, Easing.CubicInOut);

            }
            if (!expandableView3.IsExpanded)
            {
                await arrow3.RotateTo(0, 200, Easing.CubicInOut);
            }
            if (!expandableView4.IsExpanded)
            {
                await arrow4.RotateTo(0, 200, Easing.CubicInOut);
            }

            if (expandableView5.IsExpanded)
            {
                await arrow5.RotateTo(180, 200, Easing.CubicInOut);

            }
            if (!expandableView5.IsExpanded)
            {
                await arrow5.RotateTo(0, 200, Easing.CubicInOut);
            }

        }
        //-----------------------------------------
        private void vazonai_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView6.SecondaryView.FindByName<CheckBox>("vazonai_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }
        private void vazaugalai_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView6.SecondaryView.FindByName<CheckBox>("vazaugalai_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }
        private void intdetales_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView6.SecondaryView.FindByName<CheckBox>("intdetales_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }
        private void kvapai_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView6.SecondaryView.FindByName<CheckBox>("kvapai_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        //-------------------------------------------------------------------------

        private void vestuves_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView2.SecondaryView.FindByName<CheckBox>("vestuves_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void krikstynos_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView2.SecondaryView.FindByName<CheckBox>("krikstynos");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void diena_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView2.SecondaryView.FindByName<CheckBox>("diena_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void gimtadienis_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView2.SecondaryView.FindByName<CheckBox>("gimtadienis_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void rozes_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("rozes_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void tulpes_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("tulpes_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void vaskagele_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("vaskagele_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void gvazdikai_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("gvazdikai_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void vedrynai_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("vedrynai_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void eustomos_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("eustomos_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private void gerbenos_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("gerbenos_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }

        }

        private void hortenzijos_tap_Tapped(object sender, EventArgs e)
        {
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("hortenzijos_cb");
            if (checkBox.IsChecked == false)
            {
                checkBox.IsChecked = true;
            }
            else
            {
                checkBox.IsChecked = false;
            }
        }

        private async void filtravimo_button_Clicked(object sender, EventArgs e)
        {
            filtracijaAsync();
            await PopupNavigation.Instance.PopAsync();

        }
        private async void isvalyti_button_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.DropTable<FilterPost>();
                conn.Close();
            }
            CheckBox checkBox = expandableView3.SecondaryView.FindByName<CheckBox>("vestuves_cb");
            checkBox.IsChecked = false;
            CheckBox checkBox1 = expandableView3.SecondaryView.FindByName<CheckBox>("krikstynos");
            checkBox1.IsChecked = false;
            CheckBox checkBox2 = expandableView3.SecondaryView.FindByName<CheckBox>("diena_cb");
            checkBox2.IsChecked = false;
            CheckBox checkBox3 = expandableView3.SecondaryView.FindByName<CheckBox>("gimtadienis_cb");
            checkBox3.IsChecked = false;
            CheckBox checkBox4 = expandableView3.SecondaryView.FindByName<CheckBox>("krikstynos");
            checkBox4.IsChecked = false;

            CheckBox checkBox5 = expandableView4.SecondaryView.FindByName<CheckBox>("rozes_cb");
            checkBox5.IsChecked = false;
            CheckBox checkBox6 = expandableView4.SecondaryView.FindByName<CheckBox>("tulpes_cb");
            checkBox6.IsChecked = false;
            CheckBox checkBox7 = expandableView4.SecondaryView.FindByName<CheckBox>("vaskagele_cb");
            checkBox7.IsChecked = false;
            CheckBox checkBox8 = expandableView4.SecondaryView.FindByName<CheckBox>("gvazdikai_cb");
            checkBox8.IsChecked = false;
            CheckBox checkBox9 = expandableView4.SecondaryView.FindByName<CheckBox>("vedrynai_cb");
            checkBox9.IsChecked = false;
            CheckBox checkBox10 = expandableView4.SecondaryView.FindByName<CheckBox>("eustomos_cb");
            checkBox10.IsChecked = false;
            CheckBox checkBox11 = expandableView4.SecondaryView.FindByName<CheckBox>("gerbenos_cb");
            checkBox11.IsChecked = false;
            CheckBox checkBox12 = expandableView4.SecondaryView.FindByName<CheckBox>("hortenzijos_cb");
            checkBox12.IsChecked = false;


            CheckBox checkBox13 = expandableView6.SecondaryView.FindByName<CheckBox>("vazonai_cb");
            checkBox13.IsChecked = false;
            CheckBox checkBox14 = expandableView6.SecondaryView.FindByName<CheckBox>("vazaugalai_cb");
            checkBox14.IsChecked = false;
            CheckBox checkBox15 = expandableView6.SecondaryView.FindByName<CheckBox>("intdetales_cb");
            checkBox15.IsChecked = false;
            CheckBox checkBox16 = expandableView6.SecondaryView.FindByName<CheckBox>("kvapai_cb");
            checkBox16.IsChecked = false;

            var kainaA = expandableView5.SecondaryView.FindByName<Entry>("kaina_iki");
            kainaA.Text = "";
            var kainaB = expandableView5.SecondaryView.FindByName<Entry>("kaina_nuo");
            kainaB.Text = "";

            var radioB1 = expandableView.SecondaryView.FindByName<RadioButton>("orderBy1");
            radioB1.IsChecked = false;
            var radioB2 = expandableView.SecondaryView.FindByName<RadioButton>("orderBy2");
            radioB2.IsChecked = false;

            var radioB3 = expandableView4.SecondaryView.FindByName<RadioButton>("Jam");
            radioB3.IsChecked = false;
            var radioB4 = expandableView4.SecondaryView.FindByName<RadioButton>("Jai");
            radioB4.IsChecked = false;
            List<Page> li = Navigation.NavigationStack.ToList();
            Page last = li.ElementAt(li.Count - 1);
            var page = last.ToString();
            
            if (page.Equals("GeletaApp.BouquetsPage"))
            {
               await  App.Current.MainPage.Navigation.PushAsync(new BouquetsPage());
            }
            else if (page.Equals("GeletaApp.FlowersPage"))
            {
               await  App.Current.MainPage.Navigation.PushAsync(new FlowersPage());
            }
            else if (page.Equals("GeletaApp.OtherGoodsPage"))
            {
               await  App.Current.MainPage.Navigation.PushAsync(new OtherGoodsPage());
            }
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
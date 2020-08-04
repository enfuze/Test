
using GeletaApp.Logic;
using GeletaApp.Model;
using SQLite;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritePage : ContentPage
    {
        public FavouritePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            puokstes_cp.WidthRequest = xamarinWidth;
            puokstes_cp.HeightRequest = xamarinHeight;
            paieska_main_st.WidthRequest = xamarinWidth * 73.61 / 100;
            shop_img.HeightRequest = xamarinHeight * 3.229 / 100;
            paieskos_juostos_entry.HeightRequest = xamarinHeight * 5.15625 / 100;
            virsutinis_st.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 5);
            paieska.FontSize = xamarinHeight * 1.7359375 / 100;
            paieskos_juostos_entry.FontSize = xamarinHeight * 1.7359375 / 100;
            paieskos_linija.WidthRequest = xamarinWidth * 48.14 / 100;
            paieskos_juostos_entry.WidthRequest = xamarinWidth * 57.407 / 100;
            customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            puokstes_cp.Padding = new Thickness(xamarinWidth * 2.037 / 100, xamarinHeight * 2.1875 / 100, xamarinWidth * 2.037 / 100, xamarinHeight * 0.9375 / 100);
            puoktes_label.FontSize = xamarinHeight * 3.3854 / 100;
            puoktes_label.Margin = new Thickness(0, xamarinHeight * 3.6979 / 100, 0, xamarinHeight * 1.302 / 100);
            puoktes_label.FontSize = xamarinHeight * 4.79166 / 100;

            FavouriteList.HeightRequest = xamarinHeight;
            FavouriteList.WidthRequest = xamarinWidth;
            FavouriteList.Margin = new Thickness(0, 0, 0, xamarinHeight * 2.604 / 100);
            grid_layout.HorizontalItemSpacing = xamarinWidth * 1.666 / 100;
            var favorites = DataManipulation_Logic.ReadFavourite();
            if (favorites.Count > 0)
            {
                FavouriteList.ItemsSource = favorites;
            }
            else
            {
                pranesimas.IsVisible = true;
            }

        }
        [Obsolete]
        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var imageSender = sender as Grid;
            var asd = imageSender.RowDefinitions.ToArray();
            var xasd = asd[0].BindingContext as ProductObject;

            int id = xasd.Product_id;
            string type = xasd.Product_type;
            int tipas = 0;
            if (type.Equals("gele"))
            {
                tipas = 1;
            }
            else if (type.Equals("puokste"))
            {
                tipas = 2;
            }
            else if (type.Equals("kitas"))
            {
                tipas = 3;
            }
            Navigation.PushAsync(new DetailedProduct(id, tipas));
        }
        private void isekoti_tap_Tapped(object sender, EventArgs e)
        {
            bool paspaude = true;
        }
        private void heartg_Clicked(object sender, EventArgs e)
        {
            var imageSender = sender as ImageButton;
            var gaminu = imageSender.Parent;
            var toliau_gaminu = gaminu.BindingContext as ProductObject;
            string type = toliau_gaminu.Product_type;
            int id = toliau_gaminu.Product_id;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                if (type.Equals("gele"))
                {
                    string query1 = string.Format("UPDATE FlowerPost SET Favorite = NOT Favorite WHERE Id = '{0}'", id);
                    conn.Query<FlowerPost>(query1);
                }
                else if (type.Equals("puokste"))
                {
                    string query2 = string.Format("UPDATE BouquetPost SET Favorite = NOT Favorite WHERE Id = '{0}'", id);
                    conn.Query<BouquetPost>(query2);
                }
                else if (type.Equals("kitas"))
                {
                    string query3 = string.Format("UPDATE Home_StuffPost SET Favorite = NOT Favorite WHERE Id = '{0}'", id);
                    conn.Query<Home_StuffPost>(query3);
                }
            }
            App.Current.MainPage.Navigation.PushAsync(new FavouritePage());
        }
        public async void shop_img_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BuyPage());

        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("fav");
            return true;
        }
    }
}
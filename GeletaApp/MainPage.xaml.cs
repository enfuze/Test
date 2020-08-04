using GeletaApp.Logic;
using GeletaApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeletaApp
{
    public partial class MainPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            // Screen density
            paieska_main_st.WidthRequest = xamarinWidth * 73.61 / 100;
            shop_img.HeightRequest = xamarinHeight * 3.229 / 100;
            main_cp.Padding = new Thickness(0, xamarinHeight * 3.125 / 100, 0, xamarinHeight * 0.885 / 100);
            virsutinis_st.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            paieskos_juostos_entry.HeightRequest = xamarinHeight * 5.15625 / 100;
            iconImage.HeightRequest = xamarinHeight * 5.3125 / 100;
            //  iconImage.WidthRequest = xamarinWidth * (569 * 100 / width) / 100;
            main_scroll.Padding = new Thickness(0, xamarinHeight * 3.54166 / 100, 0, 0);
            main_label.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 3.489 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.5625 / 100);
            main_label.HeightRequest = xamarinHeight * 15.625 / 100;
            main_label2.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 3.489 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.5625 / 100);
            main_label2.HeightRequest = xamarinHeight * 15.625 / 100;
            main_label3.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 3.489 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.5625 / 100);
            main_label3.HeightRequest = xamarinHeight * 15.625 / 100;
            main_label.FontSize = xamarinHeight * 2.60 / 100;
            main_label2.FontSize = xamarinHeight * 2.60 / 100;
            main_label3.FontSize = xamarinHeight * 2.60 / 100;

            paieskos_linija.WidthRequest = xamarinWidth * 57.407 / 100;
            paieskos_juostos_entry.WidthRequest = xamarinWidth * 57.407 / 100;
            image1.WidthRequest = xamarinWidth;
            // image1.HeightRequest = xamarinHeight * 3.64583 / 100;
            image3.WidthRequest = xamarinWidth;
            //   image3.HeightRequest = xamarinHeight * 3.6458 / 100;
            //   image2.HeightRequest = xamarinHeight * 3.6458 / 100;
            image2.WidthRequest = xamarinWidth;
            customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);

            paieska.FontSize = xamarinHeight * 1.7359375 / 100;
            paieskos_juostos_entry.FontSize = xamarinHeight * 1.7359375 / 100;

            // Krepšelio nuskaitymas ir jo kiekio atvaizdavimas ant krepšelio ikonos
            List<CartItemPost> cart = DataManipulation_Logic.ReadCart();
            int cart_product_amount = cart.Count();
            kiekisKrepselyje.Text = cart_product_amount.ToString();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            /*using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<CartItemPost>();
                conn.DropTable<CartItemPost>();
                conn.Close();
            }*/

            //Custominis alertas


            /*
            DependencyService.Get<IBackgroundDependency>().ExecuteCommand("Click OK to navigate", "Title", "OK",
                    () => { this.Navigation.PushAsync(new BouquetsPage()); });
            */





            // --- Jeigu nori daryt kažką kai pagauna kad esi NavStack'o pabaigoj
            //Page currentPage = Navigation.NavigationStack.LastOrDefault();
            //if (currentPage == this)
            //{
            //}
            // ----------------------------------------------------
        }

        private void stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            //displayLabel1.Text = String.Format(" {0}", e.NewValue);
        }

        public async void shop_img_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BuyPage());

        }

        private async void isekoti_tap_Tapped(object sender, EventArgs e)
        {
            bool isEmpty = string.IsNullOrEmpty(paieskos_juostos_entry.Text);
            if (!isEmpty)
            {
                string searchKey = paieskos_juostos_entry.Text;
                var testas = Functions.Search(searchKey);
                await App.Current.MainPage.Navigation.PushAsync(new SearchResultPage(testas));
            }

        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("main");
            return true;
        }
    }
}

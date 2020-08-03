using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GeletaApp.Logic;
using GeletaApp.Model;
using Rg.Plugins.Popup.Services;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultPage : ContentPage
    {
        public SearchResultPage(List<ProductObject> listas)
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
            customMenu.Padding = new Thickness(xamarinWidth * 1.2037 / 100, 0, xamarinWidth * 1.2037 / 100, 0);
            puokstes_cp.Padding = new Thickness(xamarinWidth * 2.037 / 100, xamarinHeight * 2.1875 / 100, xamarinWidth * 2.037 / 100, xamarinHeight * 0.9375 / 100);
            puoktes_label.FontSize = xamarinHeight * 3.3854 / 100;
            puoktes_label.Margin = new Thickness(0, 0, 0, xamarinHeight * 1.302 / 100);
            SearchList.HeightRequest = xamarinHeight;
            SearchList.WidthRequest = xamarinWidth;
            //puoktes_label.FontSize = xamarinHeight * 4.79 / 100;
            grid_layout.HorizontalItemSpacing = xamarinWidth * 1.666 / 100;

            SearchList.ItemsSource = listas;

               
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        [Obsolete]
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {

            var imageSender = sender as Grid;
            var asd = imageSender.RowDefinitions.ToArray();
            var xasd = asd[0].BindingContext as ProductObject;

            int id = xasd.Product_id;
            string type = xasd.Product_type;
            int tipas = 0 ;
            if (type.Equals("gele"))
            {
                tipas = 1;
            } else if (type.Equals("puokste"))
            {
                tipas = 2;
            } else if (type.Equals("kitas"))
            {
                tipas = 3;
            }
            Navigation.PushAsync(new DetailedProduct(id, tipas));
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("push");
            return true;
        }

        /* void add_to_favourites_Tapped(object sender, EventArgs e)
         {
             heart_img.IsVisible = true;
             Task.Delay(200);
             heart_img.IsVisible = false;
         }*/
    }
}
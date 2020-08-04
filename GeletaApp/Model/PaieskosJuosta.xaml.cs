using GeletaApp.Logic;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp.Model
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaieskosJuosta : Frame
    {

        public PaieskosJuosta()
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Orientation (Landscape, Portrait, Square, Unknown)
            var orientation = mainDisplayInfo.Orientation;

            // Rotation (0, 90, 180, 270)
            var rotation = mainDisplayInfo.Rotation;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            // Screen density
            var density = mainDisplayInfo.Density;
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            // paieskos_juostos_frame.HeightRequest = xamarinHeight * (62 * 100 / height) / 100;
            paieskos_juostos_frame.Padding = new Thickness(0, 0, 0, xamarinHeight * 3.645 / 100);
            paieskos_stack.HeightRequest = xamarinHeight * 3.645 / 100;
            paieska_st.WidthRequest = xamarinWidth * 73.611 / 100;
            //  paieska.Padding = new Thickness(xamarinWidth * (-75 * 100 / width) / 100, xamarinHeight * (25 * 100 / height) / 100, 0, xamarinHeight * (25 * 100 / height) / 100);
            //     shop_img.HeightRequest = xamarinHeight * (70 * 100 / height) / 100;
            funnel.HeightRequest = xamarinHeight * 3.229 / 100;
            paieskos_juostos_entry.WidthRequest = xamarinWidth * 60 / 100;
            paieskos_linija.WidthRequest = xamarinWidth * 60 / 100;
            paieskos_st.WidthRequest = xamarinWidth * 60 / 100;
            paieskos_juostos_entry.HeightRequest = xamarinHeight * 3.229 / 100;
            paieska.FontSize = xamarinHeight * 1.84 / 100;
            paieskos_juostos_entry.FontSize = xamarinHeight * 1.736 / 100;

            // Krepšelio nuskaitymas ir jo kiekio atvaizdavimas ant krepšelio ikonos
            List<CartItemPost> cart = DataManipulation_Logic.ReadCart();
            int cart_product_amount = cart.Count();
            kiekisKrepselyje.Text = cart_product_amount.ToString();

        }

        private async void shop_img_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BuyPage());
        }
        [Obsolete]
        private void funnel_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.PushAsync(new RikiavimasPage());
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
    }
}
using GeletaApp.Logic;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
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

            //Virsutines juostos atitraukimai
            orders_st.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885416 / 100);

            //Contento atitraukimas
            frame1.Margin = new Thickness(xamarinWidth * -3.24 / 100, xamarinHeight * 6.35 / 100, xamarinWidth * -3.24 / 100, 0);



            orders_cp.HeightRequest = xamarinHeight;
            orders_cp.WidthRequest = xamarinWidth;
            //customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885 / 100);
            // virsutineJuosta.HeightRequest = xamarinHeight * 10.4166/ 100;
            //virsutineJuosta.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, 0, xamarinHeight * 4.3229 / 100);
            uzsakymai.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            BackImgButton.WidthRequest = xamarinWidth * 6.8518 / 100;
        }

        private void BackImgButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("pop_page");
            return true;
        }
    }
}
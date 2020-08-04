using GeletaApp.Logic;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Position = Xamarin.Forms.Maps.Position;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
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

            mapPage.HeightRequest = xamarinHeight;
            mapPage.WidthRequest = xamarinWidth;
            mapPage.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885 / 100);
            parduotuves.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            BackImgButton3.WidthRequest = xamarinWidth * 6.8518 / 100;
            sarasoStack.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 5.15625 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 11.6145 / 100);

            PridetiPinus();
        }
        private void PridetiPinus()
        {

            Pin pin = new Pin
            {
                Label = "Savanorių pr.",
                Address = "Savanorių pr. 255, HyperMaxima",
                Type = PinType.Place,
                Position = new Position(54.9144657, 23.9363115)
            };
            Pin pin2 = new Pin
            {
                Label = "Pramonės pr.",
                Address = "Pramonės pr. 29, PC “Maxima”",
                Type = PinType.Place,
                Position = new Position(54.9120509, 23.981352)
            };

            map.Pins.Add(pin);
            map.Pins.Add(pin2);
        }
        private async void BackImgButton_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("pop_page");
            return true;
        }
    }
}
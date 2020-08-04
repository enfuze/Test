using GeletaApp.Logic;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivarumoPolitikaPage : ContentPage
    {
        public PrivarumoPolitikaPage()
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
            privatumo_stc.HeightRequest = xamarinHeight;
            privatumo_stc.WidthRequest = xamarinWidth;
            privatumo_stc.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, xamarinHeight * 0.8854 / 100);
            virsutineJuosta.HeightRequest = xamarinHeight * 10.4166 / 100;
            privatumoLabel.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            kitaLabel.FontSize = xamarinHeight * 2.6041 / 100;
            BackImgButton4.WidthRequest = xamarinWidth * 6.8518 / 100;

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
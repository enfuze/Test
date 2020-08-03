using System;
using GeletaApp.Logic;
using GeletaApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SusisiekimePage : ContentPage
    {
        public SusisiekimePage()
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

            susisiekime_cp.HeightRequest = xamarinHeight;
            susisiekime_cp.WidthRequest = xamarinWidth;
            susisiekime_st.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885/ 100);
            grid1.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid2.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid1.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid2.HeightRequest = xamarinHeight * 6.5104 / 100;
            frame1.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame2.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            susisiekime.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            BackImgButton1.WidthRequest = xamarinWidth * 6.85185 / 100;
            arrow1.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow2.WidthRequest = xamarinWidth * 4.8148 / 100;
            nespalvotas_logo.WidthRequest = xamarinWidth * 64.81 / 100;
            aprasymas.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 4.166 / 100, 0, xamarinHeight * 4.6875 / 100);

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

        private async void chatas_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ChatPage());
        }
    }
}
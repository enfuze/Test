using GeletaApp.Logic;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
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

            menu_menu_cp.HeightRequest = xamarinHeight;
            menu_menu_cp.WidthRequest = xamarinWidth;
            menu_m_stc.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885416 / 100);
            frame1.Margin = new Thickness(xamarinWidth * -3.24 / 100, xamarinHeight * 6.35 / 100, xamarinWidth * -3.24 / 100, 0);
            frame2.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame3.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame4.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame5.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            grid1.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid2.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid3.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid4.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid5.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid1.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid2.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid3.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid4.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid5.HeightRequest = xamarinHeight * 6.5104 / 100;
            meniu.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            BackImgButton3.WidthRequest = xamarinWidth * 6.85185 / 100;
            arrow1.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow2.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow3.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow4.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow5.WidthRequest = xamarinWidth * 4.8148 / 100;
            //nespalvotas_logo.HeightRequest = xamarinHeight * 31.25 / 100;
            nespalvotas_logo.WidthRequest = xamarinWidth * 64.81 / 100;

        }

        private async void BackImgButton_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("push");
            return true;
        }
        private async void Main_Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        private async void susisiekime_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new SusisiekimePage());
        }

        private async void duk_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new DUKPage());
        }

        private async void privarumas_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new PrivarumoPolitikaPage());
        }

        private async void parduotuves_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new MapPage());

        }
    }
}
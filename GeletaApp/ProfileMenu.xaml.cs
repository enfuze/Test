using System;
using System.Diagnostics;
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
    public partial class ProfileMenu : ContentPage
    {
        public ProfileMenu()
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
            prof_m_stc.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885416 / 100);
            frame1.Margin = new Thickness(xamarinWidth * -3.24 / 100, xamarinHeight * 6.35 / 100, xamarinWidth * -3.24 / 100, 0);
            frame2.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame3.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame4.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame5.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            frame6.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            grid1.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid2.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid3.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid4.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid5.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid6.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            grid1.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid2.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid3.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid4.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid5.HeightRequest = xamarinHeight * 6.5104 / 100;
            grid6.HeightRequest = xamarinHeight * 6.5104 / 100;
            profilis.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            BackImgButton2.WidthRequest = xamarinWidth * 6.85185 / 100;
            arrow.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow2.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow3.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow4.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow5.WidthRequest = xamarinWidth * 4.8148 / 100;
            arrow6.WidthRequest = xamarinWidth * 4.8148 / 100;
            //nespalvotas_logo.HeightRequest = xamarinHeight * 30 / 100;
            nespalvotas_logo.WidthRequest = xamarinWidth * 64.81 / 100;


            UserAuth.GetAddresses();
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
        private async void eiti_i_profili_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProfilePage());
        }

        private async void adresai_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AdressPage());
        }

        private async void uzsakymai_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new OrdersPage());
        }

        private void megstamiausios_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new FavouritePage());

        }

        [Obsolete]
        private async void slaptazodis_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PasswordChangePage());
        }

        private async void atsijungti_Tapped(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Atsijungti", "Ar tikrai norite atsijungti?", "Taip", "Ne");
            if (answer)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<UserPost>();
                    conn.DropTable<UserPost>();
                    conn.CreateTable<UserAddressPost>();
                    conn.DropTable<UserAddressPost>();
                    conn.Close();
                }
                await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
        }
    }
}
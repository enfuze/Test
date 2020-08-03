using GeletaApp.Logic;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
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
            loginCP.HeightRequest = xamarinHeight;
            loginCP.WidthRequest = xamarinWidth;
            logas.HeightRequest = xamarinHeight * 30.625 / 100;
            loginST.Padding = new Thickness(xamarinWidth * 5.7407 / 100, xamarinHeight * 7.1875/100, xamarinWidth * 5.7407 / 100, xamarinHeight * 0.885 / 100);
            prisijungti_naudojant.FontSize = xamarinHeight * 2.325 / 100;
            prisijungti_naudojant.Padding = new Thickness(xamarinWidth * 8.796 / 100, xamarinHeight * 5.677083 / 100, xamarinWidth * 8.796 / 100, xamarinHeight * 1.0416 / 100);
            el_pastas.HeightRequest = xamarinHeight * 3.59375 / 100;
            el_pastas.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            emailEntry.HeightRequest = xamarinHeight * 3.59375 / 100;
            emailEntry.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            slaptazodisLabel.HeightRequest = xamarinHeight * 3.59375 / 100;
            slaptazodisLabel.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            passwordEntry.HeightRequest = xamarinHeight * 3.59375 / 100;
            passwordEntry.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100,0);
            arbaLabel.Padding = new Thickness(xamarinWidth * 8.796 / 100, xamarinHeight * 6.25 / 100, xamarinWidth * 8.796 / 100, xamarinHeight * 1.0416 / 100);
            grid.HeightRequest = xamarinHeight * 15.72 / 100;
            grid.WidthRequest = xamarinWidth * 42.87 / 100;
            grid.Margin = new Thickness(xamarinWidth * 22.87 / 100, 0, xamarinWidth * 22.87 / 100, 0);
            google.HeightRequest = xamarinHeight * 8.85 / 100;
            facebook.HeightRequest = xamarinHeight * 8.85 / 100;
            pamirsauSlap.HeightRequest = xamarinHeight * 3.59375 / 100;
            frame1.Margin = new Thickness(0, 0, 0, xamarinHeight * 2.083 / 100);
        }
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
               await  DisplayAlert("Klaida", "Prašome užpildyti prisijungimo duomenis", "Uždaryti");
            }
            else
            {
                string email = emailEntry.Text;
                string password = passwordEntry.Text;
                var test =  await UserAuth.Login(email, password);
                if(test != "")
                {
                    await DisplayAlert("Klaida", test, "Uždaryti");
                }
                else
                {
                    
                    await App.Current.MainPage.Navigation.PushAsync(new ProfileMenu());
                }
            }
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("push");
            return true;
        }

        private async void primintiSlap_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("Alert", "priminti slaptazodi", "OK");
        }
    }
}
using GeletaApp.Logic;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
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
            registerCP.HeightRequest = xamarinHeight;
            registerCP.WidthRequest = xamarinWidth;
            logas.HeightRequest = xamarinHeight * 30.625 / 100;
            registerST.Padding = new Thickness(xamarinWidth * 5.7407 / 100, xamarinHeight * 7.1875 / 100, xamarinWidth * 5.7407 / 100, xamarinHeight * 0.885 / 100);
            registracija.FontSize = xamarinHeight * 2.825 / 100;
            registracija.Padding = new Thickness(xamarinWidth * 8.796 / 100, xamarinHeight * 5.677083 / 100, xamarinWidth * 8.796 / 100, xamarinHeight * 1.0416 / 100);
            elPastas.HeightRequest = xamarinHeight * 3.59375 / 100;
            elPastas.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            emailEntry.HeightRequest = xamarinHeight * 3.59375 / 100;
            emailEntry.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            slaptazodisLabel.HeightRequest = xamarinHeight * 3.59375 / 100;
            slaptazodisLabel.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            patvirtintiSlaptazodi.HeightRequest = xamarinHeight * 3.59375 / 100;
            patvirtintiSlaptazodi.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            passwordEntry.HeightRequest = xamarinHeight * 3.59375 / 100;
            passwordEntry.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            confirmpasswordEntry.HeightRequest = xamarinHeight * 3.59375 / 100;
            confirmpasswordEntry.Margin = new Thickness(xamarinWidth * 8.796 / 100, 0, xamarinWidth * 8.796 / 100, 0);
            frame1.Margin = new Thickness(0, 0, 0, xamarinHeight * 2.083 / 100);
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            bool isConfirmPasswordEmpty = string.IsNullOrEmpty(confirmpasswordEntry.Text);



            if (isEmailEmpty || isPasswordEmpty || isConfirmPasswordEmpty)
            {
                await DisplayAlert("Klaida", "Prašome užpildyti visus registracijos laukus", "Uždaryti");
            }
            else
            {
                string password = passwordEntry.Text;
                string confirmPassword = confirmpasswordEntry.Text;
                if (password != confirmPassword)
                {
                    await DisplayAlert("Klaida", "Slaptažiai nesutampa", "Uždaryti");
                }
                else
                {
                    string email = emailEntry.Text;
                    var test = await UserAuth.Register(email, password);
                    if (test != "")
                    {
                        await DisplayAlert("Klaida", test, "Uždaryti");
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new ProfileMenu());
                    }
                }
            }
        }
    }
}
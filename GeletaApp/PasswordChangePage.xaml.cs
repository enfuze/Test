using GeletaApp.Logic;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordChangePage
    {
        public PasswordChangePage()
        {
            InitializeComponent();
        }

        private async void atsauktiBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void issaugotiBtn_Clicked(object sender, EventArgs e)
        {
            string oldPassword = senasPass.Text;
            string newPassword = naujasPass.Text;
            string repeatPassword = pakartotasPass.Text;
            if (oldPassword.Equals(newPassword))
            {
                await App.Current.MainPage.DisplayAlert("Klaida", "Naujas ir senas slaptažodžiai sutampa", "Uždaryti");
            }
            else if (repeatPassword.Equals(newPassword))
            {
                string result = await UserAuth.ChangePass(oldPassword, newPassword);
                if (result != "")
                {
                    await App.Current.MainPage.DisplayAlert("Klaida", result, "Uždaryti");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Pranešimas", "Slaptažodis sėkmingai pakeistas", "Uždaryti");
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Klaida", "Naujas slaptažodis nesutampa", "Uždaryti");
            }
        }
    }
}
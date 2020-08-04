using GeletaApp.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasirinktiPristatymaPage
    {
        public event EventHandler<object> CallbackEvent;

        public string address_product = "";
        public PasirinktiPristatymaPage()
        {
            InitializeComponent();
        }

        private void pristatymo_radio_button_Selected(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            address_product = radio.ClassId.ToString();
        }
        private async void atsauktiBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void idetiIkrepseliBtn_Clicked(object sender, EventArgs e)
        {
            var activity = Android.App.Application.Context;
            CallbackEvent?.Invoke(address_product, EventArgs.Empty);

            switch (address_product)
            {
                case "maxima":
                    PopupNavigation.Instance.PopAsync();
                    Utils.showToast(activity, "Prekė pridėta į krepšelį.");
                    break;
                case "maxima_pramones":
                    PopupNavigation.Instance.PopAsync();
                    Utils.showToast(activity, "Prekė pridėta į krepšelį.");
                    break;
                case "namai":
                    PopupNavigation.Instance.PopAsync();
                    Utils.showToast(activity, "Prekė pridėta į krepšelį.");
                    break;
                default:
                    DisplayAlert("Neparinktas adresas", "Prašome pasirinkti prekės pristatymo/atsiemimo adresą", "OK");
                    break;
            }
        }
    }
}
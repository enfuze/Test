using System;
using System.Collections.Generic;
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
    public partial class AdressPage : ContentPage
    {
        public AdressPage()
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
            address_cp.HeightRequest = xamarinHeight;
            address_cp.WidthRequest = xamarinWidth;
            customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885 / 100);
           // virsutineJuosta.HeightRequest = xamarinHeight * 10.416 / 100;
            //virsutineJuosta.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            BackImgButton.WidthRequest = xamarinWidth * 6.85185 / 100;
            adr_m_stc.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885416 / 100);



            ///vardas.Padding = 

            // frame1.Margin = new Thickness(0, 1, 0, 1);
            // frame1.HeightRequest = xamarinHeight * 29.739 / 100;

            /*stac1.HeightRequest = xamarinHeight * 29.739 / 100;
            prideti_adresa.HeightRequest = xamarinHeight * 5.26 / 100;
            listView.HeightRequest = xamarinHeight * 24.479 / 100;
            listView.RowHeight = (int)(xamarinHeight * 24.479 / 100);*/

            stac1.HeightRequest = xamarinHeight * 29.739 / 100;
            stac1.Padding = new Thickness(0, xamarinWidth * 8.8888 / 100, 0, 0);
            prideti_adresa.HeightRequest = xamarinHeight * 5.26 / 100;
            listView.HeightRequest = xamarinHeight * 28.5 / 100;
            listView.RowHeight = (int)(xamarinHeight * 28.5 / 100);


            //adresas.Padding = new Thickness(xamarinWidth * 9.537 / 100, 0, 0, 0);
            //pagrAdresas.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, 0, 0);
            //redAdresa.Padding = new Thickness(0, 0, xamarinWidth * 3.24 / 100, 0);

           // prideti_adresa.HeightRequest = xamarinHeight * 6.61458 / 100;

            adresai.Padding = new Thickness(xamarinWidth * 8.8888 / 100, 0, 0, 0);

            List<UserAddressPost> addressList = new List<UserAddressPost>();
            List<UserAddress> addressListToDisplay = new List<UserAddress>();
            
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                //conn.CreateTable<UserAddressPost>();
                //conn.DropTable<UserAddressPost>();
                conn.CreateTable<UserAddressPost>();
                conn.CreateTable<UserPost>();
                var userAddress = conn.Table<UserPost>().First();
                UserAddress address = new UserAddress() {
                    id = 0,
                    address = userAddress.Address,
                city = userAddress.City,
                name = userAddress.Name,
                phone_number = userAddress.Phone_number.ToString(),
                postal_code = userAddress.Postal_code.ToString(),
                isDefault = true
            };
                

                addressListToDisplay.Add(address);

                addressList = conn.Table<UserAddressPost>().ToList();


                int count = addressList.Count;
                if(count > 0)
                {
                    for (int i = 0; i < count; i++)
                {
                    UserAddress address2 = new UserAddress()
                    {
                        id = addressList[i].Id,
                    address = addressList[i].Address,
                    city = addressList[i].City,
                    name = addressList[i].Name,
                    phone_number = addressList[i].Phone_number.ToString(),
                    postal_code = addressList[i].Postal_code.ToString(),
                    isDefault = false

                };

                addressListToDisplay.Add(address2);
                    }
                listView.HeightRequest = (count+1) * listView.HeightRequest;
                stac1.HeightRequest = (count+1) * stac1.HeightRequest;
                }

                
                conn.Close();
            }
            listView.ItemsSource = addressListToDisplay;

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
        [Obsolete]
        private async void redaguotiAdresa_Tapped(object sender, EventArgs e)
        {
            var data = sender as Label;
            var gaminu = data.Parent;
            var addressObject = gaminu.BindingContext as UserAddress;
            

            await PopupNavigation.PushAsync(new NewAddressPage(addressObject));
        }

        [Obsolete]
        private async void btnPopupButton_Clicked(object sender, EventArgs e)
        {
           
            await PopupNavigation.PushAsync(new NewAddressPage());
        }
        private async void deleteImg_Clicked(object sender, EventArgs e)
        {
            var choice = await DisplayAlert("", "Ar norite pašalinti Adresą", "TAIP", "NE");
            if (choice)
            {
                var imageSender = sender as ImageButton;
                var gaminu = imageSender.Parent;
                var pagaminau = gaminu.BindingContext as UserAddress;
                int id = pagaminau.id;
                string response = await UserAuth.DeleteUserAddress(id);
                if(response == "Pavyko")
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.Delete<UserAddressPost>(id);
                        conn.Close();
                    }
                    Navigation.PopAsync();
                    Navigation.PushAsync(new AdressPage());

                }
                else
                {
                    await DisplayAlert("Klaida", "Įvyko klaida", "Uždaryti");
                }

               
            }
        }

    }
}
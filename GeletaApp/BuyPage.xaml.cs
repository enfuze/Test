using Expandable;
using GeletaApp.Logic;
using GeletaApp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyPage : ContentPage
    {
        public static List<CartItemPost> prekes = new List<CartItemPost>();
        public string address_order = "";
        double galutine_kaina = 0;
        double galutine_kaina_namai = 0;
        public BuyPage(string query = "")
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            buy_cp.HeightRequest = xamarinHeight;
            buy_cp.WidthRequest = xamarinWidth;
            buy_st.Padding = new Thickness(0, xamarinHeight * 2.03125 / 100, 0, xamarinHeight * 0.885 / 100);
            virsutineJuosta.HeightRequest = xamarinHeight * 10.416 / 100;
            BackImgButton4.WidthRequest = xamarinWidth * 6.8518 / 100;
            virsutineJuosta.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, 0, 0);
            krepselis.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            // frame1.HeightRequest = xamarinHeight * (375 * 100 / height) / 100;
            suma.FontSize = xamarinHeight * 2.5 / 100;
            kaina.FontSize = xamarinHeight * 2.5 / 100;
            stac.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            //  sarasoStack.HeightRequest = xamarinHeight - expandableView.SecondaryView.FindByName<Grid>("grid").HeightRequest - customMenu.HeightRequest - virsutineJuosta.HeightRequest - frame1.HeightRequest;
            customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            //expandableView.SecondaryView.FindByName<Grid>("grid").HeightRequest = xamarinHeight * 19.53125 / 100;
            //expandableView.SecondaryView.FindByName<RadioButton>("nemokamas_radio").FontSize = xamarinHeight * 1.8 / 100;
            //expandableView.SecondaryView.FindByName<RadioButton>("mokamas_radio").FontSize = xamarinHeight * 1.8 / 100;
            //expandableView.SecondaryView.FindByName<Label>("mokamas").FontSize = xamarinHeight * 1.5 / 100;
            //expandableView.SecondaryView.FindByName<Label>("nemokamas").FontSize = xamarinHeight * 1.5 / 100;
            //mokejimoButton.HeightRequest = xamarinHeight * 5.26 / 100;
            //CartItemList.HeightRequest = xamarinHeight - expandableView.SecondaryView.FindByName<Grid>("grid").HeightRequest - customMenu.HeightRequest - virsutineJuosta.HeightRequest - frame1.HeightRequest;
            //pristatytmoStack.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            //expandableView.SecondaryView.FindByName<Grid>("grid").Padding = new Thickness(xamarinWidth * 4.722 / 100, 0, 0, 0);
            //pristatymas.FontSize = xamarinHeight * 2 / 100;

            //do read of cart
            prekes = DataManipulation_Logic.ReadCart();
            if (prekes.Count > 0)
            {
                CartItemList.ItemsSource = prekes;
            }
            else
            {
                //CartItemList.ItemsSource = null;
                nera_rezultatu.IsVisible = true;
            }

            //user_adresai = UserAuth.GetAddresses();
        }

        private void BackImgButton_Clicked(object sender, EventArgs e)
        {
            List<Page> li = Navigation.NavigationStack.ToList();
            int last_index = 2; // 0 - out of index range, 1 - current page, 2 - last page, 3 - if the last page was not enough
            Page last = li.ElementAt(li.Count - last_index);
            string page = last.ToString();
            while (page == "GeletaApp.DetailedProduct")
            {
                last_index++;
                last = li.ElementAt(li.Count - last_index);
                page = last.ToString();
            }
            switch (page)
            {
                case "GeletaApp.FlowersPage":
                    App.Current.MainPage.Navigation.PushAsync(new FlowersPage(), true);
                    break;
                case "GeletaApp.BouquetsPage":
                    App.Current.MainPage.Navigation.PushAsync(new BouquetsPage(), true);
                    break;
                case "GeletaApp.OtherGoodsPage":
                    App.Current.MainPage.Navigation.PushAsync(new OtherGoodsPage(), true);
                    break;
                default:
                    App.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
                    break;
            }
        }

        private async void mokejimoButton_Clicked(object sender, EventArgs e)
        {
            switch (address_order)
            {
                case "maxima":

                    for (int i = 0; i < prekes.Count; i++)
                    {
                        if (prekes[i].Pickup_address_type != "maxima")
                        {
                            await DisplayAlert("Įspėjimas", "Ar tikrai norite visas prekes atsiimti mūsų parduotuvėje?", "OK", "Atšaukti");
                            break;
                        }
                    }
                    break;
                case "namai":
                    for (int i = 0; i < prekes.Count; i++)
                    {
                        if (prekes[i].Pickup_address_type != "namai")
                        {
                            await DisplayAlert("Įspėjimas", "Ar tikrai norite, kad visos prekės būtų pristatytos kurjeriu?", "OK", "Atšaukti");
                            break;
                        }

                    }
                    break;
                default:
                    await DisplayAlert("Neparinktas adresas", "Prašome pasirinkti prekės pristatymo/atsiemimo adresą", "OK");
                    break;
            }
            // if logged in then go to mokėjimas
            // else go to loginpage.
            //await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

        }

        [Obsolete]
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {

            var imageSender = sender as Grid;
            var asd = imageSender.RowDefinitions.ToArray();
            var xasd = asd[0].BindingContext as CartItemPost;

            int id = 0;
            var gele = xasd.Flower_id;
            var puokste = xasd.Bouquet_id;
            var kita = xasd.Home_stuff_id;
            int tipas = 0;
            if (gele > 0)
            {
                id = gele;
                tipas = 1;
            }
            else if (puokste > 0)
            {
                id = puokste;
                tipas = 2;
            }
            else if (kita > 0)
            {
                id = kita;
                tipas = 3;
            }
            Navigation.PushAsync(new DetailedProduct(id, tipas));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //expandableView.StatusChanged += OnStatusChanged;

            if (prekes.Count > 0)
            {
                for (int i = 0; i < prekes.Count; i++)
                {
                    if (prekes[i].Pickup_address_type == "namai")
                    {
                        galutine_kaina_namai += prekes[i].Price; // hardcoded pickup cost :))))
                    }
                    else
                    {
                        galutine_kaina += prekes[i].Price;
                    }
                }
                if (galutine_kaina_namai != 0)
                {
                    galutine_kaina_namai += 5;
                }
                var gabija = galutine_kaina + galutine_kaina_namai;
                kaina.Text = gabija + " €";
            }
            else
            {
                //frame1.IsVisible = false;
                stac.IsVisible = false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //expandableView.StatusChanged -= OnStatusChanged;

        }

        private async void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            //if (expandableView.IsExpanded)
            //{
            //    await arrow.RotateTo(180, 200, Easing.CubicInOut);
            //}

            //if (!expandableView.IsExpanded)
            //{
            //    await arrow.RotateTo(0, 200, Easing.CubicInOut);
            //}

        }
        protected override bool OnBackButtonPressed()
        {
            List<Page> li = Navigation.NavigationStack.ToList();
            if (li.Count != 0)
            {
                int last_index = 2; // 0 - out of index range, 1 - current page, 2 - last page, 3 - if the last page was not enough
                Page last = li.ElementAt(li.Count - last_index);
                string page = last.ToString();
                while (page == "GeletaApp.DetailedProduct")
                {
                    last_index++;
                    last = li.ElementAt(li.Count - last_index);
                    page = last.ToString();
                }
                Functions tools = new Functions();
                tools.BackButtonWithExit("buy", page);
            }
            return true;
        }
        private async void deleteImg_Clicked(object sender, EventArgs e)
        {
            var choice = await DisplayAlert("", "Ar norite pašalinti prekę iš krepšelio?", "TAIP", "NE");
            if (choice)
            {
                var imageSender = sender as ImageButton;
                var gaminu = imageSender.Parent;
                var pagaminau = gaminu.BindingContext as CartItemPost;
                int id = pagaminau.Id;
                //var cart_items = DataManipulation_Logic.ReadCart();
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.Delete<CartItemPost>(id);
                    conn.Close();
                }
                await App.Current.MainPage.Navigation.PushAsync(new BuyPage());
            }
        }
        public void showAlertDialogButtonClicked()
        {

        }
        private void pristatymoButton_Clicked(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;

            //    address_order = radio.ClassId.ToString();
            //    switch (address_order)
            //    {
            //        case "namai":
            //            //double pristatymo_kaina = Functions.GetPriceNumber(mokamas.Text);
            //            //double pristatymo_galutine_kaina = galutine_kaina + pristatymo_kaina;
            //            //kaina.Text = pristatymo_galutine_kaina + " €";
            //            break;
            //        default:
            //            kaina.Text = galutine_kaina + " €";
            //            break;
            //    }
        }
    }
}
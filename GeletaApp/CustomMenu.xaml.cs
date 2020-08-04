using GeletaApp.Model;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMenu : StackLayout
    {
        public CustomMenu()
        {
            InitializeComponent();
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Orientation (Landscape, Portrait, Square, Unknown)
            var orientation = mainDisplayInfo.Orientation;

            // Rotation (0, 90, 180, 270)
            var rotation = mainDisplayInfo.Rotation;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            // Screen density
            var density = mainDisplayInfo.Density;

            menu_sl.Padding = new Thickness(0, xamarinHeight * 1.5625 / 100, 0, 0);
            CustomToolBarItem.HeightRequest = xamarinHeight * 7.604 / 100;
            puokste_img.WidthRequest = xamarinWidth * 5.74 / 100;
            tulip_img.WidthRequest = xamarinWidth * 5.74 / 100;
            kitos_img.WidthRequest = xamarinWidth * 5.7 / 100;
            profile_img.WidthRequest = xamarinWidth * 5.7 / 100;
            menu.WidthRequest = xamarinWidth * 5.74 / 100;
            meniu_label.FontSize = xamarinHeight * 1.215 / 100;
            tulpes_label.FontSize = xamarinHeight * 1.215 / 100;
            puokstes_label.FontSize = xamarinHeight * 1.215 / 100;
            kitos_label.FontSize = xamarinHeight * 1.215 / 100;
            profilio_label.FontSize = xamarinHeight * 1.215 / 100; ;

        }

        private void puokste_img_Clicked(object sender, EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.Instance.PopAsync();

            var last_page = Navigation.NavigationStack.Last();
            if (last_page.ToString() != "GeletaApp.BouquetsPage")
            {
                puokste_img.Source = "puokstesROZ50px.png";
                puokstes_label.TextColor = Color.FromHex("#F7E3E3");
                this.Navigation.PushAsync(new BouquetsPage());
            }
        }
        private void tulip_img_Clicked(object sender, EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.Instance.PopAsync();

            var last_page = Navigation.NavigationStack.Last();
            if (last_page.ToString() != "GeletaApp.FlowersPage")
            {
                tulip_img.Source = "skintos_gelesROZ50px.png";
                tulpes_label.TextColor = Color.FromHex("#F7E3E3");
                this.Navigation.PushAsync(new FlowersPage());
            }
        }

        private void kitos_Clicked(object sender, EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.Instance.PopAsync();

            var last_page = Navigation.NavigationStack.Last();
            if (last_page.ToString() != "GeletaApp.OtherGoodsPage")
            {
                kitos_img.Source = "ktprekesROZ50px.png";
                kitos_label.TextColor = Color.FromHex("#F7E3E3");
                this.Navigation.PushAsync(new OtherGoodsPage());
            }
        }

        private void profile_img_Clicked(object sender, EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.Instance.PopAsync();

            var last_page = Navigation.NavigationStack.Last();
            if (last_page.ToString() != "GeletaApp.ProfileMenu")
            {
                profile_img.Source = "profilisROZ50px.png";
                profilio_label.TextColor = Color.FromHex("#F7E3E3");
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<UserPost>();
                    try
                    {
                        var posts = conn.Table<UserPost>().ToList();
                        if (posts.Count == 0)
                        {
                            this.Navigation.PushAsync(new LoginPage());
                        }
                        else
                        {
                            this.Navigation.PushAsync(new ProfileMenu());
                        }

                    }
                    catch (NullReferenceException nrex)
                    {

                    }
                    conn.Close();
                }
            }
        }

        private void menu_Clicked(object sender, EventArgs e)
        {
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.Instance.PopAsync();

            var last_page = Navigation.NavigationStack.Last();
            if (last_page.ToString() != "GeletaApp.MenuPage")
            {
                menu.Source = "meniuROZ50px";
                meniu_label.TextColor = Color.FromHex("#F7E3E3");
                this.Navigation.PushAsync(new MenuPage());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    public partial class FlowersPage : ContentPage
    {
        public FlowersPage(string query = "")
        {
            NavigationPage.SetHasNavigationBar(this, false);
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

            puokstes_cp.WidthRequest = xamarinWidth;
            puokstes_cp.HeightRequest = xamarinHeight;
            customMenu.Padding = new Thickness(xamarinWidth * 1.2037 / 100, 0, xamarinWidth * 1.2037 / 100, 0);
            puokstes_cp.Padding = new Thickness(xamarinWidth * 2.037 / 100, xamarinHeight * 2.1875 / 100, xamarinWidth * 2.037 / 100, xamarinHeight * 0.9375 / 100);
            puoktes_label.FontSize = xamarinHeight * 3.3854 / 100;
            puoktes_label.Margin = new Thickness(0, 0, 0, xamarinHeight * 1.302 / 100);
            FlowersList.HeightRequest = xamarinHeight;
            FlowersList.WidthRequest = xamarinWidth;
            //puoktes_label.FontSize = xamarinHeight * 4.79 / 100;
            grid_layout.HorizontalItemSpacing = xamarinWidth * 1.666 / 100;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FlowerPost>();
                // Pateikiami visi duomenys
                if (query.Equals(""))
                {
                    conn.DropTable<FilterPost>();
                    var posts = conn.Table<FlowerPost>().ToList();
                    FlowersList.ItemsSource = posts;
                }
                //jei neatvaizduoti nieko
                else if (query.Equals("tuscia"))
                {
                    FlowersList.ItemsSource = null;
                    pranesimas.IsVisible = true;
                }
                //Pateikiami filtruoti duomenys kurie ateina pagal pasirinktus filtrus
                else
                {
                    var posts = conn.Query<FlowerPost>(query);
                    
                    if (posts.Count > 0)
                    {
                        FlowersList.ItemsSource = posts;
                    }
                    else
                    {
                        pranesimas.IsVisible = true;
                        FlowersList.ItemsSource = null;
                    }
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        [Obsolete]
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {

            var imageSender = sender as Grid;
            var asd = imageSender.RowDefinitions.ToArray();
            var xasd = asd[0].BindingContext as FlowerPost;

            int productId = xasd.Id;
            Navigation.PushAsync(new DetailedProduct(productId, 1));
        }
        protected override bool OnBackButtonPressed()
        {
            Functions tools = new Functions();
            tools.BackButtonWithExit("push");
            return true;
        }

        /* void add_to_favourites_Tapped(object sender, EventArgs e)
         {
             heart_img.IsVisible = true;
             Task.Delay(200);
             heart_img.IsVisible = false;
         }*/
    }
}
﻿using GeletaApp.Logic;
using GeletaApp.Model;
using SQLite;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherGoodsPage : ContentPage
    {
        public OtherGoodsPage(string query = "")
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
            HomeStuffList.HeightRequest = xamarinHeight;
            HomeStuffList.WidthRequest = xamarinWidth;
            //puoktes_label.FontSize = xamarinHeight * 4.79 / 100;
            grid_layout.HorizontalItemSpacing = xamarinWidth * 1.666 / 100;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Home_StuffPost>();
                // Pateikiami visi duomenys
                if (query.Equals(""))
                {
                    conn.DropTable<FilterPost>();
                    var posts = conn.Table<Home_StuffPost>().ToList();
                    HomeStuffList.ItemsSource = posts;
                }
                //jei neatvaizduoti nieko
                else if (query.Equals("tuscia"))
                {
                    HomeStuffList.ItemsSource = null;
                    pranesimas.IsVisible = true;
                }
                //Pateikiami filtruoti duomenys kurie ateina pagal pasirinktus filtrus
                else
                {
                    var posts = conn.Query<Home_StuffPost>(query);
                    HomeStuffList.ItemsSource = posts;
                    if (posts.Count > 0)
                    {
                        HomeStuffList.ItemsSource = posts;
                    }
                    else
                    {
                        pranesimas.IsVisible = true;
                        HomeStuffList.ItemsSource = null;
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
            var xasd = asd[0].BindingContext as Home_StuffPost;

            int productId = xasd.Id;
            Navigation.PushAsync(new DetailedProduct(productId, 3));
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
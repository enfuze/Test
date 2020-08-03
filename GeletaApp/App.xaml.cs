using GeletaApp.Logic;
using GeletaApp.Model;
using Plugin.Connectivity;
using SQLite;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GeletaApp
{
    public partial class App : Application
    {
        private string _conn;
        private string Conn
        {
            get => _conn;
            set {
                _conn = value;
                OnPropertyChanged();
            }
        }
        public static string User = "Rendy";
        public static string DatabaseLocation = string.Empty;
        public static int _clickCount { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            //CheckOnStart();
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            var profileLabelCS = new Style(typeof(Label))
            {
                Class = "profileLabelCS",
                Setters =
                {
                    new Setter
                    {
                        Property = Label.FontSizeProperty,
                        Value = xamarinHeight * 1.5 / 100
                    },
                    new Setter
                    {
                        Property = Label.TextColorProperty,
                        Value = Color.Black
                    },
                     new Setter
                    {
                        Property = Label.MarginProperty,
                        Value = new Thickness(20,0,0,0)
                    }
                }
            };


            
        }
        private void CheckContiniously()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                Conn = args.IsConnected ? "true" : "false";
                if (Conn == "true")
                {
                    Functions.SyncDatabase();
                }
            };
            

        }
        public App(string databaseLocation)
        {
            InitializeComponent();
            CheckContiniously();
            /* var seconds = TimeSpan.FromSeconds(1);
             Xamarin.Forms.Device.StartTimer(seconds,
                 () =>
                 {
                     CheckConnection();
                 });*/
            DatabaseLocation = databaseLocation;
            
            MainPage = new NavigationPage(new MainPage());

            
        }
       /* private async void CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {

            }
            //await Navigation.PushAsync(new YourPageWhenThereIsNoConnection());
            else
                return;
        }*/

        protected override void OnStart()
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                ShoppingCartPost cart = new ShoppingCartPost()
                {
                    Id = 1,
                    User_id = 1,
                    Final_price = 0,
                    Pickup_address = "",
                    Active_cart = true
                };
                conn.CreateTable<ShoppingCartPost>();
                int rows = conn.InsertOrReplace(cart);
                if (rows > 0)
                {
                    //success++;
                }
                else
                {
                    //fail++;
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

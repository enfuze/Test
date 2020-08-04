using GeletaApp.Logic;
using GeletaApp.Model;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedProduct
    {
        private static readonly CompositeDisposable EventSubscriptions = new CompositeDisposable();
        private readonly PanGestureRecognizer _panGesture = new PanGestureRecognizer();
        private double _transY;
        private static readonly CompositeDisposable EventSubscriptions2 = new CompositeDisposable();
        private readonly PanGestureRecognizer _panGesture2 = new PanGestureRecognizer();
        // private double _transY2;
        private static int i = 1;
        private double pirmo_aukstis;
        //private double pirmo_aukstisM;
        //private double pirmo_max_aukstis;
        private double ekranoDydis;
        private double x;
        public string ar_atvirute = "";
        public string address_product = "";
        public string adresas = "";
        public string adresas_type = "";
        public int cart_product_amount = 0;
        public int gele = 0;
        public int puokste = 0;
        public int kitas = 0;
        public bool ar_atvirute_bool = false;
        public int active_id = 0;
        public int amount = 0;
        int id;
        string paveikslas = "";
        string type = "";
        public double kaina_vieneto = 0.0;
        public string buy_comment = "";
        public List<CartItemPost> cart = new List<CartItemPost>();
        public DetailedProduct(int selectedProduct, int type)
        {

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            ekranoDydis = xamarinHeight;
            popUpStac.HeightRequest = xamarinHeight;
            popUpStac.WidthRequest = xamarinWidth;

            close_image.Margin = new Thickness(8, 13, 0, 0);
            shop_bag_image.Margin = new Thickness(0, 15, 8, 0);
            kiekisKrepselyje.Margin = new Thickness(0, 27, 20, 0);
            //image.Margin = new Thickness(0, -50, 0, 0);





            // virsutineJuosta.Padding = new Thickness(xamarinWidth * 4.629 / 100, xamarinHeight * 2.604 / 100, xamarinWidth * 4.629 / 100, 0);

            // imgStack.Padding = new Thickness(0, xamarinHeight * (-130 * 100 / height) / 100, 0, 0);

            share.Margin = new Thickness(xamarinWidth * 4.6296 / 100, 0, 0, 0);
            like.Margin = new Thickness(0, 0, xamarinWidth * 4.629 / 100, 0);
            QuickMenuPullLayout.HeightRequest = xamarinHeight;
            QuickMenuPullLayout.WidthRequest = xamarinWidth;
            KlausimuButton.HeightRequest = xamarinHeight * 5.260416 / 100;

            //  not1_stac.Margin = new Thickness(0, 0, 0, xamarinHeight * 2.604 / 100);
            //pirmo_aukstis = QuickMenuPullLayout.HeightRequest * 41.86 / 100;

            editorFrame.Margin = new Thickness(20, 0, -50, 0);
            KrepselioButton.HeightRequest = xamarinHeight * 5.26 / 100;
            KlausimuButton.HeightRequest = xamarinHeight * 5.26 / 100;

            // Krepšelio nuskaitymas ir jo kiekio atvaizdavimas ant krepšelio ikonos
            cart = DataManipulation_Logic.ReadCart();
            cart_product_amount = cart.Count();
            kiekisKrepselyje.Text = cart_product_amount.ToString();


            /* pirmo_aukstis = Notification.Height + KrepselioButton.HeightRequest + KlausimuButton.HeightRequest;
             x = (13.72 + pirmo_aukstis) / this.Height;
             QuickMenuPullLayout.TranslationY = this.Height * x;*/

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(100);
                Device.BeginInvokeOnMainThread(() =>
                {
                    //Puokstes nuskaitymas is sqlite ir padavimas duomenu i frontend
                    switch (type)
                    {
                        case 1: // Flowers
                            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                            {
                                conn.CreateTable<FlowerPost>();
                                var post = conn.Find<FlowerPost>(selectedProduct);
                                ChangeLikeImage(post.Favorite);
                                image.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(post.Image)));
                                id_product.Text = post.Id.ToString();
                                type_product.Text = "gele";
                                pavadinimas.Text = post.Name;
                                kaina.Text = post.Price.ToString() + " €";
                                aprasymas.Text = post.Description;
                                // krepselio_pavadinimas.Text = "GĖLĖ " + post.Name;
                            }
                            break;
                        case 2: // Bouquets
                            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                            {
                                conn.CreateTable<BouquetPost>();
                                var post = conn.Find<BouquetPost>(selectedProduct);
                                ChangeLikeImage(post.Favorite);
                                image.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(post.Image)));
                                id_product.Text = post.Id.ToString();
                                type_product.Text = "puokste";
                                pavadinimas.Text = post.Name;
                                kaina.Text = post.Price.ToString() + " €";
                                aprasymas.Text = post.Description;
                                //  krepselio_pavadinimas.Text = "PUOKŠTĖ " + post.Name;
                            }
                            break;
                        case 3: // Home_stuff
                            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                            {
                                conn.CreateTable<Home_StuffPost>();
                                var post = conn.Find<Home_StuffPost>(selectedProduct);
                                ChangeLikeImage(post.Favorite);
                                image.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(post.Image)));
                                id_product.Text = post.Id.ToString();
                                type_product.Text = "kitas";
                                pavadinimas.Text = post.Name;
                                kaina.Text = post.Price.ToString() + " €";
                                aprasymas.Text = post.Description;
                                //  krepselio_pavadinimas.Text = "AKSESUARAS " + post.Name;
                            }
                            break;
                    }
                    CollapseAllMenus();
                });
                //await Task.Delay(200);
            });


            editor.Focused += editor_pastabu_Tapped;
            editor.Unfocused += editor_Unfocused;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeObservables();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            EventSubscriptions.Clear();
            kiekioGrid.FindByName<Label>("kiekisLabel").Text = "1";
            i = 1;
        }
        private void CollapseAllMenus()
        {
            pirmo_aukstis = Notification.Height + KrepselioButton.HeightRequest + KlausimuButton.HeightRequest;
            x = (13.72 + pirmo_aukstis) / this.Height;
            QuickMenuPullLayout.TranslationY = this.Height * x;
        }

        private void InitializeObservables()
        {
            //IF THERE IS OBSERVABLES
            var panGestureObservable = Observable
                .FromEventPattern<PanUpdatedEventArgs>(
                    x => _panGesture.PanUpdated += x,
                    x => _panGesture.PanUpdated -= x
                )
                //.Throttle(TimeSpan.FromMilliseconds(20), TaskPoolScheduler.Default)
                .Subscribe(x => Device.BeginInvokeOnMainThread(() => { CheckQuickMenuPullOutGesture(x); }));

            EventSubscriptions.Add(panGestureObservable);
            QuickMenuInnerLayout.GestureRecognizers.Add(_panGesture);

        }

        private void CheckQuickMenuPullOutGesture(EventPattern<PanUpdatedEventArgs> x)
        {
            var e = x.EventArgs;
            var typeOfAction = x.Sender as StackLayout;

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    MethodLockedSync(() =>
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            QuickMenuPullLayout.TranslationY = Math.Max(120,
                                Math.Min(pirmo_aukstis + 30, QuickMenuPullLayout.TranslationY + e.TotalY));

                        });
                    }, 2);

                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    _transY = QuickMenuPullLayout.TranslationY;
                    break;
                case GestureStatus.Canceled:
                    Debug.WriteLine("Canceled");
                    break;
            }
        }

        private CancellationTokenSource _throttleCts = new CancellationTokenSource();
        private void MethodLockedSync(Action method, double timeDelay = 300)
        {
            Interlocked.Exchange(ref _throttleCts, new CancellationTokenSource()).Cancel();
            Task.Delay(TimeSpan.FromMilliseconds(timeDelay), _throttleCts.Token) // throttle time
                .ContinueWith(
                    delegate { method(); },
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void prideti_atvirute_checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox atvirute = sender as CheckBox;
            ar_atvirute = atvirute.ClassId.ToString();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CollapseAllMenus();
            //   QuickMenuPullLayout.TranslationY = pirmo_aukstis;
            QuickMenuInnerLayout.IsEnabled = true;
        }

        private async void KrepselioButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                // get active shopping cart
                var active_id_obj = conn.Query<ShoppingCartPost>("SELECT Id FROM ShoppingCartPost WHERE Active_cart LIKE true");
                active_id = active_id_obj[0].Id;
                conn.Close();
            }
            kaina_vieneto = Functions.GetPriceNumber(kaina.Text);
            id = Convert.ToInt32(id_product.Text);
            type = type_product.Text;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                switch (type)
                {
                    case "gele":
                        gele = id;
                        var uzklausa = conn.Find<FlowerPost>(id);
                        paveikslas = uzklausa.Image;
                        break;
                    case "puokste":
                        puokste = id;
                        var uzklausa1 = conn.Find<BouquetPost>(id);
                        paveikslas = uzklausa1.Image;
                        break;
                    case "kitas":
                        kitas = id;
                        var uzklausa2 = conn.Find<Home_StuffPost>(id);
                        paveikslas = uzklausa2.Image;
                        break;
                }
            }

            amount = Convert.ToInt32(kiekioGrid.FindByName<Label>("kiekisLabel").Text);
            if (ar_atvirute != "")
            {
                ar_atvirute_bool = true;
            }
            buy_comment = editor.Text;

            var popupPage = new PasirinktiPristatymaPage();
            // event callback
            popupPage.CallbackEvent += (object sendertest, object etest) => CallbackMethod(sendertest); // the method where you do whatever you want to after the popup is closed
            PopupNavigation.Instance.PushAsync(popupPage);
        }
        public void CallbackMethod(object obj)
        {
            bool address_empty = false;
            switch (obj)
            {
                case "maxima":
                    adresas = maxima.Text;
                    adresas_type = "maxima";
                    break;
                case "maxima_pramones":
                    adresas = maxima_pramones.Text;
                    adresas_type = "maxima_pramones";
                    break;
                case "namai":
                    adresas = namai.Text;
                    adresas_type = "namai";
                    break;
                default:
                    address_empty = true;
                    break;
            }
            if (!address_empty)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    switch (type)
                    {
                        case "gele":
                            CartItemPost g_object = conn.FindWithQuery<CartItemPost>("SELECT * FROM CartItemPost WHERE Flower_id LIKE ? AND Pickup_address_type LIKE ?", gele, adresas_type);
                            if (g_object != null && g_object.Pickup_address == adresas)
                            {
                                CartItemPost gelite = new CartItemPost()
                                {
                                    Id = g_object.Id,
                                    Bouquet_id = puokste,
                                    Flower_id = gele,
                                    Home_stuff_id = kitas,
                                    Amount = amount + g_object.Amount,
                                    Postcard_required = ar_atvirute_bool,
                                    Buy_comment = buy_comment,
                                    Pickup_address = adresas,
                                    Pickup_address_type = adresas_type,
                                    ShoppingCart_id = active_id,
                                    Price = kaina_vieneto * amount + g_object.Price,
                                    Product_name = pavadinimas.Text,
                                    Image = paveikslas
                                };
                                conn.CreateTable<CartItemPost>();
                                conn.InsertOrReplace(gelite);
                                conn.Close();
                            }
                            else
                            {
                                CartItemPost product = new CartItemPost()
                                {
                                    Bouquet_id = puokste,
                                    Flower_id = gele,
                                    Home_stuff_id = kitas,
                                    Amount = amount,
                                    Postcard_required = ar_atvirute_bool,
                                    Buy_comment = buy_comment,
                                    Pickup_address = adresas,
                                    Pickup_address_type = adresas_type,
                                    ShoppingCart_id = active_id,
                                    Price = kaina_vieneto * amount,
                                    Product_name = pavadinimas.Text,
                                    Image = paveikslas
                                };
                                conn.CreateTable<CartItemPost>();
                                conn.Insert(product);
                                conn.Close();
                            }
                            break;
                        case "puokste":
                            CartItemPost b_object = conn.FindWithQuery<CartItemPost>("SELECT * FROM CartItemPost WHERE Bouquet_id LIKE ? AND Pickup_address_type LIKE ?", puokste, adresas_type);
                            if (b_object != null && b_object.Pickup_address == adresas)
                            {
                                CartItemPost puokstite = new CartItemPost()
                                {
                                    Id = b_object.Id,
                                    Bouquet_id = puokste,
                                    Flower_id = gele,
                                    Home_stuff_id = kitas,
                                    Amount = amount + b_object.Amount,
                                    Postcard_required = ar_atvirute_bool,
                                    Buy_comment = buy_comment,
                                    Pickup_address = adresas,
                                    Pickup_address_type = adresas_type,
                                    ShoppingCart_id = active_id,
                                    Price = kaina_vieneto * amount + b_object.Price,
                                    Product_name = pavadinimas.Text,
                                    Image = paveikslas
                                };
                                conn.CreateTable<CartItemPost>();
                                conn.InsertOrReplace(puokstite);
                                conn.Close();
                            }
                            else
                            {
                                CartItemPost product = new CartItemPost()
                                {
                                    Bouquet_id = puokste,
                                    Flower_id = gele,
                                    Home_stuff_id = kitas,
                                    Amount = amount,
                                    Postcard_required = ar_atvirute_bool,
                                    Buy_comment = buy_comment,
                                    Pickup_address = adresas,
                                    Pickup_address_type = adresas_type,
                                    ShoppingCart_id = active_id,
                                    Price = kaina_vieneto * amount,
                                    Product_name = pavadinimas.Text,
                                    Image = paveikslas
                                };
                                conn.CreateTable<CartItemPost>();
                                conn.Insert(product);
                                conn.Close();
                            }
                            break;
                        case "kitas":
                            CartItemPost o_object = conn.FindWithQuery<CartItemPost>("SELECT * FROM CartItemPost WHERE Home_stuff_id LIKE ? AND Pickup_address_type LIKE ?", kitas, adresas_type);
                            if (o_object != null && o_object.Pickup_address == adresas)
                            {
                                CartItemPost kitketas = new CartItemPost()
                                {
                                    Id = o_object.Id,
                                    Bouquet_id = puokste,
                                    Flower_id = gele,
                                    Home_stuff_id = kitas,
                                    Amount = amount + o_object.Amount,
                                    Postcard_required = ar_atvirute_bool,
                                    Buy_comment = buy_comment,
                                    Pickup_address = adresas,
                                    Pickup_address_type = adresas_type,
                                    ShoppingCart_id = active_id,
                                    Price = kaina_vieneto * amount + o_object.Price,
                                    Product_name = pavadinimas.Text,
                                    Image = paveikslas
                                };
                                conn.CreateTable<CartItemPost>();
                                conn.InsertOrReplace(kitketas);
                                conn.Close();
                            }
                            else
                            {
                                CartItemPost product = new CartItemPost()
                                {
                                    Bouquet_id = puokste,
                                    Flower_id = gele,
                                    Home_stuff_id = kitas,
                                    Amount = amount,
                                    Postcard_required = ar_atvirute_bool,
                                    Buy_comment = buy_comment,
                                    Pickup_address = adresas,
                                    Pickup_address_type = adresas_type,
                                    ShoppingCart_id = active_id,
                                    Price = kaina_vieneto * amount,
                                    Product_name = pavadinimas.Text,
                                    Image = paveikslas
                                };
                                conn.CreateTable<CartItemPost>();
                                conn.Insert(product);
                                conn.Close();
                            }
                            break;
                    }
                }
            }
            cart = DataManipulation_Logic.ReadCart();
            cart_product_amount = cart.Count();
            kiekisKrepselyje.Text = cart_product_amount.ToString();
        }
        private void minusButton_Clicked(object sender, EventArgs e)
        {
            i--;
            if (i < 1)
                i = 1;
            kiekioGrid.FindByName<Label>("kiekisLabel").Text = i.ToString();
            double kaina_vieneto = Functions.GetPriceNumber(kaina.Text);
            var final_kaina = kaina_vieneto * i;
        }

        private void pliusButton_Clicked(object sender, EventArgs e)
        {
            i++;
            kiekioGrid.FindByName<Label>("kiekisLabel").Text = i.ToString();
            double kaina_vieneto = Functions.GetPriceNumber(kaina.Text);
            var final_kaina = kaina_vieneto * i;
        }
        private void close_image_Clicked(object sender, EventArgs e)
        {
            switch (type_product.Text)
            {
                case "gele":
                    Navigation.PushAsync(new FlowersPage());
                    break;
                case "puokste":
                    Navigation.PushAsync(new BouquetsPage());
                    break;
                case "kitas":
                    Navigation.PushAsync(new OtherGoodsPage());
                    break;
            }
        }
        private async void shop_bag_image_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BuyPage());
        }
        private void editor_pastabu_Tapped(object sender, EventArgs e)
        {
            KrepselioButton.IsVisible = false;
            klausimuFrame.IsVisible = false;
            //   App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            QuickMenuPullLayout.TranslationY = -ekranoDydis / 4;
            //   editorFrame.TranslateTo(0, editorFrame.TranslationY - 150);
            //editorFrame.Margin = new Thickness(20, 0, -50, ekranoDydis / 3); //push the entry up to keyboard height when keyboard is activated

        }
        private void editor_Unfocused(object sender, FocusEventArgs e)
        {
            KrepselioButton.IsVisible = true;
            klausimuFrame.IsVisible = true;
            QuickMenuPullLayout.TranslationY = 120;
            //editor?.Unfocus();
            // editorFrame.TranslateTo(0, editorFrame.TranslationY + 150);
            // editorFrame.Margin = new Thickness(20, 0, -50, 0);
        }

        private void like_Clicked(object sender, EventArgs e)
        {
            bool check = true;
            //string test = like.Source.ToString();
            if (like.Source.ToString() == "File: heartemptynew36px.png")
            {
                check = false;
            }
            var id = Convert.ToInt32(id_product.Text);
            var type = type_product.Text;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                switch (type)
                {
                    case "gele":
                        string query1 = string.Format("UPDATE FlowerPost SET Favorite = NOT Favorite WHERE Id = '{0}'", id);
                        conn.Query<FlowerPost>(query1);
                        break;
                    case "puokste":
                        string query2 = string.Format("UPDATE BouquetPost SET Favorite = NOT Favorite WHERE Id = '{0}'", id);
                        conn.Query<BouquetPost>(query2);
                        break;
                    case "kitas":
                        string query3 = string.Format("UPDATE Home_StuffPost SET Favorite = NOT Favorite WHERE Id = '{0}'", id);
                        conn.Query<Home_StuffPost>(query3);
                        break;
                }
                ChangeLikeImage(check);
                conn.Close();
            }
            List<Page> navlist = Navigation.NavigationStack.ToList();
            Page last = navlist.ElementAt(navlist.Count - 2);
            string page = last.ToString();
            if (page == "GeletaApp.FavouritePage")
            {
                App.Current.MainPage.Navigation.PushAsync(new FavouritePage());
            }
        }
        private async void share_Clicked(object sender, EventArgs e)
        {
            await share.ScaleTo(0.75, 100);
            await share.ScaleTo(1, 100);
            await App.Current.MainPage.DisplayAlert("Alert", "Implementuoti pasidalinimą", "OK");
        }
        async void ChangeLikeImage(bool check)
        {
            if (check)
            {
                await like.ScaleTo(0.75, 100);
                await like.ScaleTo(1, 100);
                like.Source = ImageSource.FromFile("heartemptynew36px.png");
            }
            else
            {
                await like.ScaleTo(0.75, 100);
                await like.ScaleTo(1, 100);
                like.Source = ImageSource.FromFile("favoritekiauras36px.png");
            }
        }
        private async void KlausimuButton_Clicked(object sender, EventArgs e)
        {
            //await this.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new SusisiekimePage());
        }

        protected override bool OnBackButtonPressed()
        {
            List<Page> li = Navigation.NavigationStack.ToList();
            if (li.Count != 0)
            {
                Page last = li.ElementAt(li.Count - 2);
                string page = last.ToString();
                Functions tools = new Functions();
                tools.BackButtonWithExit("detail", page);
            }
            return true;
        }
    }
}

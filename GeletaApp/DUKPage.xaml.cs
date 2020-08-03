using System;
using Expandable;
using GeletaApp.Logic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeletaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DUKPage : ContentPage
    {
        public DUKPage()
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
            duk_cp.HeightRequest = xamarinHeight;
            duk_cp.WidthRequest = xamarinWidth;
            BackImgButton4.WidthRequest = xamarinWidth * 6.8518 / 100;
            duk_st.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 2.03 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885 / 100);
            // virsutineJuosta.HeightRequest = xamarinHeight * 10.416 / 100;
            aprasymas.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 4.166 / 100, 0, xamarinHeight * 4.6875 / 100);
            stack1.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 0.5208 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.04166/ 100);
            stack2.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 0.5208 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.04166 / 100);
            stack3.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 0.5208 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.04166 / 100);
            stack4.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 0.5208 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.04166 / 100);
            stack5.Padding = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 0.5208 / 100, xamarinWidth * 3.24 / 100, xamarinHeight * 1.04166 / 100);
            

            //aprasymas.Margin = new Thickness(xamarinWidth * 3.24 / 100, xamarinHeight * 4.166 / 100, 0, xamarinHeight * 4.6875 / 100);
            /*stack1.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            stack2.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            stack3.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            stack4.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);
            stack5.Margin = new Thickness(xamarinWidth * -3.24 / 100, 0, xamarinWidth * -3.24 / 100, 0);*/



            //customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885 / 100);



            //  virsutineJuosta.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, 0, 0);
              duk.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
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
        private async void eiti_i_login_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            expandableView.StatusChanged += OnStatusChanged;
            expandableView2.StatusChanged += OnStatusChanged;
            expandableView3.StatusChanged += OnStatusChanged;
            expandableView4.StatusChanged += OnStatusChanged;
            expandableView5.StatusChanged += OnStatusChanged;
           
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            expandableView.StatusChanged -= OnStatusChanged;
            expandableView2.StatusChanged -= OnStatusChanged;
            expandableView3.StatusChanged -= OnStatusChanged;
            expandableView4.StatusChanged -= OnStatusChanged;
            expandableView5.StatusChanged -= OnStatusChanged;
           
        }

        private async void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            if (expandableView.IsExpanded)
            {
                await arrow.RotateTo(180, 200, Easing.CubicInOut);
            }
            if (expandableView2.IsExpanded)
            {
                await arrow2.RotateTo(180, 200, Easing.CubicInOut);

            }
            if (!expandableView.IsExpanded)
            {
                await arrow.RotateTo(0, 200, Easing.CubicInOut);
            }
            if (!expandableView2.IsExpanded)
            {
                await arrow2.RotateTo(0, 200, Easing.CubicInOut);
            }

            if (expandableView3.IsExpanded)
            {
                await arrow3.RotateTo(180, 200, Easing.CubicInOut);
            }
            if (expandableView4.IsExpanded)
            {
                await arrow4.RotateTo(180, 200, Easing.CubicInOut);

            }
            if (!expandableView3.IsExpanded)
            {
                await arrow3.RotateTo(0, 200, Easing.CubicInOut);
            }
            if (!expandableView4.IsExpanded)
            {
                await arrow4.RotateTo(0, 200, Easing.CubicInOut);
            }

            if (expandableView5.IsExpanded)
            {
                await arrow5.RotateTo(180, 200, Easing.CubicInOut);

            }
            if (!expandableView5.IsExpanded)
            {
                await arrow5.RotateTo(0, 200, Easing.CubicInOut);
            }
           
        }

        private async void eiti_i_adresai_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AdressPage());

        }

        private async void eiti_i_apmokejimai_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new BuyPage());

        }

        private async void eiti_i_užsakymus_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new OrdersPage());

        }
    }
}
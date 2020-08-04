using GeletaApp.Logic;
using GeletaApp.ViewModels;
using GeletaApp.Views.Cells;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeletaApp.Views
{
    public partial class ChatPage : ContentPage
    {
        public bool RodytiLaika { get; set; } = false;
        IncomingViewCell IncomingViewCell = new IncomingViewCell();
        public ChatPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            this.BindingContext = new ChatPageViewModel();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
            chatcp.HeightRequest = xamarinHeight;
            chatcp.WidthRequest = xamarinWidth;
            virsutineJuosta.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, 0, 0);
            virsutineJuosta.HeightRequest = xamarinHeight * 10.416 / 100;
            BackImgButton1.WidthRequest = xamarinWidth * 6.85 / 100;
            susirasinejimas.Padding = new Thickness(xamarinWidth * 8.888 / 100, 0, 0, 0);
            customMenu.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, xamarinHeight * 0.885 / 100);
            /* Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                  Device.BeginInvokeOnMainThread(() => laikas.Text = DateTime.Now.ToString(@"HH\:mm"));
                  return true;
              });*/
            gridas.WidthRequest = xamarinWidth;
            gridas.Padding = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 3.24 / 100, 0);
            gridas.HeightRequest = xamarinHeight * 65 / 100;
            //   zinute.FontSize = xamarinHeight * 2.6 / 100;
            //chatFrame.Padding = new Thickness(xamarinWidth * (35 * 100 / width) / 100, 0, xamarinWidth * (35 * 100 / width) / 100, 0);
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
        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = BindingContext as ChatPageViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (vm.DelayedMessages.Count > 0)
                        {
                            vm.Messages.Insert(0, vm.DelayedMessages.Dequeue());
                        }
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;
                        ChatList?.ScrollToFirst();
                    });


                }

            }
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }
    }
}

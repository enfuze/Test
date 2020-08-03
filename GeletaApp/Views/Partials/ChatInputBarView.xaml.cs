using System;
using System.Collections.Generic;
using GeletaApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeletaApp.Views.Partials
{
    public partial class ChatInputBarView : ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Width (in xamarin.forms units)
            var xamarinWidth = width / mainDisplayInfo.Density;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            var xamarinHeight = height / mainDisplayInfo.Density;
         //   chatTextInput.HeightRequest = xamarinHeight * 1.51 / 100;
            prideti.HeightRequest = xamarinHeight * 3 / 100;
            siusti.Padding = new Thickness(0, 0, xamarinWidth * 3.24 / 100, 0);
            //stack.HeightRequest = xamarinHeight * 4.79 / 100;
            siusti.FontSize = xamarinHeight * 2 / 100;
            prideti.Margin = new Thickness(xamarinWidth * 3.24 / 100, 0, xamarinWidth * 12.129/ 100, 0);
        }
        public void Handle_Completed(object sender, EventArgs e)
        {
            (this.Parent.Parent.BindingContext as ChatPageViewModel).OnSendCommand.Execute(null);
            chatTextInput.Focus();
            chatTextInput.Text = null;
        }

        public void UnFocusEntry(){
            chatTextInput?.Unfocus();
        }

        private async void pridetiGest_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("Alert", "reiks turbūt leist pridėti img", "OK");
        }
    }
}

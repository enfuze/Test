using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GeletaApp.Views.Cells
{
    public partial class OutgoingViewCell : ViewCell
    {
        public OutgoingViewCell()
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
            outg.FontSize = xamarinHeight * 2.6 / 100;
            gridas.Padding = new Thickness(xamarinWidth * 16.48 / 100, 0, xamarinWidth * 3.24 / 100, 0);
          //  laikas.Margin = new Thickness(0, 0, gridas.WidthRequest / 2, 0);
        }
        private void zinuteTap_Tapped(object sender, EventArgs e)
        {
            if (laikas.IsVisible == true)
                laikas.IsVisible = false;
            else
                laikas.IsVisible = true;
        }
    }
}

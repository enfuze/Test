using System;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Rg.Plugins.Popup.Services;
using Android.Content;
using GeletaApp;
using Android.Views;
using Android.Runtime;
using Plugin.Connectivity;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using Rg.Plugins.Popup.Pages;

namespace GeletaApp.Droid
{
    [Activity(Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("RadioButton_Experimental");
            global::Xamarin.Forms.Forms.SetFlags("ImageButton_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            string dbName = "geleta_db_sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = System.IO.Path.Combine(folderPath, dbName);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App(fullPath));

        }

        //--- double click to exit + toast implementacija ---
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                //List <PopupPage> test = PopupNavigation.PopupStack.ToList();
                //PopupPage last = test.ElementAt(test.Count - 1);
                //string last_pop = last.ToString();
                PopupNavigation.Instance.PopAsync();
            }
        }

        //long lastPress;
        //bool nerodyti;
        //public override void OnBackPressed()
        //{
        //    long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        //    if (currentTime - lastPress > 500 && lastPress != 0 && nerodyti == false)
        //    {
        //        //base.OnBackPressed();
        //        Toast.MakeText(this, "Spustelk dukart, kad išeiti", ToastLength.Short).Show();
        //        lastPress = currentTime;
        //        nerodyti = true;
        //    }
        //    else if (lastPress == 0 || nerodyti == true)
        //    {
        //        //base.OnBackPressed();
        //        lastPress = currentTime;
        //        nerodyti = false;
        //    }
        //    else Finish();
        //}
    }
}
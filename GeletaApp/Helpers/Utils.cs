using Android.Content;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeletaApp.Helpers
{
    public class Utils
    {
        public static void showToast(Context mContext, String message)
        {
            Toast.MakeText(mContext, message, ToastLength.Short).Show();

        }
    }
}

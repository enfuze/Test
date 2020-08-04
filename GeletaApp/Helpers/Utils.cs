using Android.Content;
using Android.Widget;
using System;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GeletaApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(BackgroundDependency_Android))]
namespace GeletaApp.Droid
{
    public class BackgroundDependency_Android : Java.Lang.Object, IBackgroundDependency
    {
        public void ExecuteCommand(string a, string b, string c, Action d)
        {
            DisplayAlertService.ShowAlert(a,b,c,d);
        }
    }
}
using GeletaApp.Droid;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(BackgroundDependency_Android))]
namespace GeletaApp.Droid
{
    public class BackgroundDependency_Android : Java.Lang.Object, IBackgroundDependency
    {
        public void ExecuteCommand(string a, string b, string c, Action d)
        {
            DisplayAlertService.ShowAlert(a, b, c, d);
        }
    }
}
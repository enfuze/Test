using Android.App;
using Android.Widget;
using GeletaApp.Droid;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(DisplayAlertService))]
namespace GeletaApp.Droid
{
    public static class DisplayAlertService
    {
        public static async Task<bool> ShowAlert(string title, string content, string okButton, Action callback)
        {
            return await Task.Run(() => Alert(title, content, okButton, callback));
        }

        public static async Task<bool> ShowAlertConfirm(string title, string content, string confirmButton, string cancelButton, Action<bool> callback)
        {
            return await Task.Run(() => AlertConfirm(title, content, confirmButton, cancelButton, callback));
        }

        private static bool Alert(string title, string content, string okButton, Action callback)
        {
            var alert = new AlertDialog.Builder(Forms.Context);
            alert.SetTitle(title);
            alert.SetMessage(content);
            if (!Equals(callback, null))
                alert.SetNegativeButton(okButton, (sender, e) => { callback(); });
            else
                alert.SetNegativeButton(okButton, (sender, e) => { });

            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = alert.Show();
                BrandAlertDialog(dialog);
            });

            return true;
        }

        private static bool AlertConfirm(string title, string content, string confirmButton, string cancelButton,
            Action<bool> callback)
        {
            var alert = new AlertDialog.Builder(Forms.Context);

            /*Typeface typeface = Typeface.CreateFromAsset(
                    getAssets(),
                    "Assets/segoeuil.ttf");*/

            alert.SetTitle(title);
            alert.SetMessage(content);
            alert.SetPositiveButton(confirmButton, (sender, e) => { callback(true); });
            alert.SetNegativeButton(cancelButton, (sender, e) => { callback(false); });

            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = alert.Show();
                BrandAlertDialog(dialog);
            });
            //Typeface face = Typeface.CreateFromAsset(getAssets(), "fonts/FONT");
            return true;
        }

        private static void BrandAlertDialog(Dialog dialog)
        {
            try
            {

                var resources = dialog.Context.Resources;
                // var color = dialog.Context.Resources.GetColor(Resource.Color.dialog_textcolor);
                // var background = dialog.Context.Resources.GetColor(Resource.Color.dialog_background);

                var alertTitleId = resources.GetIdentifier("alertTitle", "id", "android");
                var alertTitle = (TextView)dialog.Window.DecorView.FindViewById(alertTitleId);
                //alertTitle.SetTypeface(FontHelper.getFont(Fonts.MULI_REGULAR));
                //alertTitle.SetTextColor(color); // change title text color
                //alertTitle.Font
                var titleDividerId = resources.GetIdentifier("titleDivider", "id", "android");
                var titleDivider = dialog.Window.DecorView.FindViewById(titleDividerId);

                //titleDivider.SetBackgroundColor(background); // change divider color
            }
            catch
            {
                //Can't change dialog brand color
            }
        }
    }
}
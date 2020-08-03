using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using GeletaApp.Droid;
using GeletaApp.Logic;
using Xamarin.Essentials;

namespace Splash_Screen
{
    [Activity(  Theme = "@style/MyTheme.Splash", 
                Label = "Gėlėta", Icon = "@drawable/logo", 
                MainLauncher = true, 
                NoHistory = true, 
                ScreenOrientation = ScreenOrientation.Portrait, 
                ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var current = Connectivity.NetworkAccess;
            //var profiles = Connectivity.ConnectionProfiles;
            if (current == NetworkAccess.Internet)
            {
                // lblNetworkStatus.Text = "Network is Available";
                Functions.SyncDatabase();
            }
            else
            {
                //blNetworkStatus.Text = "Network is Not Available";
            }
            // Puoksciu nuskaitymas is api ir DB atnaujinimas
            
            // --- here can implement to do whatever you want before invoking the MainActivity ---
            InvokeMainActivity();
            Finish();

        }
        private void InvokeMainActivity()
        {
            // ---- Nuima flash stutter animation nuo loading/splash screen ir permeta tvarkingai į MainActivity
            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            mainActivityIntent.AddFlags(ActivityFlags.NoAnimation);
            StartActivity(mainActivityIntent);
            // ---- 
        }
    }
}
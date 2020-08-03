﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GeletaApp;
using GeletaApp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(MyEntry), typeof(CustomEntryRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace GeletaApp.Droid
{
    [Obsolete]
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            this.Control.Background = Forms.Context.GetDrawable(Resource.Drawable.EntryBackground);
            Control?.SetPadding(20, 0, 20, 2);
        }
    }
}
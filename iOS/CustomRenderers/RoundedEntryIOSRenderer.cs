using System;
using AlzaMobile.Core.Controls;
using AlzaMobile.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryIOSRenderer))]
namespace AlzaMobile.iOS.CustomRenderers
{
    public class RoundedEntryIOSRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.RoundedRect;
                Control.BackgroundColor = UIColor.White;
                Control.Layer.BorderColor = UIColor.Black.CGColor;
                Control.Layer.BorderWidth = 1;
                Control.Layer.CornerRadius = 10;
            }
        }
    }
}

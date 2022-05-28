﻿using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ChatApp;
using ChatApp.Droid;
using Android.Util;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace ChatApp.Droid
{
    [Obsolete]
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var view = (CustomEntry)Element;

                e.NewElement.Unfocused += (sender, evt) =>
                {
                    view.BorderColor.ToAndroid();
                };
                e.NewElement.Focused += (sender, evt) =>
                {
                    view.BorderColor = Xamarin.Forms.Color.Red;
                };

                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background
                    var _gradientBackground = new GradientDrawable();
                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line
                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                    // Radius for the curves
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                    // set the background of the label
                    Control.SetBackground(_gradientBackground);
                }

                // Set padding for the internal text from border
                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                    Control.PaddingTop,
                    (int)DpToPixels(this.Context, Convert.ToSingle(12)),
                    Control.PaddingBottom);
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
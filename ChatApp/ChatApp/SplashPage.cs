using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChatApp
{
    public class SplashPage : ContentPage
    {
        Image splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this,false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "logo.png",
                WidthRequest = 300,
                HeightRequest = 300
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.White;
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await splashImage.ScaleTo(1, 2000);
            bool isLoggedIn = Application.Current.Properties.ContainsKey("IsLoggedIn") ? Convert.ToBoolean(Application.Current.Properties["IsLoggedIn"]) : false;
            if (isLoggedIn == false)
            {
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new TabbedPage1());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace ChatApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class MainPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void Pass_Clicked(Object sender, EventArgs e)
        {
            if(string.Compare(pass.Text,"Hide") == 0)
            {
                pass.Text = "Show";
                logpass.IsPassword = true;
            }
            else
            {
                pass.Text = "Hide";
                logpass.IsPassword = false;
            }
        }

        private void Handle_CLicked(Object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Create());
        }

        private async void Signin_CLicked(Object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(logemail.Text) && !string.IsNullOrEmpty(logpass.Text))
            {
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(logemail.Text, logpass.Text);

                if (res.Status == true)
                {
                    Application.Current.Properties["email"] = dataClass.loggedInUser.email.ToString();
                    Application.Current.Properties["name"] = dataClass.loggedInUser.name.ToString();
                    Application.Current.Properties["IsLoggedIn"] = DependencyService.Get<iFirebaseAuth>().IsLoggedIn().Status.ToString();
                    await Application.Current.SavePropertiesAsync();
                    await DisplayAlert("Successful", res.Response, "Okay");
                    Application.Current.MainPage = new NavigationPage(new TabbedPage1());
                }
                else
                {
                    bool retryBool = await DisplayAlert("Error", res.Response + " Retry?", "Yes", "No");
                    if (retryBool)
                    {
                        logemail.Text = string.Empty;
                        logpass.Text = string.Empty;
                        logemail.Focus();
                    }
                }
            }
            else
            {
                await DisplayAlert("Missing Fields", "There are missing fields.", "Okay");
                logemail.Focus();
                logpass.Focus();
                if (logpass.IsFocused)
                {
                    logpass.BorderColor = Color.Red;
                }
            }

        }

        private async void Signin2_CLicked(Object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("fb"))
            {
                await DisplayAlert("Successful", "Sign in success.", "Okay");
                var tab = new TabbedPage1();
                Application.Current.MainPage = new NavigationPage(tab);
                NavigationPage.SetHasNavigationBar(tab, false); 
            }
            else
            {
                await DisplayAlert("Invalid User", "Facebook user is not signed up.", "Okay");
            }

        }

        private async void Google_CLicked(Object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("google"))
            {
                await DisplayAlert("Successful", "Sign in success.", "Okay");
                var tab = new TabbedPage1();
                Application.Current.MainPage = new NavigationPage(tab);
                NavigationPage.SetHasNavigationBar(tab, false);
            }
            else
            {
                await DisplayAlert("Invalid User", "Google user is not signed up.", "Okay");
            }

        }

        private void Forgot_Clicked(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResetPassword());
        }



    }
}
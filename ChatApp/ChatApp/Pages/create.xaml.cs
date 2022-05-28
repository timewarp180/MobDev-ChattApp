using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
        public Create()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void Main_CLicked(Object sender, System.EventArgs e)
        {
            var tab = new MainPage();
            Application.Current.MainPage = new NavigationPage(tab);
        }

        private void Pass_Clicked(Object sender, EventArgs e)
        {
            if (string.Compare(button.Text, "Hide") == 0)
            {
                button.Text = "Show";
                button2.Text = "Show";
                conpass.IsPassword = true;
                pass.IsPassword = true;
            }
            else
            {
                button.Text = "Hide";
                button2.Text = "Hide";
                conpass.IsPassword = false;
                pass.IsPassword = false;
            }
        }
        private async void Signup_Clicked(Object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(pass.Text) && !string.IsNullOrEmpty(conpass.Text))
            {
                if (string.Compare(pass.Text, conpass.Text) == 0)
                {
                    FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                    res = await DependencyService.Get<iFirebaseAuth>().SignUpWithEmailPassword(username.Text, email.Text, pass.Text);

                    if (res.Status == true)
                    {
                        try
                        {
                            await CrossCloudFirestore.Current
                             .Instance
                             .GetCollection("users")
                             .GetDocument(dataClass.loggedInUser.uid)
                             .SetDataAsync(dataClass.loggedInUser);

                            await DisplayAlert("Success", res.Response, "Okay");
                            Application.Current.MainPage = new NavigationPage(new MainPage());

                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error", ex.Message, "Okay");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", res.Response, "Okay");
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Password", "Passwords do not match.", "Okay");
                }

            }
            else
            {
                await DisplayAlert("Missing Fields", "There are missing fields.", "Okay");
            }
        }

        private async void Signup2_Clicked(Object sender, EventArgs e)
        {
            Application.Current.Properties["fb"] = "True";
            await Application.Current.SavePropertiesAsync();
            await DisplayAlert("Success", "Sign up successful. Verfication email sent.", "Okay");
            await Navigation.PopAsync();
        }

        private async void Google_Clicked(Object sender, EventArgs e)
        {
            Application.Current.Properties["google"] = "True";
            await Application.Current.SavePropertiesAsync();
            await DisplayAlert("Success", "Sign up successful. Verfication email sent.", "Okay");
            await Navigation.PopAsync();
        }
    }
}
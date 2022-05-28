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
    public partial class ResetPassword : ContentPage
    {
        public ResetPassword()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Homepage");
        }

        private async void ResetPassword_Clicked(Object sender, EventArgs e)
        {
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<iFirebaseAuth>().ResetPassword(forgotemail.Text);

            if (res.Status == true)
            {
                await DisplayAlert("Success", res.Response, "Okay");
                await Navigation.PopModalAsync();
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", res.Response, "Okay");
            }

        }
    }
}
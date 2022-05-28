using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace ChatApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            if (Application.Current.Properties.ContainsKey("email") && Application.Current.Properties.ContainsKey("name"))
            {
                email.Text = Application.Current.Properties["email"].ToString();
                name.Text = Application.Current.Properties["name"].ToString();
            }
        }

        private async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = DependencyService.Get<iFirebaseAuth>().SignOut();

            if (res.Status == true)
            {
                Application.Current.Properties.Remove("email");
                Application.Current.Properties.Remove("name");
                Application.Current.Properties.Remove("IsLoggedIn");
                await Application.Current.SavePropertiesAsync();
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", res.Response, "Okay");
            }
        }
    }
}
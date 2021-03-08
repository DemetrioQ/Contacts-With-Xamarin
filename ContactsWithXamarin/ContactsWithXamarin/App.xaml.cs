using ContactsWithXamarin.Views;
using Xamarin.Forms;

namespace ContactsWithXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var contact = new ContactsPage();
            NavigationPage.SetHasNavigationBar(contact, false);
            MainPage = new NavigationPage(contact);


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

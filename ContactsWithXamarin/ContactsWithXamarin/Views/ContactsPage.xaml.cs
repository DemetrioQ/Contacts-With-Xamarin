using ContactsWithXamarin.Services;
using ContactsWithXamarin.ViewModels;
using Xamarin.Forms;

namespace ContactsWithXamarin.Views
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = new ContactsViewModel(new AlertService(), new NavigationService(), new SortService(), new ActionSheetService());
            

        }

        private void ListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;

            // do stuff 

            ((ListView)sender).SelectedItem = null;
        }
    }
}

using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using ContactsWithXamarin.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsWithXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddContactPage : ContentPage
    {
        public AddContactPage(ObservableCollection<ContactGroupCollection> contacts)
        {
            InitializeComponent();
            BindingContext = new AddContactViewModel(new AlertService(), new NavigationService(), new SortService(), new ActionSheetService(), contacts);
        }

    }
}
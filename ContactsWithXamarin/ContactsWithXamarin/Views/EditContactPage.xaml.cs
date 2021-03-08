using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using ContactsWithXamarin.ViewModels;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsWithXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditContactPage : ContentPage
    {
        public EditContactPage(ObservableCollection<ContactGroupCollection> contacts, Contact contact)
        {
            InitializeComponent();
            BindingContext = new EditContactViewModel(new AlertService(), new NavigationService(), new SortService(), new ActionSheetService(), contacts, contact);
        }
    }
}
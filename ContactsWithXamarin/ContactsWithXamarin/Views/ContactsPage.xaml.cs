using ContactsWithXamarin.ViewModels;
using ContactsWithXamarin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsWithXamarin.Views
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
            BindingContext = new ContactsViewModel(new AlertService(), new NavigationService());
            
        }

    }
}

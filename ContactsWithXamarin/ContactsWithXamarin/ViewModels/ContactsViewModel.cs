using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ContactsWithXamarin.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        ObservableCollection<Contact> Contacts = new ObservableCollection<Contact>();
        public ContactsViewModel(IAlertService alertService, INavigationService navigationService) : base(alertService, navigationService)
        {

        }
    }
}

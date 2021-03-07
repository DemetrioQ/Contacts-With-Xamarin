using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsWithXamarin.ViewModels
{
    public class AddContactViewModel : BaseViewModel
    {
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public ICommand AddCommand { get; }
        public ObservableCollection<ContactGroupCollection> Contacts { get; }
        public AddContactViewModel(IAlertService alertService, INavigationService navigationService, ObservableCollection<ContactGroupCollection> contacts) : base(alertService, navigationService)
        {
            AddCommand = new Command(OnAddContact);
            Contacts = contacts;
        }

        public async void OnAddContact()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(Phone))
            {
                await AlertService.AlertAsync("Alerta", "The first name and phone fields can't be empty when adding a contact");
            }
            else
            {
                Contact contact = new Contact() { FirstName = FirstName, LastName = LastName, Phone = Phone };
                var contactGroup = Contacts.FirstOrDefault(p => p.Key == contact.FirstName[0].ToString());
                if (contactGroup == null)
                {
                    Contacts.Add(new ContactGroupCollection(contact.FirstName[0].ToString())
                    {
                        contact
                    });
                }
                else
                {
                    contactGroup.Add(contact);
                }
                await NavigationService.NavigationPopAsync();
            }



        }

    }
}

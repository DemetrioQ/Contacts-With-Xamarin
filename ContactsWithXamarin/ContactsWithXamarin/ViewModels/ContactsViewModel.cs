using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using ContactsWithXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContactsWithXamarin.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        public ICommand AddCommand { get; }
        public ICommand MoreCommand { get; }
        public ICommand DoneCommand { get; }
        public ICommand DeleteCommand { get; }
        


        private Contact _contact;
        public Contact SelectedContact
        {
            get { return _contact; }
            set { _contact = value;}
        }

        public ContactsViewModel(IAlertService alertService, INavigationService navigationService) : base(alertService, navigationService)
        {
            AddCommand = new Command(OnAddContact);
            MoreCommand = new Command<Contact>(OnMoreContact);
            DoneCommand = new Command(OnDone);
            DeleteCommand = new Command<Contact>(OnDeleteContact);

            Contacts = new ObservableCollection<Contact>()
            {
                new Contact(){FirstName = "Demetrio", LastName = "Quiñones", Image="cat.jpg", Phone="8091111423"}
            };
        }

        private async void OnAddContact()
        {
            await NavigationService.NavigationAsync(new AddContactPage(Contacts));
        }
        private async void OnMoreContact(Contact contact)
        {
           
           var option = await App.Current.MainPage.DisplayActionSheet($"{contact.FirstName} {contact.LastName}",null,null, new string[] { $"Call {contact.Phone}", "Edit" });
            if(option == $"Call {contact.Phone}")
            {
                PhoneDialer.Open(contact.Phone);
            }
            else if (option == "Edit")
            {
                SelectedContact = contact;
                await NavigationService.NavigationAsync(new EditContactPage() { BindingContext = this });
            }

        }

        public async void OnDone()
        {
            await NavigationService.NavigationPopAsync();
            SelectedContact = null;
        }

        public void OnDeleteContact(Contact contact)
        {
            Contacts.Remove(contact);
        }
    }
}

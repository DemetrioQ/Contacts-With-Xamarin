using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Contact> Contacts {get;}
        public AddContactViewModel(IAlertService alertService, INavigationService navigationService, ObservableCollection<Contact> contacts) : base(alertService, navigationService)
        {
            AddCommand = new Command(OnAddContact);
            Contacts = contacts;
        }

        public async void OnAddContact()
        {
            if(string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(Phone))
            {
                await AlertService.AlertAsync("Alerta", "Debe de ingresar el nombre y el numero de telefono del contacto que desea agregar");
            }
            else
            {
                Contact contact = new Contact() { FirstName = FirstName, LastName = LastName, Phone = Phone };
                Contacts.Add(contact);
                await NavigationService.NavigationPopAsync();
            }
            


        }

    }
}

using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using ContactsWithXamarin.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Contact = ContactsWithXamarin.Models.Contact;

namespace ContactsWithXamarin.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        private ObservableCollection<ContactGroupCollection> _contacts;
        public ObservableCollection<ContactGroupCollection> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }
        public ICommand AddCommand { get; }
        public ICommand MoreCommand { get; }
        public ICommand DeleteCommand { get; }


        private Contact _contact;
        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
            }
        }

        public ContactsViewModel(IAlertService alertService, INavigationService navigationService, SortService sortService, ActionSheetService actionSheetService) : base(alertService, navigationService, sortService, actionSheetService)
        {
            AddCommand = new Command(OnAddContact);
            MoreCommand = new Command<Contact>(OnMoreContact);
            DeleteCommand = new Command<Contact>(OnDeleteContact);



            Contacts = new ObservableCollection<ContactGroupCollection>()
            {
                new ContactGroupCollection("D")
                {
                    new Contact(){FirstName = "Diego", LastName = "Marignal", Image="cat.jpg", Phone="8091111423"},
                    new Contact(){FirstName = "Demetrio", LastName = "Quiñones", Image="cat.jpg", Phone="8091111423"}
                },
                new ContactGroupCollection("P")
                {
                new Contact(){FirstName = "Pedro", LastName = "Lost", Image="cat.jpg", Phone="8091111423"}

                },
                new ContactGroupCollection("M")
                {
                    new Contact(){FirstName = "Manuel", LastName = "Paulino", Image="cat.jpg", Phone="8091111423"}
                },
                new ContactGroupCollection("J")
                {
                    new Contact(){FirstName = "Jose", LastName = "Espiral", Image="cat.jpg", Phone="8091111423"}
                }



            };
        }

        private async void OnAddContact()
        {
            await NavigationService.NavigationAsync(new AddContactPage(Contacts));
        }
        private async void OnMoreContact(Contact contact)
        {
            var option = await ActionSheetService.ActionSheetAsync($"{contact.FirstName} {contact.LastName}", new string[] { $"Call {contact.Phone}", "Edit" });
            if (option == $"Call {contact.Phone}")
            {
                PhoneDialer.Open(contact.Phone);
            }
            else if (option == "Edit")
            {
                await NavigationService.NavigationAsync(new EditContactPage(Contacts, contact));
            }

        }


        public void OnDeleteContact(Contact contact)
        {
            var contactGroup = Contacts.FirstOrDefault(c => c.Contains(contact));
            if (contactGroup != null)
            {
                contactGroup.Remove(contact);
            }
        }

    }
}

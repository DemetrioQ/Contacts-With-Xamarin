using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using ContactsWithXamarin.Views;
using PCLStorage;
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
            GetData();
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

        public async void GetData()
        {
            /*IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
            string fileName = "userdata.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);*/
            Contacts = await DataManagmentService.LoadData(Contacts);
        }
        public void OnDeleteContact(Contact contact)
        {
            var contactGroup = Contacts.FirstOrDefault(c => c.Contains(contact));
            if (contactGroup != null)
            {
                contactGroup.Remove(contact);
                if(contactGroup.Count == 0)
                {
                    Contacts.Remove(contactGroup);
                }
                else
                {
                    contactGroup.First().GroupHeader = contactGroup.Key;
                }

                DataManagmentService.SaveData(Contacts);
            }
        }

    }
}

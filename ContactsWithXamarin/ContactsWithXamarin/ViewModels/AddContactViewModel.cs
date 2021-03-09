using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using ContactsWithXamarin.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Contact = ContactsWithXamarin.Models.Contact;

namespace ContactsWithXamarin.ViewModels
{
    public class AddContactViewModel : BaseViewModel
    {
        public ICommand AddCommand { get; }
        public ICommand SelectImageCommand { get; }
        public ICommand ScanQrCommand { get; }

        private Contact _contact;
        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                OnPropertyChanged("Contact");
            }
        }
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
        public AddContactViewModel(IAlertService alertService, INavigationService navigationService, SortService sortService, ActionSheetService actionSheetService, ObservableCollection<ContactGroupCollection> contacts) : base(alertService, navigationService, sortService, actionSheetService)
        {
            AddCommand = new Command(OnAddContact);
            SelectImageCommand = new Command(OnSelectImage);
            ScanQrCommand = new Command(OnScanQrCode);
            Contacts = contacts;
            Contact = new Contact();
            Contact.Image = "AddImageIcon";

        }

        public async void OnAddContact()
        {
            if (string.IsNullOrEmpty(Contact.FirstName) || string.IsNullOrEmpty(Contact.Phone))
            {
                await AlertService.AlertAsync("Alerta", "The first name and phone fields can't be empty when adding a contact");
            }
            else
            {
                if (Contact.Image == "AddImageIcon")
                {
                    Contact.Image = "DefaultProfileImage";
                }
                Contact.FirstName = char.ToUpper(Contact.FirstName[0]) + Contact.FirstName.Substring(1);
                if (!string.IsNullOrEmpty(Contact.LastName))
                {
                    Contact.LastName = char.ToUpper(Contact.LastName[0]) + Contact.LastName.Substring(1);
                }
                var contactGroup = Contacts.FirstOrDefault(p => p.Key == Contact.FirstName[0].ToString());
                if (contactGroup == null)
                {
                    Contacts.Add(new ContactGroupCollection(Contact.FirstName[0].ToString())
                    {
                        Contact
                    });
                    SortService.SortGroupCollection(Contacts);

                }
                else
                {
                    contactGroup.Add(Contact);
                    SortService.SortContactCollection(contactGroup, Contacts);
                };


                await NavigationService.NavigationPopAsync();
            }


        }
        public async void OnSelectImage()
        {
            var option = await ActionSheetService.ActionSheetAsync("Change Photo", new string[] { "Take Photo", "Choose Photo" });
            if (option == "Take Photo")
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                Contact.Image = photo.FullPath;
            }
            else if (option == "Choose Photo")
            {
                var photo = await MediaPicker.PickPhotoAsync();

                Contact.Image = photo.FullPath;

            }
        }

        public async void OnScanQrCode()
        {
            await NavigationService.NavigationAsync(new QrScannerPage(Contact));
        }


    }
}

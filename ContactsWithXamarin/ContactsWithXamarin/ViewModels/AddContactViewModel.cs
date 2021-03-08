using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
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
        private ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }

            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public ICommand AddCommand { get; }
        public ICommand SelectImageCommand { get; }
        public ObservableCollection<ContactGroupCollection> Contacts { get; }
        public AddContactViewModel(IAlertService alertService, INavigationService navigationService, SortService sortService, ActionSheetService actionSheetService, ObservableCollection<ContactGroupCollection> contacts) : base(alertService, navigationService, sortService, actionSheetService)
        {
            AddCommand = new Command(OnAddContact);
            SelectImageCommand = new Command(OnSelectImage);
            Contacts = contacts;
            Image = "cat.jpg";
        }

        public async void OnAddContact()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(Phone))
            {
                await AlertService.AlertAsync("Alerta", "The first name and phone fields can't be empty when adding a contact");
            }
            else
            {
                Contact contact = new Contact() { Image = Image, FirstName = FirstName, LastName = LastName, Phone = Phone };
                contact.FirstName = char.ToUpper(contact.FirstName[0]) + contact.FirstName.Substring(1);
                if (!string.IsNullOrEmpty(contact.LastName))
                {
                    contact.LastName = char.ToUpper(contact.LastName[0]) + contact.LastName.Substring(1);
                }
                var contactGroup = Contacts.FirstOrDefault(p => p.Key == contact.FirstName[0].ToString());
                if (contactGroup == null)
                {
                    Contacts.Add(new ContactGroupCollection(contact.FirstName[0].ToString())
                    {
                        contact
                    });
                    SortService.SortGroupCollection(Contacts);

                }
                else
                {
                    contactGroup.Add(contact);
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
                
                var stream = await photo.OpenReadAsync();

                Image = photo.FullPath;
                Console.WriteLine(photo.FullPath);


            }
            else if (option == "Choose Photo")
            {

            }
        }


    }
}

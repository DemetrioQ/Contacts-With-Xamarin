using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Contact = ContactsWithXamarin.Models.Contact;

namespace ContactsWithXamarin.ViewModels
{
    public class EditContactViewModel : BaseViewModel
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
        public ICommand SelectImageCommand { get; }
        public ICommand DoneCommand { get; }
        public Contact Before { get; set; }
        
        public Contact Contact { get; set; }
        public EditContactViewModel(IAlertService alertService, INavigationService navigationService, SortService sortService, ActionSheetService actionSheetService, ObservableCollection<ContactGroupCollection> contacts, Contact contact) : base(alertService, navigationService, sortService, actionSheetService)
        {
            DoneCommand = new Command(OnDone);
            SelectImageCommand = new Command(OnSelectImage);
            Contacts = contacts;
            Before = contact;
            Contact = CopyAToB(contact);
        }
        public async void OnDone()
        {
            if (string.IsNullOrEmpty(Contact.FirstName) || string.IsNullOrEmpty(Contact.Phone))
            {
                await AlertService.AlertAsync("Alerta", "The first name and phone fields can't be empty when editing a contact");
            }
            else
            {
                if (Contact.FirstName[0].ToString() == Before.FirstName[0].ToString())
                {
                    var contactGroup = Contacts.FirstOrDefault(c => c.Contains(Before));
                    contactGroup.Remove(Before);
                    contactGroup.Add(Contact);
                    SortService.SortContactCollection(contactGroup, Contacts);

                }
                else
                {
                    var beforecontactGroup = Contacts.FirstOrDefault(c => c.Contains(Before));
                    beforecontactGroup.Remove(Before);

                    var contactGroup = Contacts.FirstOrDefault(p => p.Key == Contact.FirstName[0].ToString());

                    Contact.FirstName = char.ToUpper(Contact.FirstName[0]) + Contact.FirstName.Substring(1);
                    if (!string.IsNullOrEmpty(Contact.LastName))
                    {
                        Contact.LastName = char.ToUpper(Contact.LastName[0]) + Contact.LastName.Substring(1);
                    }

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
                    }
                }



                await NavigationService.NavigationPopAsync();
            }

        }
        public Contact CopyAToB(Contact a)
        {
            Contact b = new Contact();
            // copy fields
            var typeOfA = a.GetType();
            var typeOfB = b.GetType();
            foreach (var fieldOfA in typeOfA.GetFields())
            {
                var fieldOfB = typeOfB.GetField(fieldOfA.Name);
                fieldOfB.SetValue(b, fieldOfA.GetValue(a));
            }
            // copy properties
            foreach (var propertyOfA in typeOfA.GetProperties())
            {
                var propertyOfB = typeOfB.GetProperty(propertyOfA.Name);
                propertyOfB.SetValue(b, propertyOfA.GetValue(a));
            }

            return b;
        }
        public async void OnSelectImage()
        {
            var option = await ActionSheetService.ActionSheetAsync("Change Photo", new string[] { "Take Photo", "Choose Photo" });
            if (option == "Take Photo")
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                var stream = await photo.OpenReadAsync();

                Contact.Image = photo.FullPath;


            }
            else if (option == "Choose Photo")
            {
                var photo = await MediaPicker.PickPhotoAsync();

                var stream = await photo.OpenReadAsync();

                Contact.Image = photo.FullPath;
            }
        }

    }


}


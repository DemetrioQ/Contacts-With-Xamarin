using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsWithXamarin.ViewModels
{
    public class EditContactViewModel : BaseViewModel
    {
        ObservableCollection<ContactGroupCollection> Contacts { get; set; }
        public ICommand DoneCommand { get; }
        public Contact Before { get; set; }
        public Contact After { get; set; }
        public EditContactViewModel(IAlertService alertService, INavigationService navigationService, SortService sortService, ActionSheetService actionSheetService, ObservableCollection<ContactGroupCollection> contacts, Contact contact) : base(alertService, navigationService, sortService, actionSheetService)
        {
            DoneCommand = new Command(OnDone);
            Contacts = contacts;
            Before = contact;
            After = CopyAToB(contact);
        }
        public async void OnDone()
        {
            if (string.IsNullOrEmpty(After.FirstName) || string.IsNullOrEmpty(After.Phone))
            {
                await AlertService.AlertAsync("Alerta", "The first name and phone fields can't be empty when editing a contact");
            }
            else
            {
                if (After.FirstName[0].ToString() == Before.FirstName[0].ToString())
                {
                    var contactGroup = Contacts.FirstOrDefault(c => c.Contains(Before));
                    contactGroup.Remove(Before);
                    contactGroup.Add(After);
                    SortService.SortContactCollection(contactGroup, Contacts);

                }
                else
                {
                    var beforecontactGroup = Contacts.FirstOrDefault(c => c.Contains(Before));
                    beforecontactGroup.Remove(Before);

                    var contactGroup = Contacts.FirstOrDefault(p => p.Key == After.FirstName[0].ToString());

                    After.FirstName = char.ToUpper(After.FirstName[0]) + After.FirstName.Substring(1);
                    if (!string.IsNullOrEmpty(After.LastName))
                    {
                        After.LastName = char.ToUpper(After.LastName[0]) + After.LastName.Substring(1);
                    }

                    if (contactGroup == null)
                    {
                        Contacts.Add(new ContactGroupCollection(After.FirstName[0].ToString())
                        {
                            After
                        });
                        SortService.SortGroupCollection(Contacts);
                    }
                    else
                    {
                        contactGroup.Add(After);
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

    }


}


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
    public class EditContactViewModel : BaseViewModel
    {
        ObservableCollection<ContactGroupCollection> Contacts { get; set; }
        public ICommand DoneCommand { get; }
        public Contact Before { get; set; }
        public Contact After { get; set; }
        public EditContactViewModel(IAlertService alertService, INavigationService navigationService, SortService sortService, ObservableCollection<ContactGroupCollection> contacts, Contact contact) : base(alertService, navigationService, sortService)
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
                if(After.FirstName[0].ToString() == Before.FirstName[0].ToString())
                {
                    var contactGroup = Contacts.FirstOrDefault(c => c.Contains(Before));
                    int index = contactGroup.IndexOf(Before);
                    contactGroup.Remove(Before);
                    contactGroup.Insert(index, After);
                    
                }
                else
                {
                    var beforecontactGroup = Contacts.FirstOrDefault(c => c.Contains(Before));
                    beforecontactGroup.Remove(Before);

                    var contactGroup = Contacts.FirstOrDefault(p => p.Key == After.FirstName[0].ToString());
                    if (contactGroup == null)
                    {
                        Contacts.Add(new ContactGroupCollection(After.FirstName[0].ToString())
                        {
                            After
                        });
                    }
                    else
                    {
                        contactGroup.Add(After);
                    }
                }

                SortService.SortCollection(Contacts);

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


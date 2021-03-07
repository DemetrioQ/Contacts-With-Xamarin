using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ContactsWithXamarin.Models
{
    public class ContactGroupCollection : ObservableCollection<Contact>
    {
        public string Key { get; }

        public ContactGroupCollection(string key)
        {
            Key = key;
        }

    }
}

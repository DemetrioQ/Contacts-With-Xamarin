using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ContactsWithXamarin.Models
{
    public class ContactGroupCollection : ObservableCollection<Contact>
    {
        public string Key { get; set; }
        public ContactGroupCollection(string key)
        {
            Key = key;
        }
        public ContactGroupCollection()
        {

        }

    }
}

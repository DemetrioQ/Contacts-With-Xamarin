using System.Collections.ObjectModel;

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

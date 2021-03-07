using ContactsWithXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ContactsWithXamarin.Services
{
    public class SortService : ISortService
    {
        public void SortCollection(ObservableCollection<ContactGroupCollection> Contacts)
        { 
            var temp = new ObservableCollection<ContactGroupCollection>(Contacts.OrderBy(x => x.Key));
            Contacts.Clear();
            foreach (var group in temp)
            {
                var item = new ObservableCollection<Contact>(group.OrderBy(c => c.FirstName));
                group.Clear();
                foreach (var contact in item)
                {
                    group.Add(contact);
                }
                Contacts.Add(group);

            }
        }
    }
}

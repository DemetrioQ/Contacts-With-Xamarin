using ContactsWithXamarin.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ContactsWithXamarin.Services
{
    public class SortService : ISortService
    {
        public void SortGroupCollection(ObservableCollection<ContactGroupCollection> groups)
        {
            var temp = new ObservableCollection<ContactGroupCollection>(groups.OrderBy(x => x.Key));
            groups.Clear();

            foreach (var group in temp)
            {
                groups.Add(group);
            }
            DataManagmentService.SaveData(groups);

        }
        
        public void SortContactCollection(ContactGroupCollection Group, ObservableCollection<ContactGroupCollection> groups)
        {
            Group.First().GroupHeader = "";
            var item = new ObservableCollection<Contact>(Group.OrderBy(c => c.FirstName));
            var index = groups.IndexOf(Group);
            groups.Remove(Group);
            Group.Clear();

            foreach (var contact in item)
            {
                Group.Add(contact);
            }
            Group.First().GroupHeader = Group.Key;
            groups.Insert(index, Group);

            DataManagmentService.SaveData(groups);

        }
    }
}

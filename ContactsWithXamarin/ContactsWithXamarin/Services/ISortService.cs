using ContactsWithXamarin.Models;
using System.Collections.ObjectModel;

namespace ContactsWithXamarin.Services
{
    public interface ISortService
    {
        void SortGroupCollection(ObservableCollection<ContactGroupCollection> groups);
        void SortContactCollection(ContactGroupCollection group, ObservableCollection<ContactGroupCollection> groups);
    }
}

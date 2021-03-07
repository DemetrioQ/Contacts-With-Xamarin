using ContactsWithXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ContactsWithXamarin.Services
{
    public interface ISortService
    {
        void SortCollection(ObservableCollection<ContactGroupCollection> Contacts);
    }
}

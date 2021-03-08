using ContactsWithXamarin.Models;
using System;
using System.Collections.Generic;
using PCLStorage;
using System.Collections.ObjectModel;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Plugin.Settings;
using System.Linq;

namespace ContactsWithXamarin.Services
{
    public static class DataManagmentService
    {
        //Only Lists by group are saved, Key is missing
       public static async void SaveData(ObservableCollection<ContactGroupCollection> contacts)
       {
            var json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            if (App.Current.Properties.ContainsKey("data"))
            {
                
                App.Current.Properties["data"] = json;
            }
            else
            {
                App.Current.Properties.Add("data", json);
            }
            await App.Current.SavePropertiesAsync();

        }

        public static async Task<ObservableCollection<ContactGroupCollection>> LoadData(ObservableCollection<ContactGroupCollection> Contacts)
        {
            if (App.Current.Properties.ContainsKey("data"))
            {
                var groups = JsonConvert.DeserializeObject<ObservableCollection<ContactGroupCollection>>(App.Current.Properties["data"].ToString());
                foreach (var contacts in groups)
                {
                    contacts.Key = contacts.First().FirstName[0].ToString();
                }
                return groups;
            } 
            else
            {
               return  new ObservableCollection<ContactGroupCollection>();
            }


        }
    }
}

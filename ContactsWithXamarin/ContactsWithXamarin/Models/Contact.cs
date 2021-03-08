using ContactsWithXamarin.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContactsWithXamarin.Models
{
    public class Contact : INotifyPropertyChanged
    {
        private string _image;
        public string Image {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            } 
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

        public Contact()
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

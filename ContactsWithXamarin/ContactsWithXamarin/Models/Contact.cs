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
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Company { get; set; }
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

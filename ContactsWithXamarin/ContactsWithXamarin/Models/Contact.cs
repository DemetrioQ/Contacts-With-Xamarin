using ContactsWithXamarin.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ContactsWithXamarin.Models
{
    public class Contact : INotifyPropertyChanged
    {
        private ImageSource _image;
        public ImageSource Image {
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

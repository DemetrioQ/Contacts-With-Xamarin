using Xamarin.Forms;

namespace ContactsWithXamarin.Models
{
    public class Contact
    {
        public ImageSource Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }

    }
}

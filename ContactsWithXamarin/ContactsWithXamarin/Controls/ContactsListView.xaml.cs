using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsWithXamarin.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsListView : ContentView
    {
        public ContactsListView()
        {
            InitializeComponent();
        }
    }
}
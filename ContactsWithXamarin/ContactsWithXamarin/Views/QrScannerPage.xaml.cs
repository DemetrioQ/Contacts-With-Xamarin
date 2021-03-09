using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using ContactsWithXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsWithXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrScannerPage : ContentPage
    {
        public QrScannerPage(Contact contact)
        {
            InitializeComponent();
            BindingContext = new QrScannerViewPage(new AlertService(), new NavigationService(), new SortService(), new ActionSheetService(), contact);
        }
    }
}
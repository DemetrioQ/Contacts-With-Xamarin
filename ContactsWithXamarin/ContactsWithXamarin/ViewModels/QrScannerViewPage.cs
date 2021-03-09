using ContactsWithXamarin.Models;
using ContactsWithXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactsWithXamarin.ViewModels
{
    public class QrScannerViewPage: BaseViewModel
    {
        public ICommand ScanResultCommand { get; }
        private Contact _contact;
        public Contact Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                OnPropertyChanged("Contact");
            }
        }
        public QrScannerViewPage(IAlertService alertService, INavigationService navigationService, SortService sortService, ActionSheetService actionSheetService, Contact contact) : base(alertService, navigationService, sortService, actionSheetService)
        {
            ScanResultCommand = new Command<ZXing.Result>(OnScanResult);
            Contact = contact;
        }

        public async void OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string[] contact_s = result.Text.Trim().Split('*');
                Contact.FirstName = contact_s[0];
                Contact.LastName = contact_s[1];
                Contact. Phone = contact_s[2];
                await NavigationService.NavigationPopAsync();
            });
        }
    }
}

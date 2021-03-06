using System;
using System.Threading.Tasks;
using ContactsWithXamarin.Views;
using Xamarin.Forms;

namespace ContactsWithXamarin.Services
{
    public class AlertService : IAlertService
    {
        public Task AlertAsync(string title, string description)
        {
            return App.Current.MainPage.DisplayAlert(title, description, "Ok");
        }
    }
}

using System.Threading.Tasks;

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

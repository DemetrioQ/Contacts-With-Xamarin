using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsWithXamarin.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigationAsync(Page page)
        {
            return App.Current.MainPage.Navigation.PushAsync(page);
        }

        public Task NavigationPopAsync()
        {
            return App.Current.MainPage.Navigation.PopAsync();
        }

    }
}

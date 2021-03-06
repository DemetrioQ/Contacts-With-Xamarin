using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactsWithXamarin.Services
{
    public interface INavigationService
    {
        Task NavigationAsync(Page page);
        Task NavigationPopAsync();
    }
}

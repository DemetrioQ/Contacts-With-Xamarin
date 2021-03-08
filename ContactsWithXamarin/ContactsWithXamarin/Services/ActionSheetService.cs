using System.Threading.Tasks;

namespace ContactsWithXamarin.Services
{
    public class ActionSheetService : IActionSheetService
    {
        public Task<string> ActionSheetAsync(string tittle, string[] options)
        {
            return App.Current.MainPage.DisplayActionSheet(tittle, null, null, options);
        }
    }
}

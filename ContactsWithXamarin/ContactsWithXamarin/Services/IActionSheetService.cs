using System.Threading.Tasks;

namespace ContactsWithXamarin.Services
{
    public interface IActionSheetService
    {
        Task<string> ActionSheetAsync(string tittle, string[] options);
    }
}

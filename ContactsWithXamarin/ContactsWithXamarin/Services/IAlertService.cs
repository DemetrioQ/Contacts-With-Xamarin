using System.Threading.Tasks;

namespace ContactsWithXamarin.Services
{
    public interface IAlertService
    {
        Task AlertAsync(string title, string description);
    }
}

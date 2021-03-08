using ContactsWithXamarin.Services;
using System.ComponentModel;

namespace ContactsWithXamarin.ViewModels
{

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public IAlertService AlertService { get; }
        public INavigationService NavigationService { get; }
        public ISortService SortService { get; }
        public IActionSheetService ActionSheetService { get; }

        protected BaseViewModel(IAlertService alertService, INavigationService navigationService, ISortService sortService, IActionSheetService actionSheetService)
        {
            AlertService = alertService;
            NavigationService = navigationService;
            SortService = sortService;
            ActionSheetService = actionSheetService;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

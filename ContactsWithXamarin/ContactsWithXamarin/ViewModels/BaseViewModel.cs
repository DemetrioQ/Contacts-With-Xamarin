using ContactsWithXamarin.Services;
using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ContactsWithXamarin.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, INotifyCollectionChanged
    {
        public IAlertService AlertService { get; }
        public INavigationService NavigationService { get; }

        public ISortService SortService { get; }
        protected BaseViewModel(IAlertService alertService, INavigationService navigationService, ISortService sortService)
        {
            AlertService = alertService;
            NavigationService = navigationService;
            SortService = sortService;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

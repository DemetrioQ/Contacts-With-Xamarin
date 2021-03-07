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

        protected BaseViewModel(IAlertService alertService, INavigationService navigationService)
        {
            AlertService = alertService;
            NavigationService = navigationService;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}

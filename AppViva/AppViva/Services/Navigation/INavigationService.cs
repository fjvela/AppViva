using AppViva.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppViva.Services
{
    public interface INavigationService
    {
        ViewModelBase PreviousPageViewModel { get; }

        void Initialize();

        Task NavigateToAsync<TViewModel>(object parameter = null, bool isModal = false) where TViewModel : ViewModelBase;

        Task ShowPopupAsync(ViewModelBase viewModelobject, object parameter = null);

        Task HidePopupAsync();

        Task NavigateBackAsync();

        Task RemoveBackStackAsync();

        Task RemoveLastFromBackStackAsync();
    }
}

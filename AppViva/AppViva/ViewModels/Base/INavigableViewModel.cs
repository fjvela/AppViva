using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppViva.ViewModels
{
    public interface INavigableViewModel
    {
        Task InitializeAsync(object parameter);
        Task ActivateAsync();
        void Deactivate();

        bool OnBackButtonPressed();
    }
}

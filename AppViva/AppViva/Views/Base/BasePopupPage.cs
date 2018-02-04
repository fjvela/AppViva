using AppViva.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppViva.Views.Popups
{
    public class BasePopupPage : PopupPage
    {
        protected INavigableViewModel ViewModel => BindingContext as INavigableViewModel;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            ViewModel?.ActivateAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel?.Deactivate();
        }

        protected override bool OnBackButtonPressed()
        {
            return ViewModel?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
        }
    }
}

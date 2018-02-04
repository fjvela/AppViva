//using AppViva.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using AppViva.ViewModels;
//using Rg.Plugins.Popup.Pages;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppViva.Views.Base
//{
//    public abstract class Base 
//    {
//        protected INavigableViewModel ViewModel => BindingContext as INavigableViewModel;

//        protected override async void OnAppearing()
//        {
//            base.OnAppearing();
//            await Task.Delay(200);
//            ViewModel?.ActivateAsync();
//        }

//        protected override void OnDisappearing()
//        {
//            base.OnDisappearing();
//            ViewModel?.Deactivate();
//        }

//        protected override bool OnBackButtonPressed()
//        {
//            return ViewModel?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
//        }
//    }
//}

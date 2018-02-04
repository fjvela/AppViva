using AppViva.Extensions;
using AppViva.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppViva.Views
{
    public class BaseContentPage : ContentPage
    {
        protected INavigableViewModel ViewModel => BindingContext as INavigableViewModel;


        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);
        //}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            ViewModelBase.UpdateToken();
            ViewModel?.ActivateAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModelBase.TryCancelToken();
            ViewModel?.Deactivate();
        }

        protected override bool OnBackButtonPressed()
        {
            return ViewModel?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
        }

        public string IconPlatform
        {
            get { return (string)GetValue(IconPlatformProperty); }
            set { SetValue(IconPlatformProperty, value); }
        }

        public static BindableProperty IconPlatformProperty = BindableProperty.Create(
            nameof(IconPlatform),
            typeof(string),
            typeof(BaseContentPage),
            null,
            propertyChanged: OnSourcePlatformChanged);

        private static void OnSourcePlatformChanged(BindableObject b, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            var control = (BaseContentPage)b;
            control.SetSourcePlatform((string)newValue);
        }
    }
}

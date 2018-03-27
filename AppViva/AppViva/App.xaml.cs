using AppViva.Services;
using Xamarin.Forms;
using DLToolkit.Forms.Controls;
using AppViva.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Push;

namespace AppViva
{
    public partial class App : Application
    {
        //public static UIParent UiParent = null;

        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            InitApp();

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.UWP)
            {
                InitNavigation();
            }
        }

        private void InitApp()
        {
            ServiceLocator.RegisterDependencies();
            ViewModelLocator.RegisterDependencies();
            IocContainer.Build();


        }



        private void InitNavigation()
        {
            var navigationService = ServiceLocator.NavigationService;
            navigationService.Initialize();
        }

        protected override void OnStart()
        {
            if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP)
            {
                InitNavigation();
            }


            AppCenter.Start("android=1c988543-0715-4c99-b660-dbaf0e210557;",
                   typeof(Analytics),
                   typeof(Crashes),
                   typeof(Distribute)
                   );

        }



        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

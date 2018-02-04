using AppViva.Services;
using AppViva.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppViva.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private ILoginService loginService;
        private IClassService classesService;
        public LoginViewModel(ILoginService loginService, IClassService classesService)
        {
            this.loginService = loginService;
            this.classesService = classesService;


            Login = Helpers.Settings.Login;


        }

        #region Properties

        private string login;
        public string Login
        {
            get { return login; }
            set { Set(ref login, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { Set(ref password, value); }
        }
        #endregion

        #region Commands
        private Command _signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                return _signInCommand ??
                    (_signInCommand = new Command(o => TryToLoginAsync(false), o => !IsBusy));
            }
        }

        #endregion


        #region Private Methodsa
        private Task TryToLoginAsync(bool silentLogin)
        {
            return ExecuteOperationAsync(async () =>
            {
                try
                {

                    var loginInfo = await loginService.Login(login, password);
                    if (loginInfo != null)
                    {
                        if (string.IsNullOrEmpty(loginInfo.Error))
                        {
                            Helpers.Settings.Login = login;

                            GlobalSetting.Instance.LoginInfo = loginInfo;
                            await NavigationService.NavigateToAsync<AllClassesViewModel>();
                        }
                        else
                        {
                            await NavigationService.ShowPopupAsync(
                                 new MessageBoxPopupViewModel()
                                 {
                                     Title = "",
                                     Subtitle = loginInfo.Error
                                 });
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }



        #endregion
    }
}

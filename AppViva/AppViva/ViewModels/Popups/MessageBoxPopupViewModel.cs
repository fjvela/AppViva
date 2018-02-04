using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppViva.ViewModels.Popups
{
    public class MessageBoxPopupViewModel : ViewModelBase
    {
        public MessageBoxPopupViewModel()
        {
            Title = string.Empty;

        }


        private string title;
        private string subtitle;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }

        public string Subtitle
        {
            get { return subtitle; }
            set { Set(ref subtitle, value); }
        }

        private Command _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ??
                    (_closeCommand = new Command(async o =>
                    {
                        await NavigationService.HidePopupAsync();

                    }));
            }
        }

    }
}

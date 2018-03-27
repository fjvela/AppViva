using AppViva.Extensions;
using AppViva.Models;
using AppViva.Services;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppViva.ViewModels
{
    public class AllClassesViewModel : ViewModelBase
    {

        private IClassService classService;

        public AllClassesViewModel(IClassService classService)
        {
            this.classService = classService;

            //List<ContentPage> ContentPages = new List<ContentPage>();
            //ContentPages.Add(new ContentPage());
            Pages = new ObservableCollection<ClassViewModel>();
        }

        public override async Task ActivateAsync()
        {
            await ExecuteOperationAsync(FillClassesAsync);
            //await ExecuteOperationQuietlyAsync(cancelToken => TryToUpdateNewsAsync(cancelToken));
        }

        #region Properties
        private ObservableCollection<ClassViewModel> pages;
        public ObservableCollection<ClassViewModel> Pages { get { return pages; } set { Set(ref pages, value); } }


        #endregion




        #region Private Methods


        private async Task FillClassesAsync(CancellationToken cancelToken)
        {
            //var classes = (await classService.GetListAsync()).ToList();
            var loginInfo = GlobalSetting.Instance.LoginInfo;

            var classes = (await classService.GetClassWeeklyAsync(loginInfo.Token, DateTime.Now, loginInfo.ClubNo, loginInfo.ContractPersonId));

            if (classes.Any())
            {
                Pages = new ObservableCollection<ClassViewModel>(classes.Select(p => new ClassViewModel(classService, p)));
            }
            else
            {
                Analytics.TrackEvent("No classes!");
            }
        }
        #endregion
    }
}

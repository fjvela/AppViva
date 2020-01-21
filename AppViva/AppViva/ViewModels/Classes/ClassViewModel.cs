using AppViva.Models;
using AppViva.Services;
using AppViva.ViewModels.Popups;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static AppViva.ViewModels.Popups.EquipamentListPopupViewModel;

namespace AppViva.ViewModels
{
    public class ClassViewModel : ViewModelBase
    {
        private ClassModel classSelected;
        private IClassService classService;
        private EquipamentListPopupViewModel selectEquipment;

        public ClassViewModel(IClassService classService, ClassWeeklyModel classModel)
        {
            this.classService = classService;
            Date = classModel.Date;
            ClassList = new ObservableCollection<ClassModel>(classModel.ClubClasses);
        }

        #region Properties

        private DateTime _date;
        private Command chooseItemCommand;
        private ObservableCollection<ClassModel> classList;

        public ICommand ChooseItemCommand
        {
            get
            {
                return chooseItemCommand ??
                    (chooseItemCommand = new Command(OnChooseItem, o => !IsBusy));
            }
        }

        public ObservableCollection<ClassModel> ClassList
        {
            get { return classList; }
            set { Set(ref classList, value); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { Set(ref _date, value); }
        }

        #endregion Properties

        #region Private methods

        private void OnChooseItem(object obj)
        {
            Task result;
            classSelected = obj as ClassModel;
            var loginInfo = GlobalSetting.Instance.LoginInfo;

            result = ExecuteOperationAsync(async () =>
            {
                ClassBookResult classResult = null;

                if (classSelected.IsBooked)
                {
                    classResult = await classService.CancelBookingAsync(loginInfo.Token, loginInfo.ContractPersonId, classSelected.Id, default(CancellationToken));
                }
                else if (classSelected.CanBook)
                {
                    if (classSelected.IsSpinning)
                    {
                        IList<EquipmentModel> listEquipment = await classService.GetListEquipmentForClass(loginInfo.Token, classSelected.Id, default(CancellationToken));

                        selectEquipment = new EquipamentListPopupViewModel(
                            listEquipment.Where(x => !x.IsBooked).ToList());
                        selectEquipment.ItemSelected += SelectEquipment_ItemSelectedAsync;
                        await NavigationService.ShowPopupAsync(selectEquipment);
                    }
                    else
                    {
                        classResult = await classService.AddBookingAsync(loginInfo.Token, loginInfo.ContractPersonId, classSelected.Id, default(CancellationToken));
                    }
                }
                Analytics.TrackEvent($"Class book: {classSelected}: {classResult?.Result}");
                if (classResult != null)
                    await ShowBookingResult(classResult);
            });
        }

        private async void SelectEquipment_ItemSelectedAsync(object sender, EquipamentEventArgs e)
        {
            var loginInfo = GlobalSetting.Instance.LoginInfo;

            selectEquipment.ItemSelected -= SelectEquipment_ItemSelectedAsync;
            await NavigationService.HidePopupAsync();
            ClassBookResult classResult = await classService.AddBookingAsync(
                loginInfo.Token,
                loginInfo.ContractPersonId,
                classSelected.Id,
                default(CancellationToken),
                e.Id);

            await ShowBookingResult(classResult);
        }

        private async Task ShowBookingResult(ClassBookResult classResult)
        {
            if (classResult != null)
            {
                await NavigationService.ShowPopupAsync(
                    new MessageBoxPopupViewModel()
                    {
                        Title = "",
                        Subtitle = classResult.Result
                    });
            }
        }

        #endregion Private methods
    }
}
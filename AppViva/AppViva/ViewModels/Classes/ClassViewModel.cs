using AppViva.Models;
using AppViva.Services;
using AppViva.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static AppViva.ViewModels.Popups.EquipamentListPopupViewModel;

namespace AppViva.ViewModels
{
    public class ClassViewModel : ViewModelBase
    {
        private IClassService classService;
        private EquipamentListPopupViewModel selectEquipment;
        private ClassModel classSelected;

        public ClassViewModel(IClassService classService, ClassWeeklyModel classModel)
        {
            this.classService = classService;
            Date = classModel.Date;
            ClassList = new ObservableCollection<ClassModel>(classModel.ClubClasses);

            



        }

        #region Properties 

        private Command chooseItemCommand;
        public ICommand ChooseItemCommand
        {
            get
            {
                return chooseItemCommand ??
                    (chooseItemCommand = new Command(OnChooseItem, o => !IsBusy));
            }
        }


        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { Set(ref _date, value); }
        }
        private ObservableCollection<ClassModel> classList;
        public ObservableCollection<ClassModel> ClassList
        {
            get { return classList; }
            set { Set(ref classList, value); }
        }
        #endregion

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
                else
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
                await ShowBookingResult(classResult);
            });


            //return result;

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

                //selectedNew.IsBooked = true;
            }
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

        #endregion
    }
}

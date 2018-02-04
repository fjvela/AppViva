using AppViva.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppViva.ViewModels.Popups
{
    public class EquipamentListPopupViewModel : ViewModelBase
    {
        public event EventHandler<EquipamentEventArgs> ItemSelected;

        public class EquipamentEventArgs : EventArgs
        {
            public int Id { get; set; }
        }


        public EquipamentListPopupViewModel(IList<EquipmentModel> data)
        {
            ListEquipment = new ObservableCollection<EquipmentModel>(data);
        }

        private EquipmentModel selectedEquiment;

        public EquipmentModel SelectedEquiment
        {
            get { return selectedEquiment; }
            set { Set(ref selectedEquiment, value); }
        }

        private ObservableCollection<EquipmentModel> listEquipment;

        public ObservableCollection<EquipmentModel> ListEquipment
        {
            get { return listEquipment; }
            set { Set(ref listEquipment, value); }
        }



        private Command _selectedCommand;
        public ICommand SelectedCommand => _selectedCommand ??
                  (_selectedCommand = new Command(x =>
                  {
                      if (selectedEquiment != null)
                      {
                          ItemSelected?.Invoke(this, new EquipamentEventArgs() { Id = selectedEquiment.Id });
                          //await NavigationService.HidePopupAsync();
                      }

                  }));
    }
}

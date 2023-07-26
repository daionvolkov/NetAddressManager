using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.ViewModels
{
    public class CreateEquipmentManufacturerPageViewModel : BindableBase
    {

        private AuthToken _token;
        private EquipmentRequestService _equipmentRequestService;
        private CommonViewService _commonViewService;



        #region COMMANDS

        public DelegateCommand SaveEquipmentManufacturerCommand { get; private set; } 
        public DelegateCommand ClearEquipmentManufacturerModelCommand { get; private set; }

        #endregion

        public CreateEquipmentManufacturerPageViewModel(AuthToken token)
        {
            _token = token;
            _equipmentRequestService = new EquipmentRequestService();
            _commonViewService = new CommonViewService();
            EquipmentManufacturerModel = new EquipmentManufacturerModel();

            SaveEquipmentManufacturerCommand = new DelegateCommand(SaveEquipmentManufacturer);
            ClearEquipmentManufacturerModelCommand = new DelegateCommand(ClearEquipmentManufacturerModel);
        }


        #region PROPERTIES

        private EquipmentManufacturerModel _equipmentManufacturerModel;

        public EquipmentManufacturerModel EquipmentManufacturerModel
        {
            get => _equipmentManufacturerModel;
            set
            {
                _equipmentManufacturerModel = value;
                RaisePropertyChanged(nameof(EquipmentManufacturerModel));
            }
        }
        #endregion


        #region METHODS

        private void SaveEquipmentManufacturer()
        {
            var result = _equipmentRequestService.CreateEquipment(_token, EquipmentManufacturerModel);
            _commonViewService.ShowActionRelust(result, "Одорудование добавлено");
        }


        private void ClearEquipmentManufacturerModel()
        {
            EquipmentManufacturerModel = new EquipmentManufacturerModel();
        }

        #endregion

    }
}

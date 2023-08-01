using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace NetAddressManager.Client.ViewModels
{
    public class UpdateSwitchWindowViewModel : BindableBase
    {
        private AuthToken _token;


        #region COMMANDS
        private CommonViewService _commonViewService;
        private CoreSwitchRequestService _coreSwitchRequestService;
        private AggregationSwitchRequestService _aggregationSwitchRequestService;
        private AccessSwitchRequestService _accessSwitchRequestService;
        #endregion

        public UpdateSwitchWindowViewModel(AuthToken token)
        {
            _token = token;
            _commonViewService = new CommonViewService();
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
            _accessSwitchRequestService = new AccessSwitchRequestService();
        }
       



        #region PROPERTIES
             
        private SwitchDetailsModel _switchDetailsModel;
        public SwitchDetailsModel SwitchDetailsModel
        {
            get => _switchDetailsModel; 
            set { 
                _switchDetailsModel = value; 
               RaisePropertyChanged(nameof(SwitchDetailsModel));
            }
        }
    

        #endregion

        #region METHODS
        public void OpenUpdateGateway()
        {
            
            MessageBox.Show(nameof(OpenUpdateGateway));
        }

        public void OpenUpdateAddress()
        {
            MessageBox.Show(nameof(OpenUpdateAddress));
        }

        public void OpenUpdateEquipment(object switchDetailsModel)
        {
            var updateSwitch = switchDetailsModel as SwitchDetailsModel;
            MessageBox.Show(nameof(OpenUpdateEquipment));
        }

        public void SaveUpdateSwitch(object switchDetailsModel)
        {
            var updateSwitch = switchDetailsModel as SwitchDetailsModel;  
            if(updateSwitch != null)
            {
                if(updateSwitch.SwitchType == SwitchType.Core)
                {
                    CoreSwitchUpdateClient(updateSwitch);
                }

                else if(updateSwitch.SwitchType == SwitchType.Aggregation)
                {
                    AggregationSwitchUpdateClient(updateSwitch);
                }


                else if (updateSwitch.SwitchType == SwitchType.Access)
                {
                    AccessSwitchUpdateClient(updateSwitch);
                }
            }   
        }

        private void CoreSwitchUpdateClient(SwitchDetailsModel updateSwitch)
        {
            var coreSwitch = new CoreSwitchModel(updateSwitch.SwitchData.IPAddress, updateSwitch.SwitchData.IPMask,
                        updateSwitch.SwitchData.MACAddress, updateSwitch.SwitchData.Description, updateSwitch.IPGateway);
            coreSwitch.Id = updateSwitch.SwitchData.Id;
            var resultAction = _coreSwitchRequestService.UpdateCoreSwitch(_token, coreSwitch);
            _commonViewService.ShowActionRelust(resultAction, "Данные обновлены");
        }

        private void AggregationSwitchUpdateClient(SwitchDetailsModel updateSwitch)
        {
            var aggregationSwitch = new AggregationSwitchModel(updateSwitch.SwitchData.IPAddress, updateSwitch.SwitchData.IPMask,
                        updateSwitch.SwitchData.MACAddress, updateSwitch.SwitchData.Description);
            aggregationSwitch.Id = updateSwitch.SwitchData.Id;
            var resultAction = _aggregationSwitchRequestService.UpdateAggregationSwitch(_token, aggregationSwitch);
            _commonViewService.ShowActionRelust(resultAction, "Данные обновлены");
        }

        private void AccessSwitchUpdateClient(SwitchDetailsModel updateSwitch)
        {
            var accessSwitch = new AccessSwitchModel(updateSwitch.SwitchData.IPAddress, updateSwitch.SwitchData.IPMask,
                       updateSwitch.SwitchData.MACAddress, updateSwitch.SwitchData.Description);
            accessSwitch.Id = updateSwitch.SwitchData.Id;
            var resultAction = _accessSwitchRequestService.UpdateAccessSwitch(_token, accessSwitch);
            _commonViewService.ShowActionRelust(resultAction, "Данные обновлены");
        }
        #endregion

    }
}

using GalaSoft.MvvmLight.CommandWpf;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace NetAddressManager.Client.ViewModels
{
    public class UpdateSwitchWindowViewModel : BindableBase
    {
        private AuthToken _token;
        private CommonViewService _commonViewService;
        private CoreSwitchRequestService _coreSwitchRequestService;
        private AggregationSwitchRequestService _aggregationSwitchRequestService;
        private AccessSwitchRequestService _accessSwitchRequestService;
        private PostalAddressRequestService _postalAddressRequestService;
        private CheckDataRequestService _checkDataRequestService;



        #region COMMANDS



        #endregion

        public UpdateSwitchWindowViewModel(AuthToken token)
        {
            _token = token;
            _commonViewService = new CommonViewService();
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
            _accessSwitchRequestService = new AccessSwitchRequestService();
            _postalAddressRequestService = new PostalAddressRequestService();
            _checkDataRequestService = new CheckDataRequestService();
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
            MessageBox.Show(nameof(OpenUpdateEquipment));
/*            var updateSwitch = switchDetailsModel as SwitchDetailsModel;
            var updateEquipmentWindow = new SearchEquipmentWindow();
            updateEquipmentWindow.DataContext = updateSwitch;
            updateEquipmentWindow.Show();*/
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
                    int addressId = GetPostalAddressId(updateSwitch);
                    int switchId = updateSwitch.SwitchData.Id;
                    AddPostalAddressToAggregationSwitchClient(switchId, addressId);
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


        private int GetPostalAddressId(SwitchDetailsModel updateSwitch)
        {
            string?  address = updateSwitch.PostalAddress;
            if (address != null)
            {
                PostalAddressModel postalAddressModel = _checkDataRequestService.IsPostalAddressExists(_token, address);

                if (postalAddressModel != null)
                {
                    return postalAddressModel.Id;

                }
                else
                {
                    var addrList = address.Split(',');  
                    if(addrList.Length == 3)
                    {
                        PostalAddressModel newAddress = new PostalAddressModel(addrList[0].Trim(), addrList[1].Trim(), addrList[2].Trim());
                        _postalAddressRequestService.CreatePostalAddress(_token, newAddress);
                        int id = _postalAddressRequestService.GetAllPostalAddresses(_token).Max(i => i.Id);
                        return id;
                    }
                    
                }
            }
                return 0;
        }

        private void AddPostalAddressToAggregationSwitchClient(int switchId, int addressId)
        {
            if(addressId != 0)
            {
                _aggregationSwitchRequestService.AddPostalAddressToAggregationSwitch(_token, switchId, addressId);
            }
        }


        #endregion

    }
}

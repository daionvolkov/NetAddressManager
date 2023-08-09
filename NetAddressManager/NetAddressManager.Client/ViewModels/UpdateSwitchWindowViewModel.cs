using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

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
        private EquipmentRequestService _equipmentRequestService;
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
            _equipmentRequestService = new EquipmentRequestService();
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
        public void SaveUpdateSwitch(object switchDetailsModel)
        {
            var updateSwitch = switchDetailsModel as SwitchDetailsModel;  
            if(updateSwitch != null)
            {
                if(updateSwitch.SwitchType == SwitchType.Core)
                {
                    CoreSwitchUpdateClient(updateSwitch);
                    int addressId = GetPostalAddressId(updateSwitch);
                    int equipmentId = GetEquipmentId(updateSwitch);
                    int switchId = updateSwitch.SwitchData.Id;
                    AddPostalAddressToCoreSwitchClient(switchId, addressId);
                    AddEquipmentToCoreSwitchClient(switchId, equipmentId);
                }

                else if(updateSwitch.SwitchType == SwitchType.Aggregation)
                {
                    
                    int addressId = GetPostalAddressId(updateSwitch);
                    int equipmentId = GetEquipmentId(updateSwitch); 
                    List<int> aggregationSwitchIds = new List<int>();
                    aggregationSwitchIds.Add(updateSwitch.SwitchData.Id);
                    int switchId = updateSwitch.SwitchData.Id;                          
                    AddPostalAddressToAggregationSwitchClient(switchId, addressId);
                    AddEquipmentToAggregationSwitchClient(switchId, equipmentId);
                    
                    AddGatewayToAggregationSwitch(updateSwitch, aggregationSwitchIds);

                    AggregationSwitchUpdateClient(updateSwitch);
                }


                else if (updateSwitch.SwitchType == SwitchType.Access)
                {

                    
                    int addressId = GetPostalAddressId(updateSwitch);
                    int equipmentId = GetEquipmentId(updateSwitch);
                    int switchId = updateSwitch.SwitchData.Id;                         
                    List<int> accessSwitchIds = new List<int>();
                    accessSwitchIds.Add(updateSwitch.SwitchData.Id);
                    AddPostalAddressToAccessSwitchClient(switchId, addressId);
                    AddEquipmentToAccessSwitchClient(switchId, equipmentId);

                    AddGatewayToAccessSwitch(updateSwitch, accessSwitchIds);
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
            string? address = updateSwitch.PostalAddress;
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
                    if (addrList.Length == 3)
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

        private int GetEquipmentId(SwitchDetailsModel updateSwitch)
        {
            string? equipment = updateSwitch.Equipment;
            if(equipment != null)
            {
                EquipmentManufacturerModel equipmentManufacturerModel = _checkDataRequestService.IsEquipmentExists(_token, equipment);
                if(equipmentManufacturerModel != null)
                {
                    return equipmentManufacturerModel.Id;
                }
                else
                {
                    var equepmentList = equipment.Split(",");
                    if(equepmentList.Length == 2)
                    {
                        EquipmentManufacturerModel newEquipment = new EquipmentManufacturerModel(equepmentList[0].Trim(), equepmentList[1].Trim());
                        _equipmentRequestService.CreateEquipment(_token, newEquipment);
                        int id = _equipmentRequestService.GetAllEquipments(_token).Max(i => i.Id);
                        return id;
                    }
                }
            }
            return 0;
        }

        private void AddPostalAddressToCoreSwitchClient(int switchId, int addressId)
        {
            if (addressId != 0)
            {
                _coreSwitchRequestService.AddPostalAddressToCoreSwitch(_token, switchId, addressId);
            }
        }


        private void AddPostalAddressToAggregationSwitchClient(int switchId, int addressId)
        {
            if(addressId != 0)
            {
                _aggregationSwitchRequestService.AddPostalAddressToAggregationSwitch(_token, switchId, addressId);
            }
        }


        private void AddPostalAddressToAccessSwitchClient(int switchId, int addressId)
        {
            if(addressId != 0)
            {
                _accessSwitchRequestService.AddPostalAddressToAccessSwitch(_token, switchId, addressId);
            }
        }


        private void AddEquipmentToCoreSwitchClient(int switchId, int equipmentId)
        {
            if (equipmentId != 0)
            {
                _coreSwitchRequestService.AddEquipmentToCoreSwitch(_token, switchId, equipmentId);
            }
        }


        private void AddEquipmentToAggregationSwitchClient(int switchId, int equipmentId)
        {
            if (equipmentId != 0)
            {
                _aggregationSwitchRequestService.AddEquipmentToAggregationSwitch(_token, switchId, equipmentId);
            }
        }

        private void AddEquipmentToAccessSwitchClient(int switchId, int equipmentId)
        {
            if (equipmentId != 0)
            {
                _accessSwitchRequestService.AddEquipmentToAccessSwitch(_token, switchId, equipmentId);
            }
        }

        private void AddGatewayToAggregationSwitch(SwitchDetailsModel updateSwitch, List<int> aggregationSwitchIds)
        {
            int gatewayId = _checkDataRequestService.IsCoreSwitchExistst(_token, updateSwitch.IPGateway).Id;
            if (gatewayId != 0)
            {
                _coreSwitchRequestService.AddAggregationSwitchToCoreSwitch(_token, gatewayId, aggregationSwitchIds);
            }
            else
            {
                _commonViewService.ShowMessage("Шлюз не добавлен.\nКоммутатора ядра сети с таким IP адресом не существует");
            }
        }

        private void AddGatewayToAccessSwitch(SwitchDetailsModel updateSwitch, List<int> accessSwitchIds)
        {
            int gatewayId = _checkDataRequestService.IsAggegationSwitchExists(_token, updateSwitch.IPGateway).Id;
            if (gatewayId != 0)
            {
                _aggregationSwitchRequestService.AddAccessSwitchToAggregationSwitch(_token, gatewayId, accessSwitchIds);
            }
            else
            {
                _commonViewService.ShowMessage("Шлюз не добавлен.\nКоммутатора агрегации сети с таким IP адресом не существует");
            }
        }

        #endregion
    }
}

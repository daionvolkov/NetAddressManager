using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views.Pages;
using NetAddressManager.Client.Views.Windows;
using NetAddressManager.ClientTests.Services;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace NetAddressManager.Client.ViewModels
{
    public class SearchPageViewModel : BindableBase
    {
        private CommonViewService _commonViewService;
        private NetAddressSearchRequestService _netAddressSearchRequestService;
        private AddressSearchRequestService _addressSearchRequestService;
        private EquipmentSearchRequestService _equipmentSearchRequestService;
        private CoreSwitchRequestService _coreSwitchRequestService;
        private AggregationSwitchRequestService _aggregationSwitchRequestService;
        private AccessSwitchRequestService _accessSwitchRequestService;
        private PostalAddressRequestService _postalAddressRequestService;
        private EquipmentRequestService _equipmentRequestService;
        

        private string coreSwicthId;

      


        #region COMMANDS
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<object> DetailsSwitchCommand { get; private set; }

        #endregion

        public SearchPageViewModel(AuthToken token)
        {
            Token = token;
            _commonViewService = new CommonViewService();
            _netAddressSearchRequestService = new NetAddressSearchRequestService();
            _addressSearchRequestService = new AddressSearchRequestService();
            _equipmentSearchRequestService = new EquipmentSearchRequestService();
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
            _accessSwitchRequestService = new AccessSwitchRequestService();
            _postalAddressRequestService = new PostalAddressRequestService();   
            _equipmentRequestService = new EquipmentRequestService();

            SearchCommand = new DelegateCommand(async () => await Search());
            DetailsSwitchCommand = new DelegateCommand<object>(OnDetailsSwitchClicked);

        }


    #region PROPERTIES
    private AuthToken _token;
        public AuthToken Token
        {
            get => _token;
            private set
            {
                _token = value;
                RaisePropertyChanged(nameof(Token));
            }
        }
        private string _searchResponse;
        public string SearchResponse
        {
            get => _searchResponse;
            set
            {
                _searchResponse = value;
                RaisePropertyChanged(nameof(SearchResponse));
            }
        }
        private bool _radioButtonIP;
        public bool RadioButtonIP
        {
            get { return _radioButtonIP; }
            set { SetProperty(ref _radioButtonIP, value); }
        }
        private bool _radioButtonAddress;
        public bool RadioButtonAddress
        {
            get { return _radioButtonAddress; }
            set { SetProperty(ref _radioButtonAddress, value); }
        }
        private bool _radioButtonEquipment;
        public bool RadioButtonEquipment
        {
            get { return _radioButtonEquipment; }
            set { SetProperty(ref _radioButtonEquipment, value); }
        }
        private SwitchDataModel _switchData;
        public SwitchDataModel SwitchData
        {
            get { return _switchData; }
            set { SetProperty(ref _switchData, value); }
        }


        public ObservableCollection<CoreSwitchModel> CoreSwitchData { get; set; }
        public ObservableCollection<AggregationSwitchModel> AggregationSwitchData { get; set; }
        public ObservableCollection<AccessSwitchModel> AccessSwitchData { get; set; }


        private SwitchType _switchType;

        public SwitchType SwitchType
        {
            get =>_switchType; 
            set { 
                _switchType = value;
                RaisePropertyChanged(nameof(SwitchType));
            }
        }
        private SwitchModel<CoreSwitchModel> _selectedCoreSwitch;
        public SwitchModel<CoreSwitchModel> SelectedCoreSwitch
        {
            get { return _selectedCoreSwitch; }
            set { SetProperty(ref _selectedCoreSwitch, value); }
        }
        private SwitchModel<AggregationSwitchModel> _selectedAggregationSwitch;

        public SwitchModel<AggregationSwitchModel> SelectedAggregationSwitch
        {
            get => _selectedAggregationSwitch; 
            set { SetProperty(ref _selectedAggregationSwitch, value); }
        }
        private SwitchModel<AccessSwitchModel> _selectedAccessSwitch;
        public SwitchModel<AccessSwitchModel> SelectedAccessSwitch
        {
            get => _selectedAccessSwitch;
            set { SetProperty(ref _selectedAccessSwitch, value); }
        }

        #endregion
        #region METHODS

        private async Task Search()
        {
            if (RadioButtonIP)
            {
                SwitchData = await _netAddressSearchRequestService.GetSwitchesByNetAddress(Token, SearchResponse);
            }
            else if (RadioButtonAddress)
            {
                SwitchData = await _addressSearchRequestService.GetSwitchesByAddress(Token, SearchResponse);
            }
            else if (RadioButtonEquipment)
            {
                SwitchData = await _equipmentSearchRequestService.GetSwitchesByEquipment(Token, SearchResponse);
            }
        }




        private void OnDetailsSwitchClicked(object switchData)
        {
            if (switchData is CoreSwitchModel coreSwitch)
            {
                var switchDetailsModel = GetCoreSwitchClient(coreSwitch);

                var detailsWindow = new DetailsSwitchWindow();
                detailsWindow.DataContext = switchDetailsModel;
                detailsWindow.ShowDialog();

            }
            else if (switchData is AggregationSwitchModel aggregationSwitch)
            {
                var switchDetailsModel = GetAggregationSwitchClient(aggregationSwitch);

                var detailsWindow = new DetailsSwitchWindow();
                detailsWindow.DataContext = switchDetailsModel; 
                detailsWindow.ShowDialog();

            }
            else if (switchData is AccessSwitchModel accessSwitch)
            {
                var switchDetailsModel = GetAccessSwitchClient(accessSwitch);

                var detailsWindow = new DetailsSwitchWindow();
                detailsWindow.DataContext = switchDetailsModel; 
                detailsWindow.ShowDialog();

            }
        }


        private SwitchDetailsModel<CoreSwitchModel> GetCoreSwitchClient(CoreSwitchModel coreSwitch)
        {
            int switchId = coreSwitch.Id;
            var coreSwitchData = _coreSwitchRequestService.GetCoreSwitchById(Token, switchId);
            
            SwitchType SwitchType = SwitchType.Core;
            string ipGatewayData = coreSwitch.IPGateway;

            int equipmentId = Convert.ToInt32(coreSwitch.EquipmentManufacturerId);
            int addressId = Convert.ToInt32(coreSwitch.PostalAddressId);

            string equipmentManufacturerStr = GetEquipmentClient(equipmentId);
            string addressStr = GetPostalAddressClient(addressId);

            var switchDetailsModel = new SwitchDetailsModel<CoreSwitchModel>
            {
                SwitchData = coreSwitchData,
                SwitchType = SwitchType,
                IPGateway = ipGatewayData,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr
            };

            return switchDetailsModel;
        }


        private SwitchDetailsModel<AggregationSwitchModel> GetAggregationSwitchClient(AggregationSwitchModel aggregationSwitch)
        {
            int switchId = aggregationSwitch.Id;
            
            string ipGatewayData = string.Empty;
            SwitchType SwitchType = SwitchType.Aggregation;
            int equipmentId = Convert.ToInt32(aggregationSwitch.EquipmentManufacturerId);
            int addressId = Convert.ToInt32(aggregationSwitch.PostalAddressId);

            var aggregationSwitchData = _aggregationSwitchRequestService.GetAggregationSwitchById(Token, switchId);
            string addressStr = GetPostalAddressClient(addressId);
            string equipmentManufacturerStr = GetEquipmentClient(equipmentId);

            if (aggregationSwitch.CoreSwitchId != null)
                ipGatewayData = _coreSwitchRequestService.GetCoreSwitchById(Token, (int)aggregationSwitch.CoreSwitchId).IPAddress;
            
            var switchDetailsModel = new SwitchDetailsModel<AggregationSwitchModel>
            {
                SwitchData = aggregationSwitchData,
                IPGateway = ipGatewayData,
                SwitchType = SwitchType,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr
            };
            return switchDetailsModel;
        }

        private SwitchDetailsModel<AccessSwitchModel> GetAccessSwitchClient(AccessSwitchModel accessSwitch)
        {
            int switchId = accessSwitch.Id;
            
            string ipGatewayData = string.Empty;
            int addressId = Convert.ToInt32(accessSwitch.PostalAddressId);
            int equipmentId = Convert.ToInt32(accessSwitch.EquipmentManufacturerId);
            SwitchType SwitchType = SwitchType.Access;

            var accessSwitchData = _accessSwitchRequestService.GetAccessSwitchById(Token, switchId);
            string equipmentManufacturerStr = GetEquipmentClient(equipmentId);
            string addressStr = GetPostalAddressClient(addressId);

            if (accessSwitch.AggregationSwitchId != null)
                ipGatewayData = _aggregationSwitchRequestService.GetAggregationSwitchById(Token, (int)accessSwitch.AggregationSwitchId).IPAddress;

            var switchDetailsModel = new SwitchDetailsModel<AccessSwitchModel>
            {
                SwitchData = accessSwitchData,
                IPGateway = ipGatewayData,
                SwitchType = SwitchType,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr
            };
            return switchDetailsModel;
        }


        private string GetPostalAddressClient(int addressId)
        {
            string addressStr = string.Empty;
            if (addressId != 0)
            {
                PostalAddressModel address = _postalAddressRequestService.GetPostalAddressById(Token, addressId);
                addressStr = $"{address.City}, {address.Street}, {address.Building}";
            }
            return addressStr;
        }

        private string GetEquipmentClient(int equipmentId)
        {
            string equipmentManufacturerStr = string.Empty;
            if (equipmentId != 0)
            {
                EquipmentManufacturerModel equipmentManufacturer = _equipmentRequestService.GetEquipmentById(Token, equipmentId);
                equipmentManufacturerStr = $"{equipmentManufacturer.Manufacturer}, {equipmentManufacturer.Model}";
            }
            return equipmentManufacturerStr;
        }



        #endregion
    }
}

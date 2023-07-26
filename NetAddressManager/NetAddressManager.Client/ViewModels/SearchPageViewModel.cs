using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views;
using NetAddressManager.ClientTests.Services;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

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
        private SwitchPortRequestService _switchPortRequestService;

        private MainWindowViewModel _mainWindowVM;
        private AuthToken _token;

        #region COMMANDS
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<object> DetailsSwitchCommand { get; private set; }


        public DelegateCommand<object> OpenUpdatePortCommand { get; }
        public DelegateCommand OpenCreatePortCommand { get; }
        public DelegateCommand CancelCommand { get; }


        #endregion

        public SearchPageViewModel(AuthToken token)
        {
            _token = token;
            //_mainWindowVM = mainWindowVM;

            _commonViewService = new CommonViewService();
            _netAddressSearchRequestService = new NetAddressSearchRequestService();
            _addressSearchRequestService = new AddressSearchRequestService();
            _equipmentSearchRequestService = new EquipmentSearchRequestService();
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
            _accessSwitchRequestService = new AccessSwitchRequestService();
            _postalAddressRequestService = new PostalAddressRequestService();   
            _equipmentRequestService = new EquipmentRequestService();
            _switchPortRequestService = new SwitchPortRequestService();

            PortDetails = new List<SwitchPortModel>();
            SearchCommand = new DelegateCommand(async () => await Search());
            DetailsSwitchCommand = new DelegateCommand<object>(OnDetailsSwitchClicked);


            OpenCreatePortCommand = new DelegateCommand(OpenCreatePort);

            OpenUpdatePortCommand = new DelegateCommand<object>(OpenUpdatePort);

            CancelCommand = new DelegateCommand(Cancel);


        }


    #region PROPERTIES
   
      
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
        
        
        private List<SwitchPortModel> _portDetails;
        public List<SwitchPortModel> PortDetails
        {
            get => _portDetails;
            set
            {
                _portDetails = value;
                RaisePropertyChanged(nameof(PortDetails));
            }
        }

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

        private void OpenCreatePort()
        {
            _commonViewService.ShowMessage(nameof(OpenCreatePort));
        }


        private void OpenUpdatePort(object parameter)
        {
            //var switchPortModel = parameter as SwitchPortModel;
            //SwitchPortModel = switchPortModel;
            _commonViewService.ShowMessage(nameof(OpenUpdatePort));
        }



        private void Cancel()
        {
            _commonViewService.ShowMessage(nameof(Cancel));
        }


        private async Task Search()
        {
            if (RadioButtonIP)
            {
                SwitchData = await _netAddressSearchRequestService.GetSwitchesByNetAddress(_token, SearchResponse);
            }
            else if (RadioButtonAddress)
            {
                SwitchData = await _addressSearchRequestService.GetSwitchesByAddress(_token, SearchResponse);
            }
            else if (RadioButtonEquipment)
            {
                SwitchData = await _equipmentSearchRequestService.GetSwitchesByEquipment(_token, SearchResponse);
            }
        }

        private void OnDetailsSwitchClicked(object switchData)
        {
            if (switchData is CoreSwitchModel coreSwitch)
            {
                var switchDetailsModel = GetCoreSwitchClient(coreSwitch);

                var detailsWindow = new DetailsSwitchWindow();
                //detailsWindow.DataContext = switchDetailsModel;
                _commonViewService.OpenWindow(detailsWindow, switchDetailsModel);
                //detailsWindow.ShowDialog();
            }
            else if (switchData is AggregationSwitchModel aggregationSwitch)
            {
                var switchDetailsModel = GetAggregationSwitchClient(aggregationSwitch);

                var detailsWindow = new DetailsSwitchWindow();
                detailsWindow.DataContext = switchDetailsModel;
                //_commonViewService.OpenWindow(detailsWindow, this);
                detailsWindow.ShowDialog();
            }
            else if (switchData is AccessSwitchModel accessSwitch)
            {
                var switchDetailsModel = GetAccessSwitchClient(accessSwitch);

                var detailsWindow = new DetailsSwitchWindow();
                detailsWindow.DataContext = switchDetailsModel;
                //_commonViewService.OpenWindow(detailsWindow, this);
                detailsWindow.ShowDialog();
            }
        }


        private SwitchDetailsBindable<CoreSwitchModel> GetCoreSwitchClient(CoreSwitchModel coreSwitch)
        {
            int switchId = coreSwitch.Id;
            var coreSwitchData = _coreSwitchRequestService.GetCoreSwitchById(_token, switchId);
            
            SwitchType SwitchType = SwitchType.Core;
            string ipGatewayData = coreSwitch.IPGateway;

            int equipmentId = Convert.ToInt32(coreSwitch.EquipmentManufacturerId);
            int addressId = Convert.ToInt32(coreSwitch.PostalAddressId);
            List<int>? portIds = coreSwitchData.SwitchPortIds;

            string equipmentManufacturerStr = GetEquipmentClient(equipmentId);
            string addressStr = GetPostalAddressClient(addressId);
            if (portIds != null)
            {
                List<SwitchPortModel> PortDetails = LoadPortDetails(portIds);
            }

            var switchDetailsModel = new SwitchDetailsBindable<CoreSwitchModel>
            {
                SwitchData = coreSwitchData,
                SwitchType = SwitchType,
                IPGateway = ipGatewayData,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr,
                Port = PortDetails,
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

            var aggregationSwitchData = _aggregationSwitchRequestService.GetAggregationSwitchById(_token, switchId);
            List<int>? portIds = aggregationSwitchData.SwitchPortIds;
            
            string addressStr = GetPostalAddressClient(addressId);
            string equipmentManufacturerStr = GetEquipmentClient(equipmentId);
            if(portIds != null)
            {
                List<SwitchPortModel> PortDetails = LoadPortDetails(portIds);
            }


            if (aggregationSwitch.CoreSwitchId != null)
                ipGatewayData = _coreSwitchRequestService.GetCoreSwitchById(_token, (int)aggregationSwitch.CoreSwitchId).IPAddress;

            var switchDetailsModel = new SwitchDetailsModel<AggregationSwitchModel>
            {
                SwitchData = aggregationSwitchData,
                IPGateway = ipGatewayData,
                SwitchType = SwitchType,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr,
                Port = PortDetails,
                
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

            var accessSwitchData = _accessSwitchRequestService.GetAccessSwitchById(_token, switchId);
            string equipmentManufacturerStr = GetEquipmentClient(equipmentId);
            string addressStr = GetPostalAddressClient(addressId);
            List<int>? portIds = accessSwitchData.SwitchPortIds;

            if (portIds != null)
            {
                List<SwitchPortModel> PortDetails = LoadPortDetails(portIds);
            }
            if (accessSwitch.AggregationSwitchId != null)
                ipGatewayData = _aggregationSwitchRequestService.GetAggregationSwitchById(_token, (int)accessSwitch.AggregationSwitchId).IPAddress;

            var switchDetailsModel = new SwitchDetailsModel<AccessSwitchModel>
            {
                SwitchData = accessSwitchData,
                IPGateway = ipGatewayData,
                SwitchType = SwitchType,
                PostalAddress = addressStr,
                Equipment = equipmentManufacturerStr,
                Port = PortDetails,
            };
            return switchDetailsModel;
        }

        private string GetPostalAddressClient(int addressId)
        {
            string addressStr = string.Empty;
            if (addressId != 0)
            {
                PostalAddressModel address = _postalAddressRequestService.GetPostalAddressById(_token, addressId);
                addressStr = $"{address.City}, {address.Street}, {address.Building}";
            }
            return addressStr;
        }

        private string GetEquipmentClient(int equipmentId)
        {
            string equipmentManufacturerStr = string.Empty;
            if (equipmentId != 0)
            {
                EquipmentManufacturerModel equipmentManufacturer = _equipmentRequestService.GetEquipmentById(_token, equipmentId);
                equipmentManufacturerStr = $"{equipmentManufacturer.Manufacturer}, {equipmentManufacturer.Model}";
            }
            return equipmentManufacturerStr;
        }

      
        public List<SwitchPortModel> LoadPortDetails(List<int> portIds)
        {
            PortDetails.Clear();
            List<SwitchPortModel> switchPortModels = new List<SwitchPortModel>();
            foreach (int portId in portIds)
            {
                SwitchPortModel switchPortModel = _switchPortRequestService.GetSwitchPortById(_token, portId);

                if (switchPortModel != null)
                {
                    PortDetails.Add(switchPortModel);
                }
            }
            return PortDetails;
        }


        #endregion
    }
}

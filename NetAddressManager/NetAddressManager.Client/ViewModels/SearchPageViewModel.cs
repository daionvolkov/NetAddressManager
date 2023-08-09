using GalaSoft.MvvmLight.Command;
using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Services.HandlerServices;
using NetAddressManager.Client.Views;
using NetAddressManager.Client.Views.AddWindows;
using NetAddressManager.ClientTests.Services;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NetAddressManager.Client.ViewModels
{
    public class SearchPageViewModel : BindableBase
    {
        private AuthToken _token;
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

        private EquipmentHandlerService _equipmentHandlerService;
        private SwitchPortHandlerService _switchPortHandlerService; 
        private PostalAddressHandlerService _postalAddressHandlerService;
        private CoreSwitchHandlerService _coreSwitchHandlerService;  
        private AggregationSwitchHandlerService _aggregationSwitchHandlerService;
        private AccessSwitchHandlerService _accessSwitchHandlerService;
            
        
        private DetailsSwitchWindowViewModel _detailsSwitchWindowViewModel;



       

        #region COMMANDS
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<object> DetailsSwitchCommand { get; private set; }
        public DelegateCommand<object> OpenUpdateSwitchCommand { get; private set; }
        public DelegateCommand<object> OpenUpdatePortCommand { get; }
        public DelegateCommand OpenCreatePortCommand { get; }
        public DelegateCommand SavePortCommand { get; private set; }

        #endregion

        public SearchPageViewModel(AuthToken token)
        {
            _token = token;

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
            _equipmentHandlerService = new EquipmentHandlerService(_token);
            _switchPortHandlerService = new SwitchPortHandlerService(_token);
            _postalAddressHandlerService = new PostalAddressHandlerService(_token);
            _coreSwitchHandlerService = new CoreSwitchHandlerService(_token);
            _aggregationSwitchHandlerService = new AggregationSwitchHandlerService(_token);
            _accessSwitchHandlerService = new AccessSwitchHandlerService(_token);

            PortDetails = new List<SwitchPortModel>();
            SearchCommand = new DelegateCommand(async () => await Search());
            DetailsSwitchCommand = new DelegateCommand<object>(OnDetailsSwitchClicked);
            OpenUpdateSwitchCommand = new DelegateCommand<object>(OpenUpdateSwitch);
            SavePortCommand = new DelegateCommand(SavePort);

            OpenCreatePortCommand = new DelegateCommand(OpenCreatePort);
            OpenUpdatePortCommand = new DelegateCommand<object>(OpenUpdatePort);



            _detailsSwitchWindowViewModel = new DetailsSwitchWindowViewModel();



        }

        #region PROPERTIES



        private RelayCommand<object> _saveUpdateSwitchCommand;
        public ICommand SaveUpdateSwitchCommand
        {
            get
            {
                if (_saveUpdateSwitchCommand == null)
                {
                    _saveUpdateSwitchCommand = new RelayCommand<object>(SaveUpdateSwitch);
                }
                return _saveUpdateSwitchCommand;
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
            get => _radioButtonIP;
            set { SetProperty(ref _radioButtonIP, value); }
        }
        private bool _radioButtonAddress;
        public bool RadioButtonAddress
        {
            get => _radioButtonAddress;
            set { SetProperty(ref _radioButtonAddress, value); }
        }
        private bool _radioButtonEquipment;
        public bool RadioButtonEquipment
        {
            get => _radioButtonEquipment;
            set { SetProperty(ref _radioButtonEquipment, value); }
        }


        private SwitchDataModel _switchData;
        public SwitchDataModel SwitchData
        {
            get => _switchData;
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


        private SwitchDetailsModel _switchDetailsModel;
        public SwitchDetailsModel SwitchDetailsModel
        {
            get { return _switchDetailsModel; }
            set { _switchDetailsModel = value; }
        }

        private SwitchPortModel _selectedPort;
        public SwitchPortModel SelectedPort
        {
            get =>_selectedPort; 
            set { 
                _selectedPort = value;
                RaisePropertyChanged(nameof(SwitchPortModel));
            }
        }

        private SwitchPortAction _portAction;
        public SwitchPortAction PortAction
        {
            get => _portAction;
            set { _portAction = value; }
        }

        private bool isPortReadOnly;
        public bool IsPortReadOnly
        {
            get { return isPortReadOnly; }
            set
            {
                isPortReadOnly = value;
            }
        }
        #endregion

        #region METHODS


        

        private void OpenCreatePort()
        {
            SelectedPort = new SwitchPortModel();
            PortAction = SwitchPortAction.Create;
            IsPortReadOnly = false;
            var portWindow = new CreateOrUpdatePort();
            _commonViewService.OpenWindow(portWindow, this);
        }


        private void OpenUpdatePort(object portId)
        {
            int id = (int)portId;
            PortAction = SwitchPortAction.Update;
            IsPortReadOnly = true;
            SelectedPort = _switchPortRequestService.GetSwitchPortById(_token, id);
            var portWindow = new CreateOrUpdatePort();
            _commonViewService.OpenWindow(portWindow, this);
        }

       private void SavePort()
        {
            if (PortAction == SwitchPortAction.Update)
            {
                _switchPortHandlerService.UpdatePortClient(SelectedPort);
            }
            if (PortAction == SwitchPortAction.Create)
            {
                _switchPortHandlerService.CreatePortClient(SelectedPort, SwitchDetailsModel);
            }
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
                SwitchDetailsModel = _coreSwitchHandlerService.GetCoreSwitchClient(coreSwitch);
                var detailsWindow = new DetailsSwitchWindow();
                _commonViewService.OpenWindow(detailsWindow, this);
            }
            else if (switchData is AggregationSwitchModel aggregationSwitch)
            {
                SwitchDetailsModel = _aggregationSwitchHandlerService.GetAggregationSwitchClient(aggregationSwitch);  
                var detailsWindow = new DetailsSwitchWindow();
                _commonViewService.OpenWindow(detailsWindow, this);
            }
            else if (switchData is AccessSwitchModel accessSwitch)
            {
                SwitchDetailsModel = _accessSwitchHandlerService.GetAccessSwitchClient(accessSwitch);
                var detailsWindow = new DetailsSwitchWindow();
                _commonViewService.OpenWindow(detailsWindow, this);
            }
        }

        private void OpenUpdateSwitch(object switchData)
        {
            if (switchData is CoreSwitchModel coreSwitch)
            {
                SwitchDetailsModel = _coreSwitchHandlerService.GetCoreSwitchClient(coreSwitch);
                var detailsWindow = new UpdateSwitchWindow();
                _commonViewService.OpenWindow(detailsWindow, this);
            }

            if (switchData is AggregationSwitchModel aggregationSwitch)
            {
                SwitchDetailsModel = _aggregationSwitchHandlerService.GetAggregationSwitchClient(aggregationSwitch);
                var detailsWindow = new UpdateSwitchWindow();
                _commonViewService.OpenWindow(detailsWindow, this);
            }
            if (switchData is AccessSwitchModel accessSwitch)
            {
                SwitchDetailsModel = _accessSwitchHandlerService.GetAccessSwitchClient(accessSwitch);
                var detailsWindow = new UpdateSwitchWindow();
                _commonViewService.OpenWindow(detailsWindow, this);
            }
        }

        

        private void SaveUpdateSwitch(object switchDetailsModel)
        {
            var updateSwitchWindowViewModel = new UpdateSwitchWindowViewModel(_token);
            updateSwitchWindowViewModel.SaveUpdateSwitch(switchDetailsModel);
        }


        #endregion
    }
}

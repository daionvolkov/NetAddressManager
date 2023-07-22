using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
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

namespace NetAddressManager.Client.ViewModels
{
    public class SearchPageViewModel : BindableBase
    {
        private CommonViewService _commonViewService;
        private NetAddressSearchRequestService _netAddressSearchRequestService;
        private AddressSearchRequestService _addressSearchRequestService;
        private EquipmentSearchRequestService _equipmentSearchRequestService;

        #region COMMANDS
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand DetailsSwitchCommand { get; private set; }

        #endregion

        public SearchPageViewModel(AuthToken token)
        {
            Token = token;
            _commonViewService = new CommonViewService();
            _netAddressSearchRequestService = new NetAddressSearchRequestService();
            _addressSearchRequestService = new AddressSearchRequestService();
            _equipmentSearchRequestService = new EquipmentSearchRequestService();
            
            SearchCommand = new DelegateCommand(async () => await Search());
            DetailsSwitchCommand = new DelegateCommand(DetailsSwitch);

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

        private void DetailsSwitch()
        {
            MessageBox.Show("sdfsdfsdf");
        }

        #endregion
    }
}

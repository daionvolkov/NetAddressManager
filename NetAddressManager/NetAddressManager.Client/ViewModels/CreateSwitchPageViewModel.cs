using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace NetAddressManager.Client.ViewModels
{
    public class CreateSwitchPageViewModel :BindableBase
    {
        private AuthToken _token;
        private CommonViewService _commonViewService;
        private CoreSwitchRequestService _coreSwitchRequestService;
        private AggregationSwitchRequestService _aggregationSwitchRequestService;
        private AccessSwitchRequestService _accessSwitchRequestService;

        #region COMMAND
        public DelegateCommand SaveSwitchCommand { get; private set; }
        public DelegateCommand ClearSwitchCommand { get; private set; }

        public CreateSwitchPageViewModel(AuthToken token)
        {
            _token = token;
            _commonViewService = new CommonViewService();
            _coreSwitchRequestService = new CoreSwitchRequestService();
            _aggregationSwitchRequestService = new AggregationSwitchRequestService();
            _accessSwitchRequestService = new AccessSwitchRequestService();

            SaveSwitchCommand = new DelegateCommand(SaveSwitch);
            ClearSwitchCommand = new DelegateCommand(ClearSwitch);

            IsIPGatewayVisible = true;
        }

        #endregion



        #region PROPERTIES

        public CommonSwitch CommonModel { get; set; } = new CommonSwitch();
        public CoreSwitchModel CoreSwitchModel { get; set; } = new CoreSwitchModel();

        private bool _isIPGatewayVisible;
        public bool IsIPGatewayVisible
        {
            get { return _isIPGatewayVisible && RadioButtonCore; }
            set
            {
                if (_isIPGatewayVisible != value)
                {
                    _isIPGatewayVisible = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _radioButtonCore;
        public bool RadioButtonCore
        {
            get =>_radioButtonCore;
            set
            {
                if (_radioButtonCore != value)
                {
                    _radioButtonCore = value;
                    RaisePropertyChanged(); 
                    RaisePropertyChanged(nameof(IsIPGatewayVisible));
                }
            }
        }

        private bool _radioButtonAggregation;
        public bool RadioButtonAggregation
        {
            get => _radioButtonAggregation;
            set { _radioButtonAggregation = value; }
        }


        private bool _radioButtonAccess;
        public bool RadioButtonAccess
        {
            get => _radioButtonAccess;
            set { _radioButtonAccess = value; }
        }

        #endregion


        #region METHODS

        private void SaveSwitch() 
        {
            if (RadioButtonCore)
            {
                CreateCoreSwitchClient();
            }
            if (RadioButtonAggregation)
            {
                CreateAggregationSwitchClient();
            }
            if (RadioButtonAccess)
            {
                CreateAccessSwitchClient();
            }
        }


        private void CreateCoreSwitchClient()
        {
            var coreSwitchModel = new CoreSwitchModel(CommonModel.IPAddress, CommonModel.IPMask, CommonModel.MACAddress, CommonModel.Description, CommonModel.IPGateway);
            var resultAction = _coreSwitchRequestService.CreateCoreSwitch(_token, coreSwitchModel);
            _commonViewService.ShowActionRelust(resultAction, "Коммутатор создан");
        }

        private void CreateAggregationSwitchClient()
        {
            var aggregationSwitchModel = new AggregationSwitchModel(CommonModel.IPAddress, CommonModel.IPMask, CommonModel.MACAddress, CommonModel.Description);
            var resultAction = _aggregationSwitchRequestService.CreateAggregationSwitch(_token, aggregationSwitchModel);
            _commonViewService.ShowActionRelust(resultAction, "Коммутатор создан");
        }

        private void CreateAccessSwitchClient()
        {
            var accessSwitchModel = new AccessSwitchModel(CommonModel.IPAddress, CommonModel.IPMask, CommonModel.MACAddress, CommonModel.Description);
            var resultAction = _accessSwitchRequestService.CreateAccessSwitch(_token, accessSwitchModel);
            _commonViewService.ShowActionRelust(resultAction, "Коммутатор создан");
        }


        private void ClearSwitch() 
        {
            CommonModel = new CommonSwitch();
        }

        #endregion
    }
}

using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views;
using NetAddressManager.Models;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace NetAddressManager.Client.ViewModels
{
    public class DetailsSwitchWindowViewModel : BindableBase
    {
        private AuthToken _token;
        private CommonViewService _commonViewService;
        private Window _currentWindow;
        

        #region COMMANDS

        /*public DelegateCommand<object> OpenUpdatePortCommand { get;}
        public DelegateCommand OpenCreatePortCommand { get; }
        public DelegateCommand CancelCommand { get; }*/

        #endregion


        public DetailsSwitchWindowViewModel()
        {
            //_token = token;
            _commonViewService = new CommonViewService();

           /* OpenCreatePortCommand = new DelegateCommand(OpenCreatePort);

            OpenUpdatePortCommand = new DelegateCommand<object>(OpenUpdatePort);
         
            CancelCommand = new DelegateCommand(Cancel);*/
        }


        #region PROPERTIES
        private SwitchPortModel _switchPortModel;
        public SwitchPortModel SwitchPortModel
        {
            get=> _switchPortModel;
            set
            {
                _switchPortModel = value;
                RaisePropertyChanged(nameof(SwitchPortModel));
            }
        }

        #endregion



        #region METHODS

        /*private void OpenCreatePort()
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
        */

        #endregion

    }
}

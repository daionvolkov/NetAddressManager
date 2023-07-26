using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetAddressManager.Client.ViewModels
{
    public class CreatePostalAddressPageViewModel : BindableBase
    {

        private AuthToken _token;
        private PostalAddressRequestService _postalAddressRequestService;
        private CommonViewService _commonViewService;

        #region COMMANDS

        public DelegateCommand SavePostalAddressCommand { get; private set; }
        public DelegateCommand ClearPostalAddressCommand { get; private set; }
        

        #endregion

        public CreatePostalAddressPageViewModel(AuthToken token)
        {
            _token = token;
            _postalAddressRequestService = new PostalAddressRequestService();
            _commonViewService = new CommonViewService();
            
            PostalAddressModel = new PostalAddressModel();

            SavePostalAddressCommand = new DelegateCommand(SavePostalAddress);
            ClearPostalAddressCommand = new DelegateCommand(ClearPostalAddress);
        }

        

        #region PROPERTIES

        private PostalAddressModel _postalAddressModel;

        public PostalAddressModel PostalAddressModel
        {
            get =>_postalAddressModel; 
            set { 
                _postalAddressModel = value; 
                RaisePropertyChanged(nameof(PostalAddressModel));
            }
        }


        #endregion

        #region METHODS
        private void SavePostalAddress() 
        {
            var result = _postalAddressRequestService.CreatePostalAddress(_token, PostalAddressModel);
            _commonViewService.ShowActionRelust(result, "Адресс добавлен");
        }

        private void ClearPostalAddress() {
            PostalAddressModel = new PostalAddressModel();
        }

        #endregion
    }
}

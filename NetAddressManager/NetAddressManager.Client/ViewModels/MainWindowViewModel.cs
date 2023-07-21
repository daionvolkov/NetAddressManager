using NetAddressManager.Client.Models;
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

namespace NetAddressManager.Client.ViewModels
{
    class MainWindowViewModel : BindableBase
    {

        #region COMMANDS

        public DelegateCommand OpenSearchPageCommand { get; private set; }
        public DelegateCommand OpenCreatePageCommand { get; private set; }
        public DelegateCommand OpenUserInfoPageCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }


        #endregion


        public MainWindowViewModel()
        {
            OpenSearchPageCommand = new DelegateCommand(OpenSearchPage);
            OpenCreatePageCommand = new DelegateCommand(OpenCreatePage);
            OpenUserInfoPageCommand = new DelegateCommand(OpenUserInfoPage);
            LogoutCommand = new DelegateCommand(Logout);

        }

        #region PROPERTIES
        private AuthToken _token;
        public AuthToken Token
        {
            get => _token; 
            private set
            {
                _token = value;
                RaisePropertyChanged(nameof(AuthToken));
            }
        }

        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }



        #endregion

        #region METHODS

        private void OpenSearchPage()
        {
            ShowMessage("SearchPage");
        }

        private void OpenCreatePage()
        {
            ShowMessage("CreatePage");
        }

        private void OpenUserInfoPage() 
        {
            ShowMessage("UserInfoPage");
        }
        private void Logout()
        {
            ShowMessage("Logout");

        }


        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        #endregion

    }

}
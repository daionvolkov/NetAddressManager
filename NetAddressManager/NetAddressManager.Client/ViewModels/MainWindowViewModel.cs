using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views.Pages;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NetAddressManager.Client.ViewModels
{
    class MainWindowViewModel : BindableBase
    {

        private CommonViewService _commonViewService;

        #region COMMANDS

        public DelegateCommand OpenSearchPageCommand { get; private set; }
        public DelegateCommand OpenCreateSwitchPageCommand { get; private set; }
        public DelegateCommand OpenCreateAddressPageCommand { get; private set; }
        public DelegateCommand OpenCreateEquipmentPageCommand { get; private set; }
        public DelegateCommand OpenUserInfoPageCommand { get; private set; }
        public DelegateCommand OpenUsersManagementCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }


        #endregion


        public MainWindowViewModel(AuthToken token, UserModel currentUser, Window currentWindow = null)
        {
            Token = token;
            CurrentUser = currentUser;
            _commonViewService = new CommonViewService();

            OpenSearchPageCommand = new DelegateCommand(OpenSearchPage);
            NavButtons.Add(_serchBtnName, OpenSearchPageCommand);
         
            OpenUserInfoPageCommand = new DelegateCommand(OpenUserInfoPage);
            NavButtons.Add(_userInfoBtnName, OpenUserInfoPageCommand);
            
            if (CurrentUser.Status == UserStatus.Admin)
            {
                OpenUsersManagementCommand = new DelegateCommand(OpenUsersManagement);
                NavButtons.Add(_manageUsersBtnName, OpenUsersManagementCommand);

                OpenCreateSwitchPageCommand = new DelegateCommand(OpenCreateSwitchPage);
                NavButtons.Add(_createSwitchBtnName, OpenCreateSwitchPageCommand);
                
                OpenCreateAddressPageCommand = new DelegateCommand(OpenCreateAddressPage);
                NavButtons.Add(_createAddressBtnName, OpenCreateAddressPageCommand);

                OpenCreateEquipmentPageCommand = new DelegateCommand(OpenCreateEquipmentPage);
                NavButtons.Add(_createEquipmentBtnName, OpenCreateEquipmentPageCommand);

            }

            LogoutCommand = new DelegateCommand(Logout);
            NavButtons.Add(_logoutBtnName, LogoutCommand);

        }

        #region PROPERTIES

        private readonly string _serchBtnName = "Search";
        private readonly string _createSwitchBtnName = "Create Switch";
        private readonly string _createAddressBtnName = "Create Address";
        private readonly string _createEquipmentBtnName = "Create Equipment";
        private readonly string _userInfoBtnName = "User Info";
        
        private readonly string _usersBtnName = "Create Equipment";
        private readonly string _manageUsersBtnName = "Users";

        private readonly string _logoutBtnName = "Logout";



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

        private Dictionary<string, DelegateCommand> _navButtons = new Dictionary<string, DelegateCommand>();
        public Dictionary<string, DelegateCommand> NavButtons
        {
            get { return _navButtons; }
            set
            {
                _navButtons = value;
                RaisePropertyChanged(nameof(NavButtons));
            }
        }


        private Page _selectedPage;

        public Page SelectedPage
        {
            get =>_selectedPage; 
            set { 
                _selectedPage = value;
                RaisePropertyChanged(nameof(SelectedPage));
            }
        }
        private string _selectedPageName;

        public string SelectedPageName
        {
            get => _selectedPageName; 
            set { 
                _selectedPageName = value;
                RaisePropertyChanged(nameof(SelectedPageName));
            }
        }

        #endregion

        #region METHODS

        private void OpenSearchPage()
        {
            string pageName = "Страница поиска";
            var page = new SearchPage();
            OpenPage(page, pageName, new SearchPageViewModel(Token));
        }

        private void OpenCreateSwitchPage()
        {
            SelectedPageName = "Добавить коммутатор";
            _commonViewService.ShowMessage("CreatePage");
        }

        private void OpenCreateAddressPage()
        {
            SelectedPageName = "Добавить адрес";
            _commonViewService.ShowMessage("CreatePage");
        }
        private void OpenCreateEquipmentPage()
        {
            SelectedPageName = "Добавить оборудование";
            _commonViewService.ShowMessage("CreatePage");
        }
        private void OpenUserInfoPage() 
        {
            var page = new UserInfoPage();
            string pageName = "Личный кабинет";
            OpenPage(page, pageName, this);
        }

        private void OpenUsersManagement()
        {

        }

        private void Logout()
        {
            _commonViewService.ShowMessage("Logout");
        }




        public void OpenPage(Page page, string pageName, BindableBase viewModel)
        {
            SelectedPageName = pageName;
            SelectedPage = page;
            SelectedPage.DataContext = viewModel;
        }


        #endregion

    }

}
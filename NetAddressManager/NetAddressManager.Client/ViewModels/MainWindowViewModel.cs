using NetAddressManager.Api.Models.Enums;
using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views;
using NetAddressManager.Client.Views.Pages;
using NetAddressManager.Client.Views.Pages.AddPages;
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
        private Window _currentWindow;

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
            _currentWindow = currentWindow;
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

        private readonly string _serchBtnName = "Поиск";
        private readonly string _createSwitchBtnName = "Добавить коммутатор";
        private readonly string _createAddressBtnName = "Добавить адрес";
        private readonly string _createEquipmentBtnName = "Добавить оборудование";
        private readonly string _userInfoBtnName = "Личный кабинет";
        private readonly string _usersBtnName = "Добавить оборудование";
        private readonly string _manageUsersBtnName = "Пользователи";
        private readonly string _logoutBtnName = "Выйти";


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
            string pageName = "Добавить коммутатор";
            var page = new CreateSwitchPage();
            OpenPage(page, pageName, new CreateSwitchPageViewModel(Token));
        }

        private void OpenCreateAddressPage()
        {
            string pageName = "Добавить почтовый адрес";
            var page = new CreatePostalAddressPage();
            OpenPage(page, pageName, new CreatePostalAddressPageViewModel(Token));
        }
        private void OpenCreateEquipmentPage()
        {
            string pageName = "Добавить оборудование";
            var page = new CreateEquipmentManufacturerPage();
            OpenPage(page, pageName, new CreateEquipmentManufacturerPageViewModel(Token));
        }
        private void OpenUserInfoPage() 
        {
            var page = new UserInfoPage();
            string pageName = "Личный кабинет";
            OpenPage(page, pageName, this);
        }

        private void OpenUsersManagement()
        {
            string pageName = "Поиск пользователей";
            var page = new UsersPage();
            OpenPage(page, pageName, new UsersPageViewModel(Token));

        }

        private void Logout()
        {
            var question = MessageBox.Show("Вы уверены?", "Logout", MessageBoxButton.YesNo);
            if(question == MessageBoxResult.Yes && _currentUser != null) 
            {
                Login login = new Login();
                login.Show();
                _currentWindow.Close(); 
            }
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
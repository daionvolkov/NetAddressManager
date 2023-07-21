using NetAddressManager.Client.Models;
using NetAddressManager.Client.Views.Pages;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Controls;

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


        public MainWindowViewModel(AuthToken token, UserModel currentUser, Window currentWindow = null)
        {

            Token = token;
            CurrentUser = currentUser;


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
            OpenPage(page, pageName);
        }

        private void OpenCreatePage()
        {
            SelectedPageName = "Добавить оборудование";
            ShowMessage("CreatePage");
        }

        private void OpenUserInfoPage() 
        {
            var page = new UserInfoPage();
            string pageName = "Личный кабинет";
            page.DataContext = this;
            OpenPage(page, pageName);
        }
        private void Logout()
        {
            ShowMessage("Logout");

        }


        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void OpenPage(Page page, string pageName)
        {
            SelectedPageName = pageName;
            SelectedPage = page;
        }

        #endregion

    }

}
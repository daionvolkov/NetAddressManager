using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views.AddWindows;
using NetAddressManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace NetAddressManager.Client.ViewModels
{
    public class UsersPageViewModel : BindableBase
    {
        private AuthToken _token;
        private CommonViewService _commonViewService;
        private UserRequestService _userRequestService;
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand OpenAddNewUserCommand { get; private set; }
        public DelegateCommand<object> OpenUpdateUserCommand { get; private set; }

        public DelegateCommand<object> DeleteUserCommand { get; private set; }
        public DelegateCommand SaveUserCommand { get; private set; }
        


        public UsersPageViewModel(AuthToken token)
        {
            _token = token;
            _commonViewService = new CommonViewService();
            _userRequestService = new UserRequestService();
            SelectedUser = this.selectedUser;

            SearchCommand = new DelegateCommand(Search);
            OpenAddNewUserCommand = new DelegateCommand(OpenAddNewUser);
            OpenUpdateUserCommand = new DelegateCommand<object>(OpenUpdateUser);
            DeleteUserCommand = new DelegateCommand<object>(DeleteUser);
            SaveUserCommand = new DelegateCommand(SaveUser);


        }


        #region COMMANDS

        #endregion


        #region PROPERTIES

        private ObservableCollection<UserModel> selectedUser = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> SelectedUser
        {
            get => selectedUser; 
            set { SetProperty(ref selectedUser, value); }
        }

        private string _searchResponse;
        public string SearchResponse
        {
            get => _searchResponse;
            set { 
                _searchResponse = value;
                RaisePropertyChanged(nameof(SearchResponse));
            }
        }

        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }

        private UserAction _userClientAction;

        public UserAction UserClientAction
        {
            get =>_userClientAction; 
            set { 
                _userClientAction = value;
                RaisePropertyChanged(nameof(UserClientAction));
            }
        }

        #endregion



        #region METHODS
        private void Search()
        {
            UserModel user = _userRequestService.GetUserByEmail(_token, _searchResponse);
            SelectedUser.Clear();
            if (user != null)
            {
                SelectedUser.Add(user);
            }
        }

        

        private void SaveUser()
        {
            if(UserClientAction == UserAction.Create)
            {
                var resultAction = _userRequestService.CreateUser(_token, CurrentUser);
                _commonViewService.ShowActionRelust(resultAction, "Пользовтель создан");
            }
            else if(UserClientAction == UserAction.Update)
            {
                var resultAction = _userRequestService.UpdateUser(_token, CurrentUser);
                _commonViewService.ShowActionRelust(resultAction, "Пользовтель обновлен");
            }
        }


        private void OpenAddNewUser()
        {
            CurrentUser = new UserModel();
            UserClientAction = UserAction.Create;
            var updateUserWindow = new CreateOrUpdateUserWindow();
            _commonViewService.OpenWindow(updateUserWindow, this);

        }

        private void OpenUpdateUser(object userData)
        {
            var updateUser = userData as UserModel;
            UserClientAction = UserAction.Update;
            CurrentUser = _userRequestService.GetUserById(_token, (int)updateUser.Id);
            var updateUserWindow = new CreateOrUpdateUserWindow();
            updateUserWindow.DataContext = CurrentUser;
            _commonViewService.OpenWindow(updateUserWindow, this);
        }

        private void DeleteUser(object userId)
        {
            UserClientAction = UserAction.Delete;
            int id = (int)userId;
            var deleteResult =  MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Удаление", MessageBoxButton.YesNo);
            if (deleteResult == MessageBoxResult.Yes)
            {
                var resultAction = _userRequestService.DeleteUser(_token, id);
                _commonViewService.ShowActionRelust(resultAction, "Пользовтель удален");
            }

        } 

        #endregion

    }
}

using NetAddressManager.Client.Models;
using NetAddressManager.Client.Services;
using NetAddressManager.Client.Views;
using NetAddressManager.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetAddressManager.Client.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        #region COMMANDS

        public DelegateCommand<object> GetUserFromDBCommand { get; private set; }
        public DelegateCommand<object> LoginFromCacheCommand { get; private set; }

        #endregion

        public LoginViewModel()
        {
            _userRequestService = new UserRequestService();
            CurrentUserCache = GetUserCache();
            GetUserFromDBCommand = new DelegateCommand<object>(GetUserFromDB);
            LoginFromCacheCommand = new DelegateCommand<object>(LoginFromCache);    
        }

        #region PROPERTIES

        private UserRequestService _userRequestService;
        private string _cachePath = Path.GetTempPath() + "useraccount.txt";
        private Window _currentWnd;

        public string UserLogin { get; set; }
        public string UserPassword { get; private set; }

        private UserCache _currentUserCache;
        public UserCache CurrentUserCache
        {
            get => _currentUserCache; 
            set 
            { 
                _currentUserCache = value; 
                RaisePropertyChanged(nameof(CurrentUserCache));
            }
        }


        private AuthToken _authToken;
        public AuthToken AuthToken
        {
            get => _authToken;
            set
            {
                _authToken = value;
                RaisePropertyChanged(nameof(AuthToken));
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

        #endregion


        #region METHODS
        private void GetUserFromDB(object parameter)
        {
            var passBox = parameter as PasswordBox;
            _currentWnd = Window.GetWindow(passBox);
            bool isNewUser = false;
            if(UserLogin != CurrentUserCache?.Login || UserPassword != CurrentUserCache?.Password)
            {
                isNewUser = true;
            }

            UserPassword = passBox.Password;
            AuthToken = _userRequestService.GetToken(UserLogin, UserPassword);
            if (AuthToken == null)
            {
                return;
            }

            CurrentUser = _userRequestService.GetCurrentUser(AuthToken);
            if (CurrentUser != null)
            {
                if (isNewUser)
                {
                    var saveUserCacheMessage = MessageBox.Show("Сохранить логин и пароль?", "Сохранить", MessageBoxButton.YesNo);
                    if (saveUserCacheMessage == MessageBoxResult.Yes)
                    {
                        UserCache newUserCache = new UserCache
                        {
                            Login = UserLogin,
                            Password = UserPassword,
                        };
                        CreateUserCache(newUserCache);
                    }
                }
                OpenMainWIndow();
            }
        }

        private void CreateUserCache(UserCache userCache)
        {
            string userCacheJson = JsonConvert.SerializeObject(userCache);
            using(StreamWriter sw = new StreamWriter(_cachePath, false, Encoding.Default))
            {
                sw.Write(userCacheJson);
                MessageBox.Show("Готово");
            }
        } 

        private UserCache GetUserCache()
        {
            bool isCacheExists = File.Exists(_cachePath);
            if (isCacheExists && File.ReadAllText(_cachePath).Length > 0)
            {
                return JsonConvert.DeserializeObject<UserCache>(File.ReadAllText(_cachePath));
            }
            return null;
        }

        private void LoginFromCache(object wnd) {

            _currentWnd = wnd as Window;
            UserLogin = CurrentUserCache.Login;
            UserPassword = CurrentUserCache.Password;

            AuthToken = _userRequestService.GetToken(UserLogin, UserPassword);
            CurrentUser = _userRequestService.GetCurrentUser(AuthToken);
            if(CurrentUser != null)
            {
                OpenMainWIndow();
            }
        }

        private void OpenMainWIndow() {
            MainWindow window = new MainWindow();
            window.DataContext = new MainWindowViewModel(AuthToken, CurrentUser, window);
            window.Show();
            _currentWnd.Close();
        }
        #endregion


    }
}

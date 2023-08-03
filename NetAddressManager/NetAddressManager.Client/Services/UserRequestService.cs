using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services
{
    public class UserRequestService : CommonRequestService
    {
        
        private string _usersControllerUrl = HOST + "user";

        public AuthToken GetToken(string userName, string password)
        {
            string url = HOST + "account/token";
            string resultStr = GetDataByUrl(HttpMethod.Post, url, null, userName, password);
            AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr);
            return token;
        }


        public HttpStatusCode CreateUser(AuthToken token, UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            var result = SendDataByUrl(HttpMethod.Post, _usersControllerUrl, token, userJson);
            return result;
        }

        public HttpStatusCode UpdateUser(AuthToken token, UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            var result = SendDataByUrl(HttpMethod.Patch, _usersControllerUrl + $"/{user.Id}", token, userJson);
            return result;
        }

        public List<UserModel> GetAllUsers(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _usersControllerUrl, token);
            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(response) ?? new List<UserModel>();
            return users;
        }

        public UserModel GetCurrentUser(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, HOST+"account/info", token);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(response) ?? new UserModel();
            return user;
        }

        public UserModel GetUserByEmail(AuthToken token, string email)
        {
            string response = GetDataByUrl(HttpMethod.Get, _usersControllerUrl + $"/{email}/login", token);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(response) ?? new UserModel();
            return user;
        }

        public UserModel GetUserById(AuthToken token, int id)
        {
            string response = GetDataByUrl(HttpMethod.Get, _usersControllerUrl + $"/{id}", token);
            UserModel user = JsonConvert.DeserializeObject<UserModel>(response) ?? new UserModel();
            return user;
        }


        public HttpStatusCode DeleteUser(AuthToken token, int userId)
        {
            var result = DeleteDataByUrl(_usersControllerUrl + $"/{userId}", token);
            return result;
        }
    }
}

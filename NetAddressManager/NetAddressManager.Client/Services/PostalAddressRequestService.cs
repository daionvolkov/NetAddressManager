using NetAddressManager.Client.Models;
using NetAddressManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services
{
    public class PostalAddressRequestService : CommonRequestService
    {
        private string _postalAddressControllerUrl = HOST + "postaladdress";

        public List<PostalAddressModel> GetAllPostalAddresses(AuthToken token)
        {
            string response = GetDataByUrl(HttpMethod.Get, _postalAddressControllerUrl, token);
            List<PostalAddressModel> postalAddresses = JsonConvert.DeserializeObject<List<PostalAddressModel>>(response) ?? new List<PostalAddressModel>();
            return postalAddresses;
        }

        public PostalAddressModel GetPostalAddressById(AuthToken token, int postalAddressId)
        {
            var response = GetDataByUrl(HttpMethod.Get, _postalAddressControllerUrl + $"/{postalAddressId}", token);
            PostalAddressModel postalAddress = JsonConvert.DeserializeObject<PostalAddressModel>(response) ?? new PostalAddressModel();
            return postalAddress;
        }

        public HttpStatusCode CreatePostalAddress(AuthToken token, PostalAddressModel postalAddress)
        {
            string postalAddressJson = JsonConvert.SerializeObject(postalAddress);
            var result = SendDataByUrl(HttpMethod.Post, _postalAddressControllerUrl, token, postalAddressJson);
            return result;
        }

        public HttpStatusCode UpdatePostalAddress(AuthToken token, PostalAddressModel postalAddress)
        {
            string postalAddressJson = JsonConvert.SerializeObject(postalAddress);
            var result = SendDataByUrl(HttpMethod.Patch, _postalAddressControllerUrl + $"/{postalAddress.Id}", token, postalAddressJson);
            return result;
        }

        public HttpStatusCode DeletePostalAddress(AuthToken token, int postalAddressId)
        {
            var result = DeleteDataByUrl(_postalAddressControllerUrl + $"/{postalAddressId}", token);
            return result;
        }
    }
}

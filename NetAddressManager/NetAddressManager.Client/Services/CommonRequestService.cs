﻿using NetAddressManager.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetAddressManager.Client.Services
{
    public abstract class CommonRequestService
    {


        public const string HOST = "https://localhost:7033/api/";
        //public const string HOST = "http://addressmanagerapi.somee.com/api/";


        protected string GetDataByUrl(HttpMethod method, string url, AuthToken token, string userName = null, string password = null)
        {
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = method.Method;

            if (userName != null && password != null)
            {
                string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
                request.Headers.Add("Authorization", "Basic " + encoded);


            }
            else if (token != null)
            {
                request.Headers.Add("Authorization", "Bearer " + token.access_token);
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseStr = reader.ReadToEnd();
                    result = responseStr;
                }

                return result;
            }
            catch (Exception ) {
                return string.Empty;
            }
        }

        protected HttpStatusCode SendDataByUrl(HttpMethod method, string url, AuthToken token, string data)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);

            var content = new StringContent(data, Encoding.UTF8, "application/json");
            if (method == HttpMethod.Post)
            {
                result = client.PostAsync(url, content).Result;
            }
            if (method == HttpMethod.Patch)
            {
                result = client.PatchAsync(url, content).Result;
            }

            return result.StatusCode;
        }


        protected HttpStatusCode DeleteDataByUrl(string url, AuthToken token)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);
            result = client.DeleteAsync(url).Result;

            return result.StatusCode;
        }
    }
}

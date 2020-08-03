using GeletaApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GeletaApp.Logic
{
    public class UserAPI_Logic
    {
        public async static Task<List<object>> GetToken(string userEmail, string userPassword)
        {
            User user = new User();
            List<object> userx = new List<object>();

            var url = DataRoot.GenerateURL("login");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    email = userEmail,
                    password = userPassword
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var userRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                if (userRoot.user_data != null)
                {
                    user = userRoot.user_data as User;
                    user.email = userEmail;
                    userx.Add("Token");
                    userx.Add(user);
                }
                else
                {
                    userx.Add("Message");
                    userx.Add(userRoot.message);
                }
            }
            return userx;
        }
        public async static Task<List<string>> RegisterUser(string userEmail, string userPassword)
        {
            // User user = new User();
            List<string> userx = new List<string>();

            var url = DataRoot.GenerateURL("register");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    email = userEmail,
                    password = userPassword
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var userRoot = JsonConvert.DeserializeObject<DataRoot>(json);

                if (userRoot.userToken != null)
                {
                   // user = userRoot.user_data as User;
                    userx.Add("Token");
                    userx.Add(userRoot.userToken);
                }
                else
                {
                    userx.Add("Message");
                    userx.Add(userRoot.message);
                }
            }
            return userx;
        }
        public async static Task<string> EditUserData(string tokenC, User user)
        {
            var rez = "";
            var url = DataRoot.GenerateURL("editUser");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    token = tokenC,
                    address = user.address,
                    postal_code = user.postal_code,
                    name = user.name,
                    phone_number = user.phone_number,
                    discount_code = user.discount_code,
                    city = user.city
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var userRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                var result = userRoot.message;
                rez = result;
               
            }
            return rez;
        }

        public async static Task<string> ChangeUserPass(string tokenC, string oldPassword, string newPassword)
        {
            var rez = "";
            var url = DataRoot.GenerateURL("changeUserPass");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    token = tokenC,
                    oldPass = oldPassword,
                    newPass = newPassword
                };
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var userRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                var result = userRoot.message;
                rez = result;

            }
            return rez;
        }
        public async static Task<List<object>> AddUserAddress(string tokenC, UserAddress userAddress)
        {
            List<object> rez = new List<object>();
            var url = DataRoot.GenerateURL("addUserAddress");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    address = userAddress.address,
                    postal_code = userAddress.postal_code,
                    city = userAddress.city,
                    phone_number = userAddress.phone_number,
                    customer_name = userAddress.name
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenC);
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var userRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                if(userRoot.last_id != null)
                {
                    rez.Add("Id");
                    rez.Add(userRoot.last_id);
                }
                else
                {
                    var result = userRoot.message;
                    rez.Add("Message");
                    rez.Add(result);
                }
            }
            return rez;
        }
        public async static Task<List<UserAddress>> GetUserAddresses(string tokenC)
        {
            List<UserAddress> addresses = new List<UserAddress>();

            var url = DataRoot.GenerateURL("getUserAddresses");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenC);
                //var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var dataRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                addresses = dataRoot.user_addresses as List<UserAddress>;
            }
            return addresses;
        }
        public async static Task<string> EditUserAddress(string tokenC, UserAddress userAddress)
        {
            var rez = "";
            var url = DataRoot.GenerateURL("editUserAddress");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    id = userAddress.id,
                    address = userAddress.address,
                    postal_code = userAddress.postal_code,
                    city = userAddress.city,
                    phone_number = userAddress.phone_number,
                    name = userAddress.name
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenC);
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var userRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                var result = userRoot.message;
                rez = result;

            }
            return rez;
        }
        public async static Task<string> DeleteUserAddress(string tokenC, int id)
        {
            var rez = "";
            var url = DataRoot.GenerateURL("deleteUserAddress");
            //-----
            // sito reikia nes nera ssl
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            //------
            using (HttpClient client = new HttpClient(handler))
            {
                var data = new
                {
                    id = id
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenC);
                var jsonData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, jsonData);
                var json = await response.Content.ReadAsStringAsync();
                var userRoot = JsonConvert.DeserializeObject<DataRoot>(json);
                var result = userRoot.message;
                rez = result;
            }
            return rez;
        }
    }
}

using Admin.Models;
using KeyCloakApi.Models;
using LoginData.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Services.Models
{
    public class Service
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<string> AdminToken()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {   
                AdminCredential cred = new AdminCredential();

                var body = new[]
                {
                    new KeyValuePair<string,string>("grant_type",cred.grant_type),
                    new KeyValuePair<string,string>("client_id",cred.client_id),
                    new KeyValuePair<string,string>("username",cred.username),
                    new KeyValuePair<string,string>("password",cred.password),
                    new KeyValuePair<string,string>("client_secret",cred.client_secret)
                };

                using HttpResponseMessage response = await client.PostAsync("http://localhost:8080/realms/master/protocol/openid-connect/token", new FormUrlEncodedContent(body));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();                

                string Token = JObject.Parse(responseBody)["access_token"].ToString();

                return Token;
            }
            catch (HttpRequestException e)
            {
                return "Exception Caught!\n Message :{0} " + e.Message;
            }
        }

        public async Task<string> GetUserList(string BearerToken)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
                using HttpResponseMessage response = await client.GetAsync("http://localhost:8080/admin/realms/Prashant/users");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return "Exception Caught!\n Message :{0} " + e.Message;
            }
        }


        public async Task<string> AddNewUser(string BearerToken , Register data)
        {
           
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

                var cont = JsonConvert.SerializeObject(data);
                var content = new StringContent(cont , Encoding.UTF8 , "application/json");
                
                using HttpResponseMessage response = await client.PostAsync("http://localhost:8080/admin/realms/Prashant/users", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return "Exception Caught!\n Message :{0} " + e.Message;
            }
        }


        public async Task<string> LoginUserToken(Login data)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                var body = new[]
                {
                    new KeyValuePair<string,string>("grant_type",data.grant_type),
                    new KeyValuePair<string,string>("client_id",data.client_id),
                    new KeyValuePair<string,string>("username",data.username),
                    new KeyValuePair<string,string>("password",data.password),
                    new KeyValuePair<string,string>("client_secret",data.client_secret)
                };

                using HttpResponseMessage response = await client.PostAsync("http://localhost:8080/realms/Prashant/protocol/openid-connect/token", new FormUrlEncodedContent(body));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                string Token = JObject.Parse(responseBody)["access_token"].ToString();

                return Token;
            }
            catch (HttpRequestException e)
            {
                return "Exception Caught!\n Message :{0} " + e.Message;
            }
        }


        public string getJWTTokenClaim(string token, string claimName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
            return claimValue;
        }
    }
}

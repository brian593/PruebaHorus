using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Horus.Models;
using System.Net.Mime;
using System.Net;

namespace Horus.Services
{
    public class ApiService
    {
        public async Task<DataUser> Login(User usuario)
        {
            try
            {
                

                var request = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.Usuario;
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("No fue posible conectarse");
                    return new DataUser();

                }
                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<DataUser>(result);
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No fue posible conectarse");
                return new DataUser();
              
            }

        }

        public async Task<List<Challenges>> GetCallenges(string Token)
        {
            try
            {



                var httpClient = new HttpClient();
                var httpMethod = HttpMethod.Get; //or Get, or whatever HTTP verb your API endpoint needs

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(Constants.ServiceUrl+Constants.Challenges),
                    Method = httpMethod
                };
                request.Headers.Add("Authorization", Token);

                var httpResponse = await httpClient.SendAsync(request);


                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var result = await httpResponse.Content.ReadAsStringAsync();

                    var Challenges = JsonConvert.DeserializeObject<List<Challenges>>(result);


                    return Challenges;
                }
                else
                {
                    return null;
                }



            }
            catch (Exception ex)
            {
                Debug.WriteLine("No fue posible conectarse");
                return null;

            }

        }

    }
}
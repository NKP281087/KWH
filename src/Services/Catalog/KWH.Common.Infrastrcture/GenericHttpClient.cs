using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.Infrastrcture
{
    public class GenericHttpClient
    {
        private HttpClient client;

        public GenericHttpClient()
        {

        }

        public string BaseURL { get; set; }
        public string ApiUrl { get; set; }

        public async Task<object> PostWithoutTokenAsync<T>(T modelObject)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            var result = await client.PostAsync(ApiUrl, new StringContent(JsonConvert.SerializeObject(modelObject)));
            //.ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()));
            var contents = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.SerializeObject(new ApiResponse(result.StatusCode, null, result.ReasonPhrase));
                return errorResponse;
            }
            return contents;
        }

        /// <summary>
        /// this method can be used to make request to api with token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        /// <returns></returns>
        public async Task<object> PostWithTokenAsync<T>(RequestViewModel<T> modelObject)
        {
            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + modelObject.Token);
                client.BaseAddress = new Uri(BaseURL);
                var result = await client.PostAsync(ApiUrl, new StringContent(JsonConvert.SerializeObject(modelObject.ModelObject), Encoding.UTF8, "application/json"));

                var contents = await result.Content.ReadAsStringAsync();
                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = JsonConvert.SerializeObject(new ApiResponse(result.StatusCode, null, contents));
                    return errorResponse;
                }
                return contents;
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = JsonConvert.SerializeObject(new ApiResponse(System.Net.HttpStatusCode.InternalServerError, null, ex.Message));
                return errorResponse;
            }
        }

        public async Task<object> PostWithToken<T>(RequestViewModel<T> modelObject)
        {
            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + modelObject.Token);
                client.BaseAddress = new Uri(BaseURL);
                 
                StringContent content = new StringContent(JsonConvert.SerializeObject(modelObject.ModelObject), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(ApiUrl, content);
                //var result = await client.PostAsync(ApiUrl, new StringContent(JsonConvert.SerializeObject(modelObject.ModelObject)));

                var contents = await result.Content.ReadAsStringAsync();
                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = JsonConvert.SerializeObject(new ApiResponse(result.StatusCode, null, contents));
                    return errorResponse;
                }
                return contents;
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = JsonConvert.SerializeObject(new ApiResponse(System.Net.HttpStatusCode.InternalServerError, null, ex.Message));
                return errorResponse;
            }
        }

        /// <summary>
        /// this api method can be used to make a get request using token
        /// </summary>
        /// <typeparam name="T">token</typeparam>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<object> GetWithTokenAsync(string Token)
        {
            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                client.BaseAddress = new Uri(BaseURL);
                var response = await client.GetAsync(ApiUrl);
                //    var response = await client.GetAsync(baseURL + "/api/values/1");
                var result = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = JsonConvert.SerializeObject(new ApiResponse(response.StatusCode, null, response.ReasonPhrase));
                    return errorResponse;
                }
                return result;
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = JsonConvert.SerializeObject(new ApiResponse(System.Net.HttpStatusCode.InternalServerError, null, ex.Message));
                return errorResponse;
            }
        }

        /// <summary>
        /// this method is used for Login purpose only
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> LoginAsync(LoginModel model)
        {
            try
            {

                client = new HttpClient();
                var formContent = new FormUrlEncodedContent(new[]{
                    new KeyValuePair<string, string>("username", model.UserName),
                    new KeyValuePair<string, string>("password", model.Password),
                    new KeyValuePair<string, string>("grant_type", "password"),
        });
                var response = await client.PostAsync(BaseURL + "/Token", formContent);
                var result = response.Content.ReadAsStringAsync().Result;

                return new ApiResponse(response.StatusCode, result);

            }
            catch (HttpRequestException ex)
            {
                var errorResponse = new ApiResponse(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
                return errorResponse;
            }
        }

        /// <summary>
        /// this method can be used to make request to api with token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelObject"></param>
        /// <returns></returns>
        public async Task<object> PostWithTokenForTimeOutAsync<T>(RequestViewModel<T> modelObject)
        {
            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + modelObject.Token);
                client.BaseAddress = new Uri(BaseURL);
                client.Timeout = TimeSpan.FromMinutes(60);
                var result = await client.PostAsync(ApiUrl, new StringContent(JsonConvert.SerializeObject(modelObject.ModelObject)));

                var contents = await result.Content.ReadAsStringAsync();
                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = JsonConvert.SerializeObject(new ApiResponse(result.StatusCode, null, contents));
                    return errorResponse;
                }
                return contents;
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = JsonConvert.SerializeObject(new ApiResponse(System.Net.HttpStatusCode.InternalServerError, null, ex.Message));
                return errorResponse;
            }
        }
        /// <summary>
        /// this api method can be used to make a get request using token
        /// </summary>
        /// <typeparam name="T">token</typeparam>
        /// <param name="Token"></param>
        /// <returns></returns>
        public async Task<object> GetWithTokenAsyncForTimeOutAsync(string Token)
        {
            try
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                client.BaseAddress = new Uri(BaseURL);
                client.Timeout = TimeSpan.FromMinutes(60);
                var response = await client.GetAsync(ApiUrl);
                //    var response = await client.GetAsync(baseURL + "/api/values/1");
                var result = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = JsonConvert.SerializeObject(new ApiResponse(response.StatusCode, null, response.ReasonPhrase));
                    return errorResponse;
                }
                return result;
            }
            catch (HttpRequestException ex)
            {
                var errorResponse = JsonConvert.SerializeObject(new ApiResponse(System.Net.HttpStatusCode.InternalServerError, null, ex.Message));
                return errorResponse;
            }
        }
    }
}

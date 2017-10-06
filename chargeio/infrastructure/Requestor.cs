using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChargeIO.Infrastructure
{
    internal static class Requestor
    {
        public static readonly string GetUserAgent = $"ChargeIO .net Client ({Configuration.AssemblyVersion})";

        public static string GetString(string url, string secretKey = null)
        {
            return Get(url).Result;
        }

        public static string PostString(string url, string postData, string secretKey = null)
        {
            return Post(url, postData, "application/x-www-form-urlencoded", secretKey).Result;
        }

        public static string PostJson(string url, string postData, string secretKey = null)
        {
            return Post(url, postData, "application/json", secretKey).Result;
        }

        public static string PutString(string url, string putData, string secretKey = null)
        {
            return Put(url, putData, "application/x-www-form-urlencoded", secretKey).Result;
        }

        public static string PutJson(string url, string putData, string secretKey = null)
        {
            return Put(url, putData, "application/json", secretKey).Result;
        }

        public static string Delete(string url, string secretKey = null)
        {
            return Delete(url).Result;
        }

        private static async Task<string> Get(string url)
        {
            using (var client = PrepareHttpClient())
            {
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                client.Dispose();
                return HandleResponse(response, content);
            }
        }

        private static async Task<string> Put(string url, string putData, string contentType, string secretKey)
        {
            using (var client = PrepareHttpClient())
            {
                var response = await client.PutAsync(url, new StringContent(putData, Encoding.UTF8, contentType));
                var content = await response.Content.ReadAsStringAsync();
                client.Dispose();
                return HandleResponse(response, content);
            }
        }

        private static async Task<string> Post(string url, string putData, string contentType, string secretKey)
        {
            using (var client = PrepareHttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(putData, Encoding.UTF8, contentType));
                var content = await response.Content.ReadAsStringAsync();
                client.Dispose();
                return HandleResponse(response, content);
            }
        }

        private static async Task<string> Delete(string url)
        {
            using (var client = PrepareHttpClient())
            {
                var response = await client.DeleteAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                client.Dispose();
                return HandleResponse(response, content);
            }
        }

        private static HttpClient PrepareHttpClient()
        {
            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes(Configuration.SecretKey + ":");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(byteArray));
            client.Timeout = TimeSpan.FromMilliseconds(Configuration.HttpTimeout);
            client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent);
            return client;
        }

        private static string HandleResponse(HttpResponseMessage response, string content)
        {
            if (response.IsSuccessStatusCode)
            {
                return content;
            }
            var errors = Mapper<List<ChargeIoError>>.MapFromJson(content, "messages");
            throw new ChargeIoException(response.StatusCode, errors, errors[0].Message);
        }
    }
}
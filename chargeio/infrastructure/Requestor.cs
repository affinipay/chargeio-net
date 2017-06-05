using System;
using System.Net.Http;
using System.Text;

namespace ChargeIO.Infrastructure
{
	internal static class Requestor
	{
	    public static readonly string GetUserAgent = $"ChargeIO .net Client ({Configuration.AssemblyVersion})";
	    
        public static string GetString(string url, string secretKey = null)
		{
            return Get(url);
		}

        public static string PostString(string url, string postData, string secretKey = null)
        {
            return Post(url, postData, "application/x-www-form-urlencoded", secretKey);
        }

        public static string PostJson(string url, string postData, string secretKey = null)
        {
            return Post(url, postData, "application/json", secretKey);
        }

        public static string PutString(string url, string putData, string secretKey = null)
        {
            return Put(url, putData, "application/x-www-form-urlencoded", secretKey);
        }

        public static string PutJson(string url, string putData, string secretKey = null)
        {
            return Put(url, putData, "application/json", secretKey);
        }

        public static string Delete(string url, string secretKey = null)
		{
            return Delete(url);
		}

        private static string Get(string url)
        {
            using(var client = new HttpClient()) {
                try {
                    var byteArray = Encoding.ASCII.GetBytes(Configuration.SecretKey + ":");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.Timeout = TimeSpan.FromMilliseconds(Configuration.HttpTimeout);
                    client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent);
                    var result = client.GetStringAsync(url);
                    return result.Result;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        private static string Put(string url, string putData, string contentType, string secretKey)
        {
            using(var client = new HttpClient()) {
                try {
                    var byteArray = Encoding.ASCII.GetBytes(Configuration.SecretKey + ":");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.Timeout = TimeSpan.FromMilliseconds(Configuration.HttpTimeout);
                    client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent);
                    var result = client.PutAsync(url, new StringContent(putData, Encoding.UTF8, contentType));
                    return result.Result.Content.ReadAsStringAsync().Result;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        private static string Post(string url, string putData, string contentType, string secretKey)
        {
            using(var client = new HttpClient()) {
                try {
                    var byteArray = Encoding.ASCII.GetBytes(Configuration.SecretKey + ":");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.Timeout = TimeSpan.FromMilliseconds(Configuration.HttpTimeout);
                    client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent);
                    var result = client.PostAsync(url, new StringContent(putData, Encoding.UTF8, contentType));
                    return result.Result.Content.ReadAsStringAsync().Result;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        private static string Delete(string url)
        {
            using(var client = new HttpClient()) {
                try {
                    var byteArray = Encoding.ASCII.GetBytes(Configuration.SecretKey + ":");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.Timeout = TimeSpan.FromMilliseconds(Configuration.HttpTimeout);
                    client.DefaultRequestHeaders.Add("User-Agent", GetUserAgent);
                    var result = client.DeleteAsync(url);
                    return result.Result.Content.ReadAsStringAsync().Result;
                } catch (Exception e) {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }
    }
}
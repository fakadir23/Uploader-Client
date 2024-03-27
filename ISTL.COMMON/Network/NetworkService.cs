using ISTL.MODELS.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ISTL.COMMON.Network
{
    public class NetworkService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static T SubmitRequest<T>(object requestModel, string url, string accessToken)
        {
            try
            {
                return SubmitRequest<T>(requestModel, url, "POST", accessToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T SubmitSNSOPRequest<T>(object requestModel, string url, string accessToken, string header = "")
        {
            try
            {
                return SubmitRequest<T>(requestModel, url, "POST", accessToken, header);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T SubmitRequest<T>(object requestModel, string url, string requestType = "POST", string accessToken = "", string header = "")
        {
            try
            {
                string apiBaseURL = "";
                if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                {
                   apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlDev"];
                }
                else
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlProd"];
                }

                logger.Debug("API Endpoint:" + apiBaseURL + url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiBaseURL);
                client.DefaultRequestHeaders.Add("DeviceId", header);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromMinutes(10);


                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }


                T responseModel;
                HttpResponseMessage response = new HttpResponseMessage();

                if (requestType == "POST")
                {
                    response = client.PostAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "PUT")
                {
                    response = client.PutAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "GET")
                {
                    response = client.GetAsync(string.Format("{0}", url)).Result;
                }
                response.EnsureSuccessStatusCode();
                responseModel = response.Content.ReadAsAsync<T>().Result;
                return responseModel;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401")) throw new WebException();
                if (ex.Message.Contains("403")) throw new WebException();
                if (ex.Message.Contains("404")) throw new WebException();

                var expMsg1 = ex.InnerException?.Message;
                var expMsg2 = ex.InnerException?.InnerException?.Message;

                if (!string.IsNullOrEmpty(expMsg1))
                {
                    if (expMsg1.Contains("Unable to connect to the remote server")) throw new WebException();
                }

                if (!string.IsNullOrEmpty(expMsg2))
                {
                    if (expMsg2.Contains("Unable to connect to the remote server")) throw new WebException();
                    if (expMsg2.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host")) throw new WebException();

                    //The remote name could not be resolved: 'rabcdms.rab.gov.bd'
                    if (expMsg2.Contains("The remote name could not be resolved")) throw new WebException();
                    if (expMsg2.Contains("rabcdms.rab.gov.bd")) throw new WebException();
                }

                throw ex;
            }
        }

        public static T SubmitSmallTimeRequest<T>(object requestModel, string url, string requestType = "POST", string accessToken = "")
        {
            try
            {
                string apiBaseURL = "";
                if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlDev"];
                }
                else
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlProd"];
                }

                logger.Debug("API Endpoint:" + apiBaseURL + url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiBaseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);

                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                T responseModel;
                HttpResponseMessage response = new HttpResponseMessage();

                if (requestType == "POST")
                {
                    response = client.PostAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "PUT")
                {
                    response = client.PutAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "GET")
                {
                    response = client.GetAsync(string.Format("{0}", url)).Result;
                }
                response.EnsureSuccessStatusCode();
                responseModel = response.Content.ReadAsAsync<T>().Result;
                return responseModel;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401")) throw new WebException();
                if (ex.Message.Contains("403")) throw new WebException();
                if (ex.Message.Contains("404")) throw new WebException();

                var expMsg1 = ex.InnerException?.Message;
                var expMsg2 = ex.InnerException?.InnerException?.Message;

                if (!string.IsNullOrEmpty(expMsg1))
                {
                    if (expMsg1.Contains("Unable to connect to the remote server")) throw new WebException();
                }

                if (!string.IsNullOrEmpty(expMsg2))
                {
                    if (expMsg2.Contains("Unable to connect to the remote server")) throw new WebException();
                    if (expMsg2.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host")) throw new WebException();
                }

                throw ex;
            }
        }

        public static T SubmitBioRequest<T>(object requestModel, string url, string requestType = "POST", string accessToken = "")
        {
            try
            {
                string apiBaseURL = "";
                if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiAfisBaseUrlDev"];
                }
                else
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiAfisBaseUrlProd"];
                }

                logger.Debug("API Biometric Endpoint:" + apiBaseURL + url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiBaseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);


                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }


                T responseModel;
                HttpResponseMessage response = new HttpResponseMessage();

                if (requestType == "POST")
                {
                    response = client.PostAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "PUT")
                {
                    response = client.PutAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "GET")
                {
                    response = client.GetAsync(string.Format("{0}", url)).Result;
                }
                response.EnsureSuccessStatusCode();
                responseModel = response.Content.ReadAsAsync<T>().Result;
                return responseModel;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401")) throw new WebException();
                if (ex.Message.Contains("403")) throw new WebException();
                if (ex.Message.Contains("404")) throw new WebException();

                var expMsg1 = ex.InnerException?.Message;
                var expMsg2 = ex.InnerException?.InnerException?.Message;

                if (!string.IsNullOrEmpty(expMsg1))
                {
                    if (expMsg1.Contains("Unable to connect to the remote server")) throw new WebException();
                }

                if (!string.IsNullOrEmpty(expMsg2))
                {
                    if (expMsg2.Contains("Unable to connect to the remote server")) throw new WebException();
                    if (expMsg2.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host")) throw new WebException();
                }

                throw ex;
            }
        }

        public static T SubmitJailDbMatchBioRequest<T>(object requestModel, string url, string requestType = "POST", string accessToken = "")
        {
            try
            {
                string apiBaseURL = "";
                try
                {
                    apiBaseURL = ConfigurationManager.AppSettings["ApiJailAfisBaseUrlProd"];
                }
                catch(Exception x)
                {
                    logger.Error("Error resolving App config for Jail db ApiBaseUrl" + x.ToString());
                    apiBaseURL = "https://rabcdms.rab.gov.bd/jailafis/";
                }
                
                if (string.IsNullOrEmpty(apiBaseURL) || string.IsNullOrWhiteSpace(apiBaseURL))
                {
                    apiBaseURL = "https://rabcdms.rab.gov.bd/jailafis/";
                }

                logger.Debug("API Jail DB Biometric Endpoint:" + apiBaseURL + url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiBaseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);


                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }


                T responseModel;
                HttpResponseMessage response = new HttpResponseMessage();

                if (requestType == "POST")
                {
                    response = client.PostAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "PUT")
                {
                    response = client.PutAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "GET")
                {
                    response = client.GetAsync(string.Format("{0}", url)).Result;
                }
                response.EnsureSuccessStatusCode();
                responseModel = response.Content.ReadAsAsync<T>().Result;
                return responseModel;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401")) throw new WebException();
                if (ex.Message.Contains("403")) throw new WebException();
                if (ex.Message.Contains("404")) throw new WebException();

                var expMsg1 = ex.InnerException?.Message;
                var expMsg2 = ex.InnerException?.InnerException?.Message;

                if (!string.IsNullOrEmpty(expMsg1))
                {
                    if (expMsg1.Contains("Unable to connect to the remote server")) throw new WebException();
                }

                if (!string.IsNullOrEmpty(expMsg2))
                {
                    if (expMsg2.Contains("Unable to connect to the remote server")) throw new WebException();
                    if (expMsg2.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host")) throw new WebException();
                }

                throw ex;
            }
        }

        public static T SubmitNidRequest<T>(object requestModel, string url, string requestType = "POST", string accessToken = "")
        {
            try
            {
                string apiBaseURL = "";
                if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlDev"];
                }
                else
                {
                    apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlProd"];
                }

                logger.Debug("API NID Endpoint:" + apiBaseURL + url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiBaseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                int NID_TIME_LIMIT = Convert.ToInt32(ConfigurationManager.AppSettings["NIDIdentifyTimeLimitInSeconds"].ToString());
                client.Timeout = TimeSpan.FromSeconds(NID_TIME_LIMIT);

                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                T responseModel;
                HttpResponseMessage response = new HttpResponseMessage();

                if (requestType == "POST")
                {
                    response = client.PostAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "PUT")
                {
                    response = client.PutAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "GET")
                {
                    response = client.GetAsync(string.Format("{0}", url)).Result;
                }
                response.EnsureSuccessStatusCode();
                responseModel = response.Content.ReadAsAsync<T>().Result;
                return responseModel;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401")) throw new WebException();
                if (ex.Message.Contains("403")) throw new WebException();
                if (ex.Message.Contains("404")) throw new WebException();

                var expMsg1 = ex.InnerException?.Message;
                var expMsg2 = ex.InnerException?.InnerException?.Message;

                if (!string.IsNullOrEmpty(expMsg1))
                {
                    if (expMsg1.Contains("Unable to connect to the remote server")) throw new WebException();
                }
                if (!string.IsNullOrEmpty(expMsg2))
                {
                    if (expMsg2.Contains("Unable to connect to the remote server")) throw new WebException();
                    if (expMsg2.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host")) throw new WebException();
                }
                throw ex;
            }
        }

        public static T SubmitProfileManagementRequest<T>(object requestModel, string url, string requestType = "POST", string accessToken = "")
        {
            try
            {
                string apiBaseURL = "";
                if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                {
                    try
                    {
                        apiBaseURL = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrlProfileManagementDev"];
                    }
                    catch (Exception x)
                    {
                        logger.Error("Error resolving App config for Profile Management dev ApiBaseUrl" + x.ToString());
                    }
                }
                else
                {
                    try
                    {
                        apiBaseURL = ConfigurationManager.AppSettings["ApiBaseUrlProfileManagementProd"];
                    }
                    catch (Exception x)
                    {
                        logger.Error("Error resolving App config for Profile Management prod ApiBaseUrl" + x.ToString());
                    }
                }

                if (string.IsNullOrEmpty(apiBaseURL) || string.IsNullOrWhiteSpace(apiBaseURL))
                {
                    if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "dev")
                    {
                        apiBaseURL = "http://192.168.10.164:8090/profilemanagement/";
                    }
                }

                logger.Debug("API Profile Management Endpoint:" + apiBaseURL + url);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(apiBaseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(60);

                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                T responseModel;
                HttpResponseMessage response = new HttpResponseMessage();

                if (requestType == "POST")
                {
                    response = client.PostAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "PUT")
                {
                    response = client.PutAsJsonAsync(string.Format("{0}", url), requestModel).Result;
                }
                else if (requestType == "GET")
                {
                    response = client.GetAsync(string.Format("{0}", url)).Result;
                }
                response.EnsureSuccessStatusCode();
                responseModel = response.Content.ReadAsAsync<T>().Result;
                return responseModel;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401")) throw new WebException();
                if (ex.Message.Contains("403")) throw new WebException();
                if (ex.Message.Contains("404")) throw new WebException();

                var expMsg1 = ex.InnerException?.Message;
                var expMsg2 = ex.InnerException?.InnerException?.Message;

                if (!string.IsNullOrEmpty(expMsg1))
                {
                    if (expMsg1.Contains("Unable to connect to the remote server")) throw new WebException();
                }

                if (!string.IsNullOrEmpty(expMsg2))
                {
                    if (expMsg2.Contains("Unable to connect to the remote server")) throw new WebException();
                    if (expMsg2.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host")) throw new WebException();
                }

                throw ex;
            }
        }

        public static ApiResponse SubmitRequestForUpload(string url, byte[] data)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "PUT";
                byte[] byteArray = data;
                httpWebRequest.ContentType = "multipart/form-data";
                httpWebRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = httpWebRequest.GetRequestStream())
                {
                    // do pausing and stuff here by using a while loop and changing byteArray.Length into the desired length of your chunks
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream receiveStream = httpWebResponse.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream);
                string internalResponseString = readStream.ReadToEnd();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return new ApiResponse();
        }
    }
}

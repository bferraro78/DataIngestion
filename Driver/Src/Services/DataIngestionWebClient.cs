using DataIngestion.Src.Responses;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace DataIngestion.Src.Services
{
    public interface IDataIngestionWebClient
    { 
        ApiResponse<TResponse> Get<TResponse>(string url) where TResponse : new();

    }
    public class DataIngestionWebClient : IDataIngestionWebClient
    {
        
        private const string _requestFailed = "Failed to make request to provider.";

        public DataIngestionWebClient()
        {
        }

        public ApiResponse<TResponse> Get<TResponse>(string url) where TResponse : new()
        {
            var transactionId = Guid.NewGuid();
            var response = new ApiResponse<TResponse>();
            response.Data = new TResponse();
            try
            {
                using (var client = new WebClient())
                {
                    var responseJson = client.DownloadString(url);
                    if (response != null)
                    {
                        response = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(responseJson);
                    }
                }
            }
            catch (WebException ex)
            {
                HanldeError(transactionId, response, ex);
            }
            return response;
        }

        private void HanldeError<TResponse>(Guid transactionId, ApiResponse<TResponse> response, WebException ex)
        {
            {
                try
                {
                    var resp = ex.Response as HttpWebResponse;
                    if (resp == null)
                    {
                        response.ErrorMessage = _requestFailed;
                    }
                    else
                    {
                        using (var stream = resp.GetResponseStream())
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            var responseText = reader.ReadToEnd();
                            if (string.IsNullOrEmpty(responseText))
                            {
                                response.ErrorMessage = _requestFailed;
                                response.StatusCode = resp.StatusCode.ToString();
                            }
                            else
                            {
                                try
                                {
                                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseText);
                                    response.ErrorMessage = errorResponse.ErrorMessage;
                                    response.StatusCode = errorResponse.StatusCode;
                                }
                                catch
                                {
                                    response.ErrorMessage = responseText;
                                    response.StatusCode = resp.StatusCode.ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    response.ErrorMessage = _requestFailed;
                }
            }
        }
    }
}

using JSONDeserializerOfT.Interfaces;
using JSONDeserializerOfT.Response;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JSONDeserializerOfT
{
    //inspiration from https://wesleycabus.be
    public class Deserializer
    {
        public async Task<IResponse<T>> InsertGetRequest<T>(string url)
        {
            HttpResponseMessage response = null;
            string content = null;

            try
            {
                using (var client = new HttpClient())
                {
                    response = await client.GetAsync(url).ConfigureAwait(true);
                    content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

                    return response.IsSuccessStatusCode ? CreateResponse<T>(response.StatusCode, content) :
                        CreateErrorResponse<T>(response.StatusCode, response.ReasonPhrase, originalData: content);
                }
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(response?.StatusCode ?? HttpStatusCode.BadRequest, ex.Message, ex, content);
            }
        }

        public async Task<IResponse<T>> InsertGetByIdRequest<T>(string url, string id)
        {
            HttpResponseMessage response = null;
            string content = null;

            try
            {
                using (var client = new HttpClient())
                {
                    response = await client.GetAsync(url + id).ConfigureAwait(true);
                    content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

                    return response.IsSuccessStatusCode ? CreateResponse<T>(response.StatusCode, content) :
                        CreateErrorResponse<T>(response.StatusCode, response.ReasonPhrase, originalData: content);
                }
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(response?.StatusCode ?? HttpStatusCode.BadRequest, ex.Message, ex, content);
            }
        }

        public async Task<IResponse<T>> InsertPostRequest<T>(object request, string url)
        {
            HttpResponseMessage response = null;
            string content = null;
            var headers = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    response = await client.GetAsync(url).ConfigureAwait(true);
                    content = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

                    return response.IsSuccessStatusCode ? CreateResponse<T>(response.StatusCode, content) :
                        CreateErrorResponse<T>(response.StatusCode, response.ReasonPhrase, originalData: content);
                }
            }
            catch (Exception ex)
            {
                return CreateErrorResponse<T>(response?.StatusCode ?? HttpStatusCode.BadRequest, ex.Message, ex, content);
            }
        }

        private IResponse<T> CreateResponse<T>(HttpStatusCode statusCode, string json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<T>(json);
                return new Response<T>(statusCode, data);
            }
            catch (Exception e)
            {
                return new ErrorMessage<T>(statusCode, "Couldn't deserialize the received data to the requested type.")
                {
                    OriginalData = json,
                    Exception = e
                };
            }
        }

        private IResponse<T> CreateErrorResponse<T>(
        HttpStatusCode statusCode,
        string error,
        Exception exception = null,
        string originalData = null)
        {
            return new ErrorMessage<T>(statusCode, error)
            {
                Exception = exception,
                OriginalData = originalData
            };
        }

    }
}

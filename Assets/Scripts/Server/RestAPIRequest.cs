using System.Text.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using RestSharp;
using UnityEngine;
using DTO;

namespace Server
{
    public class RestAPIRequest : MonoBehaviour
    {
        public static async Task<RestResponse> Post<T>(string path, [CanBeNull] T bodyJsonData,
            [CanBeNull] HeaderDTO headerDto)
            where T : class
        {
            RestClient client = new RestClient(Constant.URL);
            RestRequest request = new RestRequest(path, Method.Post);

            if (headerDto != null)
            {
                request.AddHeader(headerDto.name, headerDto.value);
            }

            if (bodyJsonData != null)
            {
                request.AddJsonBody(bodyJsonData);
            }

            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Debug.Log(response.Content);
                return response;
            }
            else
            {
                    Debug.Log(response.Content);
                Debug.Log(response.StatusCode + " " + response.ErrorMessage);
                return response;
            }
        }

        public static async Task<RestResponse> Get<T>(string path, [CanBeNull] T bodyJsonData,
            [CanBeNull] HeaderDTO headerDto) where T : class
        {
            RestClient client = new RestClient(Constant.URL);
            RestRequest request = new RestRequest(path, Method.Get);
            if (headerDto != null)
            {
                request.AddHeader(headerDto.name, headerDto.value);
            }   

            if (bodyJsonData != null)
            {
                request.AddJsonBody(bodyJsonData);
            }

            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Debug.Log(response.Content);
                return response;
            }
            else
            {
                Debug.Log(response.StatusCode + " " + response.ErrorMessage);
                return null;
            }
        }
    }
}





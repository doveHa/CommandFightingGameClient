using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using UnityEngine;
using Server;
using Manager;
using DTO;
using UnityEngine.SceneManagement;

namespace Authentication
{
    public class Authentication : MonoBehaviour
    {
        public GameObject loginFailed;

        public static async Task<string> regist(string id, string pw, string name)
        {
            RestResponse response =
                await RestAPIRequest.Post(Constant.RestAPI.Auth.REGIST, new { loginId = id, loginPassword = pw }, null);

            if (response.IsSuccessful)
            {
                response =
                    await RestAPIRequest.Post(Constant.RestAPI.Auth.LOGIN, new { loginId = id, loginPassword = pw }, null);
                if (response.IsSuccessful)
                {
                    LoginManager.Manager.SetTokens(JsonSerializer.Deserialize<AuthTokensDTO>(response.Content));

                    response = await RestAPIRequest.Post(Constant.RestAPI.Player.CREATE, new { playerName = name },
                        LoginManager.Manager.GetAuthHeader());
                    
                    logout(LoginManager.Manager.GetTokens().refreshToken);
                }
            }
            else
            {
                return response.Content;
            }

            return null;
        }

        public static async Task<string> login(string id, string pw)
        {
            RestResponse response =
                await RestAPIRequest.Post(Constant.RestAPI.Auth.LOGIN, new { loginId = id, loginPassword = pw }, null);

            if (response.IsSuccessful)
            {
                Debug.Log("�α��� ����!");
                SceneLoadManager.Manager.LoadLoadingScene();
                LoginManager.Manager.SetTokens(JsonSerializer.Deserialize<AuthTokensDTO>(response.Content));
                string pName = await GetPlayerName();
                await PlayerLogin(pName);
                await LoginManager.Manager.Login();
            }

            return response.Content;
        }

        private static async Task<string> GetPlayerName()
        {
            RestResponse response = await RestAPIRequest.Get<string>(Constant.RestAPI.Player.PLAYERLIST, null,
                LoginManager.Manager.GetAuthHeader());
            if (response.IsSuccessful)
            {
                return JsonSerializer.Deserialize<List<PlayerDTO>>(response.Content)[0].name;
            }

            return null;
        }

        private static async Task PlayerLogin(string pName)
        {
            RestResponse response = await RestAPIRequest.Post(Constant.RestAPI.Auth.PLAYERLOGIN,
                new { playerName = pName, refreshToken = LoginManager.Manager.GetTokens().refreshToken, },
                LoginManager.Manager.GetAuthHeader());
            if (response.IsSuccessful)
            {
                LoginManager.Manager.SetTokens(JsonSerializer.Deserialize<AuthTokensDTO>(response.Content));
            }
        }

        public static async void Refresh(string refreshToken)
        {
            RestResponse response = await RestAPIRequest.Post(Constant.RestAPI.Auth.REFRESH,
                new { refreshToken = refreshToken },
                LoginManager.Manager.GetAuthHeader());
            if (response.IsSuccessful)
            {
                LoginManager.Manager.SetTokens(JsonSerializer.Deserialize<AuthTokensDTO>(response.Content));
            }
        }

        public static async void logout(string refreshToken)
        {
            RestResponse response = await RestAPIRequest.Post(Constant.RestAPI.Auth.LOGOUT,
                new { refreshToken = refreshToken },
                LoginManager.Manager.GetAuthHeader());
            if (response.IsSuccessful)
            {
                LoginManager.Manager.Logout();
            }
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Authentication;
using DTO;

namespace Manager
{
    public class LoginManager : MonoBehaviour
    {
        public static LoginManager Manager { get; private set; }
        private AuthTokensDTO tokensDto;
        private bool isLoggedIn = false;

        private Thread tokenRefreshThread;

        void Awake()
        {
            DontDestroyOnLoad(this);
            if (Manager == null)
            {
                Manager = this;
            }

            tokensDto = new AuthTokensDTO();
        }


        public HeaderDTO GetAuthHeader()
        {
            return new HeaderDTO("Authorization", "Bearer " + tokensDto.accessToken);
        }

        public AuthTokensDTO GetTokens()
        {
            return tokensDto;
        }

        public void SetTokens(AuthTokensDTO tokensDto)
        {
            this.tokensDto = tokensDto;
        }

        public async Task Login()
        {
            isLoggedIn = true;

            switch (tokensDto.role)
            {
                case "User":
                    await CharacterManager.Manager.Initialize();
                    SceneLoadManager.Manager.LoadUserMainScene();
                    break;
                case "Administer":
                    SceneLoadManager.Manager.LoadAdministerScene();
                    break;
            }

            tokenRefreshThread = new Thread(TokenRefresh);
            tokenRefreshThread.IsBackground = true;
            tokenRefreshThread.Start();
        }

        public void Logout()
        {
            isLoggedIn = false;
            Destroy(gameObject);
            SceneLoadManager.Manager.LoadHomeScene();
        }

        public bool IsLoggedIn()
        {
            return isLoggedIn;
        }

        private void TokenRefresh()
        {
            while (isLoggedIn)
            {
                Thread.Sleep(Constant.REFRESHINTERVAL);
                Authentication.Authentication.Refresh(tokensDto.refreshToken);
            }
        }
    }
}
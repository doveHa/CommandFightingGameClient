using UnityEngine.SceneManagement;
using UnityEngine;


namespace Manager
{
    public class SceneLoadManager : MonoBehaviour
    {
        public static SceneLoadManager Manager { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }

        public void LoadHomeScene()
        {
            SceneManager.LoadScene(Constant.Scene.HOME_SCENE);
        }

        public void LoadUserMainScene()
        {
            SceneManager.LoadScene(Constant.Scene.USER_MAIN_SCENE);
        }

        public void LoadAdministerScene()
        {
            SceneManager.LoadScene(Constant.Scene.ADMINISTRATOR_SCENE);
        }

        public void LoadGameScene(string opponentName)
        {
            Debug.Log(opponentName);
//            VarManager.Manager.OpponentCharacterName = opponentName;
            VarManager.Manager.OpponentCharacterName = "Kagetsu";
            SceneManager.LoadScene(Constant.Scene.GAME_SCENE);
        }

        public void LoadLoadingScene()
        {
            SceneManager.LoadScene(Constant.Scene.LOADING_SCENE);

        }
    }
}
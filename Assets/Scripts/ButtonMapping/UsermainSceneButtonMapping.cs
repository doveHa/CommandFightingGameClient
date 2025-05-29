using UnityEngine;
using Handler;
using Manager;

namespace ButtonMapping
{
    public class UsermainSceneButtonMapping : MonoBehaviour
    {
        [SerializeField] private GameObject userMainGroup, characterInfoGroup;
        [SerializeField] private CharacterSelectHandler characterSelectHandler;
        [SerializeField] private CommandRecodeHandler commandRecodeHandler;
        public GameObject main_Under_Bar;
        public GameObject character_Under_Bar;

        public void mainToCharacterInfo()
        {
            userMainGroup.SetActive(false);
            characterInfoGroup.SetActive(true);
            character_Under_Bar.SetActive(true);
            main_Under_Bar.SetActive(false);
        }

        public void characterInfoToMain()
        {
            characterInfoGroup.SetActive(false);
            characterSelectHandler.AllDescriptionOff();
            userMainGroup.SetActive(true);
            character_Under_Bar.SetActive(false);
            main_Under_Bar.SetActive(true);
        }

        public void Select()
        {
            if (CharacterSelectHandler.CurrentShowCharacter != null)
            {
                VarManager.Manager.PlayerCharacterName = CharacterSelectHandler.CurrentShowCharacter;
            }

            characterInfoToMain();

            CharacterManager.Manager.CharacterOn();
        }
        public void Logout()
        {
            Authentication.Authentication.logout(LoginManager.Manager.GetTokens().refreshToken);
        }
    }
}
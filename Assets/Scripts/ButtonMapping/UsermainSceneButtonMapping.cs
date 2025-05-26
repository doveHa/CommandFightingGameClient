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
        
        public void mainToCharacterInfo()
        {
            userMainGroup.SetActive(false);
            characterInfoGroup.SetActive(true);
        }

        public void characterInfoToMain()
        {
            characterInfoGroup.SetActive(false);
            characterSelectHandler.AllDescriptionOff();
            userMainGroup.SetActive(true);
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
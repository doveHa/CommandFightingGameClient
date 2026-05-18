using Core.OnClick;
using Handler;
using Manager;

namespace Characters.OnClick
{
    public class CharacterSelectButton : OnClickBase
    {
        protected override void OnClick()
        {
            if (CharacterSelectHandler.CurrentShowCharacter != null)
            {
                VarManager.Manager.PlayerCharacterName = CharacterSelectHandler.CurrentShowCharacter;
            }

            CharacterManager.Manager.CharacterOn(VarManager.Manager.PlayerCharacterName);
        }
    }
}
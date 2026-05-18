using Core.OnClick;
using Manager;

namespace Authentication.OnClick
{
    public class LogoutButton : OnClickBase
    {
        protected override void OnClick()
        {
            Authentication.logout(LoginManager.Manager.GetTokens().refreshToken);
        }
    }
}
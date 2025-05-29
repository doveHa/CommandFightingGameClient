using UnityEngine;
using UnityEngine.UI; // 반드시 필요!

public class ToggleObjects : MonoBehaviour
{
    public GameObject object1; // 활성화할 객체
    public GameObject object2; // 비활성화할 객체
    public Button ToHomeButton;   // 연결할 버튼

    private void Start()
    {
        if (ToHomeButton != null)
        {
            ToHomeButton.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (object1 != null) object1.SetActive(true);
        if (object2 != null) object2.SetActive(false);
    }
}

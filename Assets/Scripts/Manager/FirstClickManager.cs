using UnityEngine;

public class FirstClickManager : MonoBehaviour
{
    public GameObject openObject; // 활성화할 오브젝트
    public GameObject closeObject;
    private bool isActivated = false; // 이미 활성화되었는지 여부

    void Update()
    {
        if (!isActivated && Input.GetMouseButtonDown(0)) // 아직 활성화 안 됐고, 마우스 클릭했을 때
        {
            if (openObject != null)
            {
                openObject.SetActive(true);
                closeObject.SetActive(false);
                isActivated = true; // 더 이상 실행되지 않도록 설정
            }
        }
    }
}

using Manager;
using UnityEngine;

public class CameraMovingManager : MonoBehaviour
{
    private float verticalSize = 5f;
    private float zoomSpeed = 5f;

    private Transform player1;
    private Transform player2;
    private float additionalRender = 4f;
    private float minDistance = 18f;
    public float maxDistance = 26f;

    public float currentDistance;

    private Camera cam;
    private float initialY;

    private Vector3 velocity = Vector3.zero; // SmoothDamp용 속도 캐시

    void Start()
    {
        cam = GetComponent<Camera>();
        initialY = transform.position.y;
        player1 = VarManager.Manager.PlayerGameObject.transform.GetChild(0);
        player2 = VarManager.Manager.OpponentGameObject.transform.GetChild(0);
    }

    void LateUpdate() // 위치 업데이트는 LateUpdate에서 하면 더 안정적
    {
        float distance = Mathf.Abs(player1.position.x - player2.position.x) + additionalRender;
        currentDistance = distance;

        float currentWidth = Mathf.Clamp(distance / 2f, minDistance / 2f, maxDistance / 2f);
        float needHeight = Mathf.Max(verticalSize, currentWidth / cam.aspect);

        // 줌 부드럽게 적용
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, needHeight, Time.deltaTime * zoomSpeed);

        // 카메라 위치 보간
        float upwardOffset = cam.orthographicSize - verticalSize;
        Vector3 targetPosition = new Vector3(
            (player1.position.x + player2.position.x) / 2f,
            initialY + upwardOffset,
            transform.position.z
        );

        // 부드럽게 위치 이동
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.2f);
    }
}

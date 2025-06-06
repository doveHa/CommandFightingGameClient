using Manager;
using UnityEngine;

public class CameraMovingManager : MonoBehaviour
{
    private float verticalSize = 5f;
    private float zoomSpeed = 5f;
    
    private Transform player1;
    private Transform player2;
    private float additionalRender = 4;
    private float minDistance = 18f;
    private float maxDistance = 26f;
    
    private Camera cam;
    private float initialY;

    void Start()
    {
        cam = GetComponent<Camera>();
        initialY = transform.position.y;
        player1 = VarManager.Manager.PlayerGameObject.transform.GetChild(0);
        player2 = VarManager.Manager.OpponentGameObject.transform.GetChild(0);
    }
    
    void Update()
    {
        float distance = Mathf.Abs(player1.position.x - player2.position.x) + additionalRender;
        
        float currentWidth = Mathf.Clamp(distance / 2f, minDistance / 2f, maxDistance / 2f);
        float needHeight = Mathf.Max(verticalSize, currentWidth / cam.aspect);
        
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, needHeight, Time.deltaTime * zoomSpeed);

        float upwardOffset = (cam.orthographicSize - verticalSize);
        Vector3 center = (player1.position + player2.position) / 2f;

        transform.position = new Vector3(center.x, initialY + upwardOffset, transform.position.z);
    }
}
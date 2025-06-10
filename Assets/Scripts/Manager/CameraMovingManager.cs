using System;
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

    private Vector3 velocity = Vector3.zero; // SmoothDamp�� �ӵ� ĳ��

    void Start()
    {
        cam = GetComponent<Camera>();
        initialY = transform.position.y;
        player1 = VarManager.Manager.PlayerGameObject.transform.GetChild(0);
        player2 = VarManager.Manager.OpponentGameObject.transform.GetChild(0);
    }

    void LateUpdate() // ��ġ ������Ʈ�� LateUpdate���� �ϸ� �� ������
    {
        float distance = Mathf.Abs(player1.position.x - player2.position.x) + additionalRender;
        currentDistance = distance;

        float currentWidth = Mathf.Clamp(distance / 2f, minDistance / 2f, maxDistance / 2f);
        float needHeight = Mathf.Max(verticalSize, currentWidth / cam.aspect);

        // �� �ε巴�� ����
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, needHeight, Time.deltaTime * zoomSpeed);

        // ī�޶� ��ġ ����
        float upwardOffset = cam.orthographicSize - verticalSize;
        double targetX = Math.Clamp((player1.position.x + player2.position.x) / 2f, -4.3, 4.3);
        Vector3 targetPosition = new Vector3(
            (float)targetX,
            initialY + upwardOffset,
            transform.position.z
        );
        // �ε巴�� ��ġ �̵�
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.2f);
    }
}
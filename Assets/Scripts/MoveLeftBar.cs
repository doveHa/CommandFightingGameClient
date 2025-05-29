using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveLeftBar : MonoBehaviour
{
    public Transform targetObject;      // 이동시킬 객체
    public Button moveButton;           // UI 버튼
    public float moveDistance = 200f;
    public float moveSpeed = 10f;

    public GameObject triggerObject;    // 감지할 객체 (비활성화되면 되돌리기)

    private Vector3 startPos;
    private Vector3 endPos;
    private bool isMoving = false;
    private bool hasMoved = false;

    private Coroutine currentMoveCoroutine = null;

    void Start()
    {
        if (moveButton != null)
        {
            moveButton.onClick.AddListener(MoveObject);
        }

        if (targetObject != null)
        {
            startPos = targetObject.position;
        }
    }

    void Update()
    {
        // triggerObject가 비활성화되었고, 이동했거나 이동 중이면 즉시 되돌림
        if (triggerObject != null && !triggerObject.activeSelf && (hasMoved || isMoving))
        {
            // 현재 이동 중이면 중지
            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
                currentMoveCoroutine = null;
            }

            // 즉시 원래 위치로 되돌림
            MoveBackInstantly();
        }
    }

    void MoveObject()
    {
        if (targetObject == null)
            return;

        // 이동 중이거나 이미 이동된 상태라면 먼저 되돌린 후 이동
        if (isMoving || hasMoved)
        {
            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
                currentMoveCoroutine = null;
            }

            MoveBackInstantly();
        }

        // 시작 위치 업데이트 (원위치일 수도 있고 갱신될 수도 있음)
        startPos = targetObject.position;
        endPos = startPos + new Vector3(moveDistance * 2f, 0f, 0f);

        currentMoveCoroutine = StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        isMoving = true;

        while (Vector3.Distance(targetObject.position, endPos) > 0.01f)
        {
            targetObject.position = Vector3.MoveTowards(
                targetObject.position,
                endPos,
                moveSpeed * Time.deltaTime * 200f
            );

            yield return null;
        }

        targetObject.position = endPos;
        isMoving = false;
        hasMoved = true;
        currentMoveCoroutine = null;
    }

    void MoveBackInstantly()
    {
        targetObject.position = startPos;
        isMoving = false;
        hasMoved = false;
    }
}

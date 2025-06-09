using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public Camera targetCamera;            // 이동 기준이 될 카메라
    public float speed = 30f;              // 이동 속도
    public float distanceFromCamera = 5f;  // 카메라 앞 거리
    public AudioClip moveSound;            // 재생할 오디오 클립

    private bool moveToCenter = false;
    private Vector3 targetPosition;
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource 컴포넌트를 추가하거나 가져오기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (targetCamera == null)
            {
                Debug.LogError("카메라가 할당되지 않았습니다.");
                return;
            }

            // 목표 위치 계산
            Vector3 screenCenter = new Vector3(0.5f, 0.7f, distanceFromCamera);
            targetPosition = targetCamera.ViewportToWorldPoint(screenCenter);
            moveToCenter = true;

            // 사운드 재생
            if (moveSound != null)
            {
                audioSource.PlayOneShot(moveSound);
            }
        }

        if (moveToCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                moveToCenter = false;
            }
        }
    }
}

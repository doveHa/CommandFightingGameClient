using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayCharacterSoundOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip clickSound;
    public float scaleMultiplier = 1.0f;
    public Button stopButton;

    private Vector3 originalScale;
    private AudioSource audioSource;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // 마우스 왼쪽 버튼이 클릭되었을 때
        if (Input.GetMouseButtonDown(0))
        {
            // stopButton이 설정되어 있다면
            if (stopButton != null)
            {
                RectTransform stopRect = stopButton.GetComponent<RectTransform>();

                // 클릭한 위치가 stopButton 안에 있는지 확인
                if (RectTransformUtility.RectangleContainsScreenPoint(stopRect, Input.mousePosition, null))
                {
                    Debug.Log("Stop button clicked (via Update).");
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                        Debug.Log("Audio stopped.");
                    }
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale * scaleMultiplier;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.localScale = originalScale;

        if (clickSound != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = clickSound;
            audioSource.Play();
        }
    }
}

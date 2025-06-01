using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayCharacterSoundOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip clickSound;
    public float scaleMultiplier = 1.0f;
    public Button stopButton1;
    public Button stopButton2;

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
            // stopButton1이 설정되어 있다면
            if (stopButton1 != null)
            {
                RectTransform stopRect1 = stopButton1.GetComponent<RectTransform>();

                if (RectTransformUtility.RectangleContainsScreenPoint(stopRect1, Input.mousePosition, null))
                {
                    Debug.Log("Stop button 1 clicked (via Update).");
                    if (audioSource.isPlaying)
                    {
                        audioSource.Stop();
                        Debug.Log("Audio stopped.");
                    }
                }
            }

            // stopButton2도 같은 방식으로 처리
            if (stopButton2 != null)
            {
                RectTransform stopRect2 = stopButton2.GetComponent<RectTransform>();

                if (RectTransformUtility.RectangleContainsScreenPoint(stopRect2, Input.mousePosition, null))
                {
                    Debug.Log("Stop button 2 clicked (via Update).");
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

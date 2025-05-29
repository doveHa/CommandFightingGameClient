using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioClip clickSound;
    public float scaleMultiplier = 0.8f;

    private Vector3 originalScale;
    private AudioSource audioSource;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;

        // AudioSource 자동 추가
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
            audioSource.PlayOneShot(clickSound);
        }
    }
}

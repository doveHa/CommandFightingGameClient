using TMPro;
using UnityEngine;

public class LoadingTextAnimator : MonoBehaviour
{
    public TextMeshProUGUI loadingText; // 연결할 TMP UI 텍스트
    public float switchInterval = 0.1f;  // 간격 (초)

    private string[] texts = { "Loding", "Loding.", "Loding..", "Loding..." };
    private int currentIndex = 0;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % texts.Length;
            loadingText.text = texts[currentIndex];
        }
    }
}

using TMPro;
using UnityEngine;

public class LoadingTextAnimator : MonoBehaviour
{
    public TextMeshProUGUI loadingText; // ������ TMP UI �ؽ�Ʈ
    public float switchInterval = 0.1f;  // ���� (��)

    private string[] texts = { "Loading", "Loading.", "Loading..", "Loading..." };
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

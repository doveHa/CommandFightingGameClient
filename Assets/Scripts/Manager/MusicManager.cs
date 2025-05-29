using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject sound_Stop_Btn;
    public GameObject sound_Play_Btn;

    private void Start()
    {
        // 자동 재생을 원하면 이 줄을 사용
        audioSource.Play();
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.UnPause();
            sound_Stop_Btn.SetActive(true);
            sound_Play_Btn.SetActive(false);
        }
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
            sound_Stop_Btn.SetActive(false);
            sound_Play_Btn.SetActive(true);
        }
    }
}

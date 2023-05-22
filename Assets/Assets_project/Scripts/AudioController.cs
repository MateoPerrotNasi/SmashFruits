using UnityEngine;

public class AudioController : MonoBehaviour
{
    public string audioPath = "Assets/Assets_project/Audio/";
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnAndOff()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }
}

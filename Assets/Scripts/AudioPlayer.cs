using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip menuButtonClick;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShotMenuButtonClick()
    {
        audioSource.PlayOneShot(menuButtonClick);
    }
}

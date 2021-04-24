using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip menuButtonClick;

    AudioSource audioSource;

    bool isMusicOn = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShotMenuButtonClick()
    {
        audioSource.PlayOneShot(menuButtonClick);
    }

    public void OptionsMenuMusicOnOffButton()
    {
        if (isMusicOn == false)
        {
            print("[AudioPlayer] PlayMainMenuMusic() if (isMusicOn == false)");
            audioSource.Play();
            isMusicOn = true;
        }
        else if (isMusicOn == true)
        {
            print("[AudioPlayer] PlayMainMenuMusic() if (isMusicOn == true)");
            audioSource.PlayOneShot(menuButtonClick);
            audioSource.Stop();
            isMusicOn = false;
        }
    }
}

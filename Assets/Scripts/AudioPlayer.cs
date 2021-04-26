using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip menuButtonClick;
    [SerializeField] AudioClip ding;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip descentLevelMusic;
    [SerializeField] AudioClip explorationlevelMusic;

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

    public void LoadMainMenuEvent()
    {
        audioSource.clip = mainMenuMusic;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void LoadExploratLevelEvent()
    {
        audioSource.clip = explorationlevelMusic;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void LoadDescentLevelEvent()
    {
        audioSource.clip = descentLevelMusic;
        audioSource.volume = 0.10f;
        audioSource.Play();
    }
}
using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region STATE SELECTION
    enum UIstateSelection
    {
        MainMenu, FadeCanvas, OptionsMenu, CreditsMenu, PauseMenu
    }

    [SerializeField] UIstateSelection currentState;
    #endregion

    #region EVENTS
    //EVENTS
    [SerializeField] GameEvent UIButtonPressEvent;
    [SerializeField] GameEvent MainMenuPlayButtonEvent;
    [SerializeField] GameEvent MainMenuOptionsButtonEvent;
    [SerializeField] GameEvent MainMenuCreditsButtonEvent;
    [SerializeField] GameEvent MainMenuExploratEvent;
    [SerializeField] GameEvent MainMenuInfDescentEvent;
    [SerializeField] GameEvent ESCKeyPressEvent;
    [SerializeField] GameEvent MusicOnOffButtonEvent;
    [SerializeField] GameEvent PauseMenuMainMenuEvent;
    #endregion

    #region MENU SETTINGS
    // FADE CANVAS
    [SerializeField] float fadeOutTargetAlpha = 1f;
    [SerializeField] float fadeInTime = 1f;
    CanvasGroup canvasGroup;
    int nFrames;
    int remainingFrames;
    float deltaAlpha;

    // CREDITS MENU
    [SerializeField] TMP_Text creditsText;
    string designText = "@MITCH_HEISENBERG";
    string musicText = "@TroyanskiyYaroslav";

    // PAUSE MENU
    bool isPauseMenuOpen = false;
    #endregion

    #region UNITY GAME LOOP
    private void OnEnable()
    {
        if (currentState == UIstateSelection.FadeCanvas)
        {
            MenuSetActive();
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            StartCoroutine(FadeIn(fadeInTime));
        }
    }

    private void Start()
    {
        if (currentState == UIstateSelection.OptionsMenu)
        {
            MenuSetInactive();
        }
        else if (currentState == UIstateSelection.CreditsMenu)
        {
            MenuSetInactive();
        }
        else if (currentState == UIstateSelection.PauseMenu)
        {
            MenuSetInactive();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape") && currentState == UIstateSelection.OptionsMenu)
        {
            ESCKeyPressEvent.Raise();
        }
        else if (Input.GetKeyDown("escape") && currentState == UIstateSelection.PauseMenu)
        {
            ESCKeyPressEvent.Raise();
        }
    }
    #endregion

    #region GENERAL SHARED METHODS
    // GENERAL SHARED METHODS
    void ChildrenSetActive(bool _bool)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(_bool);
        }
    }

    public void UIButtonPress()
    {
        UIButtonPressEvent.Raise();
    }


    public void MenuSetActive()
    {
        ChildrenSetActive(true);
    }

    public void MenuSetInactive()
    {
        ChildrenSetActive(false);
    }
    #endregion

    #region FADE CANVAS
    // FADE CANVAS
    public IEnumerator FadeOut(float fadeOutTime)
    {
        nFrames = Mathf.RoundToInt(fadeOutTime / Time.deltaTime);
        remainingFrames = nFrames;
        deltaAlpha = fadeOutTargetAlpha / nFrames;
        while (remainingFrames > 0)
        {
            canvasGroup.alpha += deltaAlpha;
            remainingFrames--;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float fadeInTime)
    {
        nFrames = Mathf.RoundToInt(fadeInTime / Time.deltaTime);
        remainingFrames = nFrames;
        deltaAlpha = fadeOutTargetAlpha / nFrames;
        while (remainingFrames > 0)
        {
            canvasGroup.alpha -= deltaAlpha;
            remainingFrames--;
            yield return null;
        }

        MenuSetInactive();
    }
    #endregion

    #region MAIN MENU
    // MAIN MENU
    public void MainMenuPlayButton()
    {
        MainMenuPlayButtonEvent.Raise();
    }

    public void MainMenuOptionsButton()
    {
        MainMenuOptionsButtonEvent.Raise();
    }

    public void MainMenuCreditsButton()
    {
        MainMenuCreditsButtonEvent.Raise();
    }

    public void MainMenuInfDescentSelection()
    {
        MainMenuInfDescentEvent.Raise();
    }

    public void MainMenuExploratSelection()
    {
        MainMenuExploratEvent.Raise();
    }
    #endregion

    #region OPTIONS MENU
    // OPTIONS MENU
    public void OptionsMenuMusicOnOffButton()
    {
        UIButtonPressEvent.Raise();

        if (currentState == UIstateSelection.OptionsMenu || currentState == UIstateSelection.PauseMenu)
        {
            MusicOnOffButtonEvent.Raise();
        }
    }
    #endregion

    #region CREDITS MENU
    // CREDITS MENU
    public void CreditsMenuDesignButton()
    {
        creditsText.text = designText;
    }

    public void CreditsMenuMusicButton()
    {
        creditsText.text = musicText;
    }
    #endregion

    #region PAUSE MENU
    // PAUSE MENU
    public void PauseMenuOnESCPress()
    {
        if (isPauseMenuOpen == false && currentState == UIstateSelection.PauseMenu)
        {
            MenuSetActive();
            isPauseMenuOpen = true;
            Time.timeScale = 0;
        }
        else if (isPauseMenuOpen == true && currentState == UIstateSelection.PauseMenu)
        {
            MenuSetInactive();
            isPauseMenuOpen = false;
            Time.timeScale = 1;
        }
        else if (currentState == UIstateSelection.MainMenu)
        {
            print("[UIManager] OnESCPressEventMethod() Quit application here from main menu");
            // ADD APPLICATION QUIT METHOD HERE
        }
    }

    public void PauseMenuMainMenuButton()
    {
        PauseMenuMainMenuEvent.Raise();
    }
    #endregion
}
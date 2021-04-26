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
    [SerializeField] GameEvent ESCKeyPressEvent;
    [SerializeField] GameEvent MusicOnOffButtonEvent;
    [SerializeField] GameEvent PauseMenuMainMenuEvent;
    #endregion

    #region MENU SETTINGS
    // FADE CANVAS
    Animator animator;
    [SerializeField] float delay = 1f;

    // CREDITS MENU
    [SerializeField] TMP_Text creditsText;
    string designText = "@MITCH_HEISENBERG";
    string musicText = "@TroyanskiyYaroslav";

    // PAUSE MENU
    bool isPauseMenuOpen = false;
    #endregion

    #region UNITY GAME LOOP
    private void Start()
    {
        if (currentState == UIstateSelection.FadeCanvas)
        {
//            MenuSetActive();

            animator = GetComponentInChildren<Animator>();
            StartCoroutine(FadeInCoroutine());
        }
        if (currentState == UIstateSelection.OptionsMenu)
        {
            MenuSetInactive();
        }
        if (currentState == UIstateSelection.CreditsMenu)
        {
            MenuSetInactive();
        }
        if (currentState == UIstateSelection.PauseMenu)
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
    IEnumerator FadeInCoroutine()
    {
        print("[UIManager] FadeInCoroutine start");
        
        animator.enabled = true;
        yield return new WaitForSeconds(delay);

        ChildrenSetActive(false);

        print("[UIManager] FadeInCoroutine finish");
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
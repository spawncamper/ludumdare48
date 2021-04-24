using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    enum UIstateSelection
    {
        MessageCanvas, GameOverMenu, MainMenu, FadeCanvas, OptionsMenu
    }

    [SerializeField] UIstateSelection currentState;

    //EVENTS
    [SerializeField] GameEvent UIButtonPressEvent;
    [SerializeField] GameEvent MainMenuPlayButtonEvent;
    [SerializeField] GameEvent MainMenuOptionsButtonEvent;
    [SerializeField] GameEvent ESCKeyPressEvent;
    [SerializeField] GameEvent MusicOnOffButtonEvent;

    // FADE CANVAS
    Animator animator;
    [SerializeField] float delay = 1f;

    private void Start()
    {
        if (currentState == UIstateSelection.FadeCanvas)
        {
            ChildrenSetActive(true);

            animator = GetComponentInChildren<Animator>();
            StartCoroutine(FadeInCoroutine());
        }
        if (currentState == UIstateSelection.OptionsMenu)
        {
            ChildrenSetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape") && currentState == UIstateSelection.OptionsMenu)
        {
            ESCKeyPressEvent.Raise();
        }
    }

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

    // FADE CANVAS
    IEnumerator FadeInCoroutine()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(delay);

        ChildrenSetActive(false);
    }

    // MAIN MENU
    public void MainMenuPlayButton()
    {
        MainMenuPlayButtonEvent.Raise();
    }

    public void MainMenuOptionsButton()
    {
        MainMenuOptionsButtonEvent.Raise();
    }

    // OPTIONS MENU
    public void OptionsMenuMusicOnOffButton()
    {
        UIButtonPressEvent.Raise();

        if (currentState == UIstateSelection.OptionsMenu)
        {
            MusicOnOffButtonEvent.Raise();
        }
    }
}

using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    enum UIstateSelection
    {
        MessageCanvas, GameOverMenu, MainMenu, FadeCanvas
    }

    [SerializeField] UIstateSelection currentState;

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
    }

    IEnumerator FadeInCoroutine()
    {
        animator.enabled = true;
        yield return new WaitForSeconds(delay);

        ChildrenSetActive(false);
    }

    void ChildrenSetActive(bool _bool)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(_bool);
        }
    }
}

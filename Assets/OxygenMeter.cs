using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    [SerializeField] GameEvent PlayerDeathEvent;
    [SerializeField] GameEvent GameOverEvent;

    [SerializeField] int initialO2 = 100;
    [SerializeField] int o2DepletionRate = 2;

    [SerializeField] float restartDelay = 3f;

    Slider slider;

    bool playerAlive = true;

    // Update is called once per frame
    IEnumerator Start()
    {
        slider = GetComponentInChildren<Slider>();

        slider.value = initialO2;

        yield return StartCoroutine(GameLoop());

        yield return new WaitForSeconds(restartDelay);

        GameOverEvent.Raise();
    }

    IEnumerator GameLoop()
    {
        while (playerAlive == true)
        {
            yield return new WaitForSeconds(o2DepletionRate);

            slider.value -= o2DepletionRate;

            if (slider.value <= 0)
            {
                playerAlive = false;

                PlayerDeathEvent.Raise();

                Time.timeScale = 0;
            }
        }
    }
}
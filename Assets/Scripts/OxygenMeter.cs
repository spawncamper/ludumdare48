using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    [SerializeField] GameEvent PlayerDeathEvent;
    [SerializeField] GameEvent GameOverEvent;

    [SerializeField] int initialO2 = 100;
    [SerializeField] int o2DepletionRate = 2;
    [SerializeField] int rockCollision = 10;

    [SerializeField] float restartDelay = 3f;

    Slider slider;

    bool playerAlive = true;

    // Update is called once per frame
    IEnumerator Start()
    {
        print("[OxygenMeter] IEnumerator Start() level Started");
        
        slider = GetComponentInChildren<Slider>();

        slider.value = initialO2;

        yield return StartCoroutine(GameLoop());

        yield return new WaitForSeconds(restartDelay);

        playerAlive = true;

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
            }
        }
    }

    public void CollisionRockEvent()
    {
        slider.value -= rockCollision;
    }

    public void RuinFoundEvent()
    {
        slider.value = 100;
    }
}
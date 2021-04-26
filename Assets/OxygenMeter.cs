using System.Collections;
using UnityEngine;

public class OxygenMeter : MonoBehaviour
{
    [SerializeField] GameEvent PlayerDeathEvent;

    [SerializeField] int initialO2 = 100;
    [SerializeField] int o2DepletionRate = 2;

    bool playerAlive = true;

    // Update is called once per frame
    IEnumerator Start()
    {
        int currentOxygen = initialO2;
        
        while (playerAlive == true)
        {
            yield return new WaitForSeconds(o2DepletionRate);
            currentOxygen -= o2DepletionRate;

            if (currentOxygen <= 0)
            {
                playerAlive = false;

                PlayerDeathEvent.Raise();
            }
        }
    }
}
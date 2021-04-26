using System.Collections.Generic;
using UnityEngine;

public class RuinsManager : MonoBehaviour
{
    List<GameObject> ruinsList = new List<GameObject>();



    private void Start()
    {
        ruinsList = new List<GameObject>();

        ruinsList.AddRange(GameObject.FindGameObjectsWithTag("Ruin"));

        print(ruinsList.Count);
    }

    public void RuinsFoundEvent()
    {
        foreach (GameObject ruin in ruinsList)
        {
            if (!ruin.activeInHierarchy)
            {

            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuinsManager : MonoBehaviour
{
    [SerializeField] TMP_Text ruinsDisplay;

    List<GameObject> ruinsList = new List<GameObject>();

    int ruinLeft;
    int initialCount;
    int ruinsFound;

    private void Start()
    {
        ruinsList = new List<GameObject>();

        ruinsList.AddRange(GameObject.FindGameObjectsWithTag("Ruin"));

        ruinLeft = ruinsList.Count;
        initialCount = ruinsList.Count;

        ruinsDisplay.text = 0 + " OUT OF " + initialCount;
    }

    public void RuinsFoundEvent()
    {
        foreach (GameObject ruin in ruinsList)
        {
            ruinLeft--;
            ruinsFound = initialCount - ruinLeft;
        }

        ruinsDisplay.text = ruinsFound + " OUT OF " + initialCount;
    }
}

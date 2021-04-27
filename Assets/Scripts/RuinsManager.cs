using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuinsManager : MonoBehaviour
{
    [SerializeField] GameEvent AllRuinsFound;
    
    [SerializeField] TMP_Text ruinsDisplay;

    List<GameObject> ruinsList = new List<GameObject>();

    int initialCount;
    int ruinsFound = 0;

    private void Start()
    {
        ruinsList = new List<GameObject>();

        ruinsList.AddRange(GameObject.FindGameObjectsWithTag("Ruin"));

        initialCount = ruinsList.Count;

        ruinsDisplay.text = 0 + " OUT OF " + initialCount;
    }

    public void RuinsFoundEvent()
    {
        ruinsFound++;

        ruinsDisplay.text = ruinsFound + " OUT OF " + initialCount;

        if (ruinsFound == initialCount)
        {
            AllRuinsFound.Raise();
        }
    }
}
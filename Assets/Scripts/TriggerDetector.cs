using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] GameEvent RuinsFoundEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Ruin")
            {
                gameObject.SetActive(false);

                RuinsFoundEvent.Raise();

                print("Ruins found event Raised");
            }
        }
    }
}
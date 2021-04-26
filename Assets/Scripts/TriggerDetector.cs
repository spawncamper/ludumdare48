using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    [SerializeField] GameEvent RuinsFoundEvent;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RuinsFoundEvent.Raise();
        }
    }
}
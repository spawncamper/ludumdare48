using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] GameEvent CollisionRockEvent;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            CollisionRockEvent.Raise();
        }
    }
}

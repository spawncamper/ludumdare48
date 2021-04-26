using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Rock")
        {
            print("collision");
        }
    }
}

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] ObjectPool[] objectPools;
    [SerializeField] int worldSize = 10;
    [SerializeField] float tileHeight = 9f;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Sets up the initial tiles in the world.
        for (int i = -5; i < worldSize - 5; i++)
        {
            GenerateTile(tileHeight * i);
        }
    }

    float DistanceToPlayer()
    {
        float distanceToPlayer = transform.position.y - player.transform.position.y;
        return distanceToPlayer;
    }

    public void DisableTile(GameObject obj)
    {
        //Return the tile to the pool
        obj.SetActive(false);

        //Create a new replacement tile for the far end of the level
        //GenerateTile(tileDepth * worldSize + (tileDespawner.transform.position.z + (2 * tileDepth)));
    }

    void GenerateTile(float yOffset)
    {
        //Select a tile from a random pool
        int selectedPool = Random.Range(0, objectPools.Length);

        GameObject poolObject = objectPools[selectedPool].EnableObjectInPool(0, yOffset, 0);

     /*   //If there was not availalbe tile in the pool, naively select the first one you can find from another pool
        if (poolObject == null)
        {
            foreach (ObjectPool pool in objectPools)
            {                
                poolObject = pool.EnableObjectInPool(yOffset);

                float yOffset +

                if (poolObject != null) { break; }
            }
        } */
    }
}
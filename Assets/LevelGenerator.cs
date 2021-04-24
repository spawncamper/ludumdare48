using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int worldSize = 10;
    [SerializeField] float tileHeight = 1f;
    [SerializeField] int poolSize;
    [SerializeField] GameObject objectPrefab;

    GameObject[] activePool;

    GameObject player;

    private void Awake()
    {
        activePool = PopulatePool();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Sets up the initial tiles in the world.
        for (int i = - Mathf.RoundToInt(worldSize/2); i < Mathf.RoundToInt(worldSize / 2); i++)
        {
            GenerateTile(tileHeight * i);
        }
    }

    private void Update()
    {
        
    }

    float DistanceToPlayer()
    {
        float distanceToPlayer = transform.position.y - player.transform.position.y;
        return distanceToPlayer;
    }

    void GenerateTile(float yOffset)
    {
        //Select a tile from a random pool
        int selectedPool = Random.Range(0, activePool.Length);

        GameObject poolObject = EnableObjectInPool(0, yOffset, 0);

        if (poolObject == null)
        {
            Debug.LogError("[LevelGenerator] GenerateTile poolObject == null");
        }    
    }

    GameObject[] PopulatePool()
    {
        //Create the pool
        activePool = new GameObject[poolSize];

        //Instantiate the objects into the pool and disable them
        for (int i = 0; i < activePool.Length; i++)
        {
            activePool[i] = Instantiate(objectPrefab, transform);
            activePool[i].SetActive(false);
        }

        return activePool;
    }

    public GameObject EnableObjectInPool(float xOffset, float yOffset, float zOffset)
    {
        //Find the next available object in the pool and try to enable it
        for (int i = 0; i < activePool.Length; i++)
        {
            if (!activePool[i].activeInHierarchy)
            {
                activePool[i].transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);
                activePool[i].SetActive(true);
                return activePool[i];
            }
        }
        return null;
    }
}
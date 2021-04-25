using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int worldSize = 10;
    [SerializeField] float tileHeight = 1f;
    [SerializeField] int poolSize;
    [SerializeField] GameObject[] tilePrefabs;
    [SerializeField] GameObject[] minePrefabs;

    float zeroPosition;

    GameObject[] activePool;
    GameObject[] tilePool;

    GameObject player;

    private void Awake()
    {
        activePool = PopulatePool(poolSize, tilePrefabs);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("[LevelGenerator] Start() player == null");
        }
        
        //Sets up the initial tiles in the world.
        for (int i = -worldSize; i < 0; i++)
        {
            GenerateTile(0, tileHeight * i, 0);
        }

        zeroPosition = transform.position.y;
    }

    private void Update()
    {
        float distanceToPlayer = zeroPosition - player.transform.position.y;
        
        if (distanceToPlayer > tileHeight)
        {
            zeroPosition = player.transform.position.y;

            UpdateTilePositions(zeroPosition);
        }
    }

    void UpdateTilePositions(float zeroPosition)
    {
        foreach (GameObject tile in activePool)
        {
            float distanceToPlayer = player.transform.position.y - tile.transform.position.y;

            if (distanceToPlayer < 0 && tile.activeInHierarchy)
            {
                tile.SetActive(false);

                GenerateTile(0, Mathf.Round(zeroPosition - tileHeight * worldSize), 0);
            }
        }
    }

    // Enables a random object in the pool
    void GenerateTile(float xOffset, float yOffset, float zOffset)
    {
        //Select a tile from a random pool
        int selectedPool = Random.Range(0, activePool.Length);

        GameObject poolObject = EnableObjectInPool(xOffset, yOffset, zOffset);

        if (poolObject == null)
        {
            Debug.LogError("[LevelGenerator] GenerateTile poolObject == null");
        }    
    }

    GameObject[] PopulatePool(int _poolSize, GameObject[] prefabs)
    {
        //Create the pool
        activePool = new GameObject[_poolSize];

        //Instantiate the objects into the pool and disable them
        for (int i = 0; i < activePool.Length; i++)
        {
            activePool[i] = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
            activePool[i].SetActive(false);
        }

        return activePool;
    }

    // Goes through each object in pool, if not active, then SetActive at given offset
    public GameObject EnableObjectInPool(float xOffset, float yOffset, float zOffset)
    {
        //Find the next available object in the pool and try to enable it
        for (int i = 0; i < activePool.Length; i++)
        {
            if (!activePool[i].activeInHierarchy)
            {
                activePool[i].transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);
                activePool[i].SetActive(true);

                print(i + activePool[i].transform.position.y);

                return activePool[i];
            }
        }
        return null;
    }
}
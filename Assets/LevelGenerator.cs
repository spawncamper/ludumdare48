using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] int worldSize = 10;
    [SerializeField] float tileHeight = 1f;
    [SerializeField] int tilePoolSize;
    [SerializeField] int minePoolSize;
    [SerializeField] GameObject[] tilePrefabs;
    [SerializeField] GameObject[] minePrefabs;

    float zeroPosition;

    GameObject[] tilePool;
    GameObject[] minePool;

    GameObject player;

    private void Awake()
    {
        tilePool = PopulatePool(tilePoolSize, tilePrefabs);
        minePool = PopulatePool(minePoolSize, minePrefabs);
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
            EnableObjectInPool(tilePool, 0, tileHeight * i, 0);
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
        foreach (GameObject tile in tilePool)
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
        GameObject poolObject = EnableObjectInPool(tilePool, xOffset, yOffset, zOffset);

        if (poolObject == null)
        {
            Debug.LogError("[LevelGenerator] GenerateTile poolObject == null");
        }    
    }

    void GenerateMine(float xOffset, float yOffset, float zOffset)
    {
        GameObject poolObject = EnableObjectInPool(minePool, xOffset, yOffset, zOffset);

        if (poolObject == null)
        {
            Debug.LogError("[LevelGenerator] GenerateTile poolObject == null");
        }
    }

    GameObject[] PopulatePool(int _poolSize, GameObject[] prefabs)
    {
        //Create the pool
        GameObject[] _Pool = new GameObject[_poolSize];

        //Instantiate the objects into the pool and disable them
        for (int i = 0; i < _Pool.Length; i++)
        {
            _Pool[i] = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
            _Pool[i].SetActive(false);
        }

        return _Pool;
    }

    // Goes through each object in pool, if not active, then SetActive at given offset
    public GameObject EnableObjectInPool(GameObject[] _Pool, float xOffset, float yOffset, float zOffset)
    {
        //Find the next available object in the pool and try to enable it
        for (int i = 0; i < _Pool.Length; i++)
        {
            if (!_Pool[i].activeInHierarchy)
            {
                _Pool[i].transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);
                _Pool[i].SetActive(true);

                print(i + _Pool[i].transform.position.y);

                return _Pool[i];
            }
        }

        Debug.LogError("[LevelGenerator] EnableObjectInPool returns null");

        return null;
    }
}
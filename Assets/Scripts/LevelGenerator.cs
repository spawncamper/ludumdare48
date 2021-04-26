using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] int worldSize = 10;
    [SerializeField] float tileHeight = 1f;
    [SerializeField] int tilePoolSize;
    [SerializeField] int minePoolSize;
    [SerializeField] GameObject[] tilePrefabs;
    [SerializeField] GameObject[] minePrefabs;

    [Range(0.0f, 100.0f)]
    [SerializeField] float mineSpawnChance = 20f;
    [SerializeField] float offsetMin = -20f;
    [SerializeField] float offsetMax = 20f;

    float zeroPosition;
    #endregion

    [SerializeField] GameEvent MineSpawnedEvent;

    #region GAME OBJECTS
    GameObject[] tilePool;
    GameObject[] minePool;

    GameObject player;
    #endregion

    #region UNITY GAME LOOP
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
    #endregion

    #region TILES
    void UpdateTilePositions(float zeroPosition)
    {
        foreach (GameObject tile in tilePool)
        {
            float distanceToPlayer = player.transform.position.y - tile.transform.position.y;

            if (distanceToPlayer < 0 && tile.activeInHierarchy)
            {
                // Disable the topmost tile
                tile.SetActive(false);

                // Enable a tile on the bottom
                EnableObjectInPool(tilePool, 0, Mathf.Round(zeroPosition - tileHeight * worldSize), 0);

                //Spawn a mine at the bottom
                SpawnMine(zeroPosition);
                MineSpawnedEvent.Raise();
            }
        }
    }
    #endregion

    #region MINES
    void SpawnMine(float _zeroPosition)
    {
        float spawnChance = Random.Range(0, 100);

        float xOffset = Random.Range(offsetMin, offsetMax);
        float zOffset = Random.Range(offsetMin, offsetMax);

        if (spawnChance < mineSpawnChance)
        {
            EnableObjectInPool(minePool, xOffset, Mathf.Round(_zeroPosition - tileHeight * worldSize), zOffset);
        }
    }

    //Called by MineSpawnedEvent
    public void UpdateMines()
    {
        foreach (GameObject mine in minePool)
        {
            float distanceToPlayer = player.transform.position.y - mine.transform.position.y;

            if (distanceToPlayer < 0 && mine.activeInHierarchy)
            {
                // Disable the topmost tile
                mine.SetActive(false);
            }
        }
    }
    #endregion

    #region OBJECT POOLING
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

        Debug.LogError("[LevelGenerator] EnableObjectInPool returns null in pool " + _Pool);

        return null;
    }
    #endregion
}
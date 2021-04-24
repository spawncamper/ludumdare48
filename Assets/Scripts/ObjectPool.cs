using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] int poolSize;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    public GameObject EnableObjectInPool(float xOffset, float yOffset, float zOffset)
    {
        //Find the next available object in the pool and try to enable it
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null;
    }

    public void DisableObjectInPool(GameObject obj)
    {
        obj.SetActive(false);
    }


    void PopulatePool()
    {
        //Create the pool
        pool = new GameObject[poolSize];

        //Instantiate the objects into the pool and disable them
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(objectPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    //Called by Unity
    void OnValidate()
    {
        //Prevent the designer from entering an invalid pool size
        if (poolSize < 0) { poolSize = 0; }
    }
}
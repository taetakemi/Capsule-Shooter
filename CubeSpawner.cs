using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static CubeSpawner Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Pool Cube;
    List<GameObject> _cubePool;

    public float defautSpawnTime;
    float time;
    [Range(-50f,50f)]
    public float minHorizontalRange;
    [Range(-50f,50f)]
    public float maxHorizontalRange;

    private void Start()
    {
        _cubePool = new List<GameObject>();
        //List<GameObject> objectPool = new List<GameObject>();
        for (int i = 0; i < Cube.size; i++)
        {
            GameObject obj = Instantiate(Cube.prefab);
            obj.SetActive(false);
            cubePool.Add(obj);
        }
        time = defautSpawnTime;
        if (maxHorizontalRange < minHorizontalRange)
        {
            maxHorizontalRange = minHorizontalRange;
        }
        //cubePool.Add(pool.tag, objectPool);
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time < 0f)
        {
            SpawnCube(
                new Vector3(
                Random.Range(minHorizontalRange, maxHorizontalRange), 
                10f, 
                Random.Range(minHorizontalRange, maxHorizontalRange)), 
                Quaternion.identity);
            time = defautSpawnTime;
        }
    }

    public GameObject SpawnCube(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < cubePool.Count; i++)
        {
            GameObject tempCube = cubePool[i];
            if (!cubePool[i].activeInHierarchy)
            {
                tempCube.transform.position = position;
                tempCube.transform.rotation = rotation;
                tempCube.SetActive(true);
                tempCube.GetComponent<Cube>().cubeCreated();
                return tempCube;
            }
        }
        return null;
    }

    public void DestroyCube(GameObject cube)
    {
        for (int i = 0; i < cubePool.Count; i++)
        {
            if (cubePool[i].Equals(cube))
            {
                cubePool[i].SetActive(false);
            }
        }
    }

    public List<GameObject> cubePool { get { return _cubePool; } }
}

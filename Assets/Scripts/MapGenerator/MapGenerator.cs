using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    [System.Serializable]
    public struct MapObject
    {
        public GameObject prefab;
        public Vector2Int size;
    }

    [SerializeField] Vector2Int mapSize = new Vector2Int(100, 100);
    [SerializeField] float cellSize = 100;
    [SerializeField] MapObject[] prefabs;


    void Start()
    {
        transform.localScale = new Vector3(mapSize.x, 1, mapSize.y);

        int spawnCount = mapSize.x * mapSize.y;

        Vector3 spawnPoint = transform.position - new Vector3(mapSize.x * cellSize / 2, 0, mapSize.y * cellSize / 2);

        for (int i = 0; i < spawnCount; ++i)
        {
            Vector2Int position = new Vector2Int(i % mapSize.x, i / mapSize.x);
            MapObject obj = prefabs[Random.Range(0, prefabs.Length)];

            Vector3 spwanPosition = spawnPoint + new Vector3(position.x * cellSize, 0, position.y * cellSize);
            Instantiate(obj.prefab, spwanPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}

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
    [SerializeField] uint maxBuildingCount = 0;
    [SerializeField] uint maxSpawnAttemptCount = 0;
    [SerializeField] MapObject[] prefabs;


    void Start()
    {
        transform.localScale = new Vector3(mapSize.x, 1, mapSize.y);

        bool[,] map = new bool[mapSize.x, mapSize.y];

        uint bildingSpawnCount = 0;
        uint attemptCount = 0;

        while(bildingSpawnCount < maxBuildingCount && attemptCount < maxSpawnAttemptCount)
        {
            Vector2Int position = new Vector2Int(Random.Range(0, mapSize.x), Random.Range(0, mapSize.y));
            MapObject obj = prefabs[Random.Range(0, prefabs.Length)];

            if(CheckBuildingCollision(map, position, obj.size))
            {
                ++attemptCount;
                continue;
            }
           
            attemptCount = 0;
            WriteBuildingToMap(map, position, obj.size);

            Vector3 spwanPosition = transform.position + new Vector3(position.x * cellSize, 0, position.y * cellSize);
            spwanPosition = spwanPosition + new Vector3(obj.size.x / 2 * cellSize, 0, obj.size.y / 2 * cellSize);
            Instantiate(obj.prefab, spwanPosition, Quaternion.identity);
            ++bildingSpawnCount;
        }
    }

    bool CheckBuildingCollision(bool[,] map, Vector2Int position, Vector2Int size)
    {

        if (position.x + size.x >= mapSize.x || position.y + size.y >= mapSize.y)
            return true;

        int i = 0;
        int j = 0;
        int iMax = size.y;
        int jMax = size.x;

        if (position.x > 0)
            j = -1;

        if (position.x + size.x < mapSize.x - 1)
            ++jMax;

        if (position.y > 0)
            i = -1;

        if (position.y + size.y < mapSize.y - 1)
            ++iMax;

        for (; i < iMax; ++i)
        {
            for (; j < jMax; ++j)
            {
                Vector2Int currentPosition = position + new Vector2Int(j, i);
                if (map[currentPosition.y, currentPosition.x])
                {
                    return true;
                }
            }
        }

        return false;
    }

    void WriteBuildingToMap(bool[,] map, Vector2Int position, Vector2Int size)
    {
        for (int i = 0; i < size.y; ++i)
        {
            for (int j = 0; j < size.x; ++j)
            {
                Vector2Int currentPosition = position + new Vector2Int(j, i);
                map[currentPosition.y, currentPosition.x] = true;
            }
        }
    }

    void Update()
    {
        
    }
}

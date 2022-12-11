using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{

    [SerializeField] Vector2Int mapSize = new Vector2Int(100, 100);
    [SerializeField] float cellSize = 100;
    [SerializeField] GameObject[] roadPrefabs;
    [SerializeField] GameObject[] housePrefabs;
    [SerializeField] GameObject[] prefabs;

    [SerializeField] int minKeyPointCount = 5;
    [SerializeField] int maxKeyPointCount = 10;

    enum TileType
    {
        EMPTY,
        ROAD
    }

    class TileInfo
    {
        public TileInfo()
        {
            tileType = TileType.EMPTY;
            tileCode = 0;
            angle = 0;
        }

        public TileType tileType;
        public int tileCode;
        public float angle;
    }

    void Start()
    {
        transform.localScale = new Vector3(mapSize.x, 1, mapSize.y);

        List<Vector2Int> keyPoints = GenerateKeyPoints();
        bool[,] roadmap = GenerateRoadMap(keyPoints);
        TileInfo[,] levelMap = new TileInfo[mapSize.x, mapSize.y];
        
        for(int i = 0; i < mapSize.x; ++i)
        {
            for (int j = 0; j < mapSize.y; ++j)
            {
                levelMap[i,j] = new TileInfo();

                if (roadmap[i,j])
                {
                    int neighborCount = 0;
                    bool U = false;
                    bool D = false;
                    bool L = false;
                    bool R = false;

                    if (i - 1 > 0 && roadmap[i - 1, j])
                    {
                        ++neighborCount;
                        L = true;
                    }

                    if (i + 1 < mapSize.x - 1 && roadmap[i + 1, j])
                    {
                        ++neighborCount;
                        R = true;
                    }

                    if (j - 1 > 0 && roadmap[i, j - 1])
                    {
                        ++neighborCount;
                        U = true;
                    }

                    if (j + 1 < mapSize.y - 1 && roadmap[i, j + 1])
                    {
                        ++neighborCount;
                        D = true;
                    }

                    TileInfo tileInfo = levelMap[i, j];
                    tileInfo.tileCode = 1;
                    tileInfo.tileType = TileType.ROAD;
                    switch (neighborCount)
                    {
                        case 1:
                            tileInfo.tileCode = 0;
                            tileInfo.tileType = TileType.ROAD;

                            if (U)
                                tileInfo.angle = 180;

                            if (L)
                                tileInfo.angle = 90;

                            if (R)
                                tileInfo.angle = -90;

                            break;
                        case 2:
                            if(U && D || L && R)
                                tileInfo.tileCode = 1;
                            else
                                tileInfo.tileCode = 2;

                            if (tileInfo.tileCode == 1 && U && D)
                                tileInfo.angle = 90;

                            if (tileInfo.tileCode == 2 && L && D)
                                tileInfo.angle = 180;

                            if (tileInfo.tileCode == 2 && D && R)
                                tileInfo.angle = 90;

                            if (tileInfo.tileCode == 2 && U && L)
                                tileInfo.angle = 270;

                            tileInfo.tileType = TileType.ROAD;
                            break;
                        case 3:
                            tileInfo.tileCode = 3;
                            tileInfo.tileType = TileType.ROAD;


                            if (!L)
                                tileInfo.angle = 90;

                            if (!U)
                                tileInfo.angle = 180;

                            if (!R)
                                tileInfo.angle = 270;

                            break;
                        case 4:
                            tileInfo.tileCode = 4;
                            tileInfo.tileType = TileType.ROAD;
                            break;
                    }
                }
            }
        }

        String str = "";
        for (int i = 0; i < mapSize.x; ++i)
        {
            for (int j = 0; j < mapSize.y; ++j)
            {
                if (roadmap[j, i])
                    str += "*";
                else
                    str += " ";
            }
            str += "\n";
        }

        for (int i = 0; i < mapSize.x; ++i)
        {
            for (int j = 0; j < mapSize.y; ++j)
            {
                TileInfo tileInfo = levelMap[i, j];

                GameObject obj = null;

                switch (tileInfo.tileType)
                {
                    case TileType.ROAD:
                        obj = roadPrefabs[tileInfo.tileCode];
                        break;
                    case TileType.EMPTY:

                        if(UnityEngine.Random.Range(0, 10) > 1)
                            obj = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
                        else
                            obj = housePrefabs[UnityEngine.Random.Range(0, housePrefabs.Length)];
                        break;
                }

                Vector3 spwanPosition = new Vector3(i * cellSize, 0, (mapSize.x - 1 - j) * cellSize);
                GameObject newObject = Instantiate(obj, spwanPosition, Quaternion.identity);
                
                if(tileInfo.tileType != TileType.EMPTY)
                    newObject.transform.Rotate(new Vector3(0, tileInfo.angle, 0));
                else
                {
                    newObject.transform.Rotate(new Vector3(0, 90 * UnityEngine.Random.Range(0, 4), 0));
                }

            }
        }

        GetComponent<CoinSpawner>().FindCoinSpawnPoints();
    }

    List<Vector2Int> GenerateKeyPoints()
    {
        List<Vector2Int> pointList = new List<Vector2Int>();
        int keyPointCount = UnityEngine.Random.Range(minKeyPointCount, maxKeyPointCount + 1);

        for(int i = 0; i < keyPointCount; ++i)
        {
            RND_KEY_POINT:
            Vector2Int keyPointPosition = new Vector2Int(UnityEngine.Random.Range(0, mapSize.x), UnityEngine.Random.Range(0, mapSize.y));

            foreach (var keyPoint in pointList)
            {
                if (keyPoint == keyPointPosition)
                {
                    goto RND_KEY_POINT;
                }
            }

            pointList.Add(keyPointPosition);
        }

        return pointList;
    }

    bool[,] GenerateRoadMap(List<Vector2Int> keyPoints)
    {
        List<int> keyPointValue = new List<int>();

        for (int i = 0; i < keyPoints.Count; ++i)
        {
            keyPointValue.Add(UnityEngine.Random.Range(1, 4));
        }

        bool[,] roadMap = new bool[mapSize.x, mapSize.y];

        for (int i = 0; i < keyPoints.Count; ++i)
        {
            while (keyPointValue[i] > 0)
            {
                int targetKeyPoint = UnityEngine.Random.Range(0, keyPoints.Count);

                while (targetKeyPoint == i)
                {
                    targetKeyPoint = UnityEngine.Random.Range(0, keyPoints.Count);
                }

                --keyPointValue[i];

                if (keyPointValue[targetKeyPoint] > 0)
                {
                    --keyPointValue[targetKeyPoint];

                    Vector2Int startPoint = keyPoints[i];
                    Vector2Int endPoint = keyPoints[targetKeyPoint];
                    Vector2Int direction = endPoint - startPoint;
                    int xDir = 0;
                    int yDir = 0;

                    if (direction.x != 0)
                        xDir = direction.x / Math.Abs(direction.x);
                    if (direction.y != 0)
                        yDir = direction.y / Math.Abs(direction.y);

                    Vector2Int currentPoint = startPoint;

                    bool xGy = Math.Abs(direction.x) > Math.Abs(direction.y);

                    if (xGy)
                    {
                        while (currentPoint.x != endPoint.x)
                        {
                            roadMap[currentPoint.x, currentPoint.y] = true;
                            currentPoint.x += xDir;
                        }

                        while (currentPoint.y != endPoint.y)
                        {
                            roadMap[currentPoint.x, currentPoint.y] = true;
                            currentPoint.y += yDir;
                        }
                    }
                    else
                    {
                        while (currentPoint.y != endPoint.y)
                        {
                            roadMap[currentPoint.x, currentPoint.y] = true;
                            currentPoint.y += yDir;
                        }

                        while (currentPoint.x != endPoint.x)
                        {
                            roadMap[currentPoint.x, currentPoint.y] = true;
                            currentPoint.x += xDir;
                        }
                    }
                }
            }
        }


        return roadMap;
    }

    void Update()
    {
        
    }
}

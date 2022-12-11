using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    [SerializeField] private int maxCoinCount = 1000;
    [SerializeField] private float minSpawnDistance = 100;
    [SerializeField] private GameObject coinPrefab = null;


    List<Transform> spawnPointList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
    }
    
    public void FindCoinSpawnPoints()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("CoinSpawner");

        foreach (GameObject spawnPoint in spawnPoints)
        {
            spawnPointList.Add(spawnPoint.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnPointList.Count == 0)
            return;

        if(GameObject.FindGameObjectsWithTag("Coin").Length < maxCoinCount)
        {
            Instantiate(coinPrefab, spawnPointList[UnityEngine.Random.Range(0, spawnPointList.Count)].position, Quaternion.identity);
        }
    }
}

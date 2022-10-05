using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] int maxSpikes;
    [SerializeField] int maxCoins;
    [SerializeField] Transform spikePrefab;
    [SerializeField] Transform coinPrefab;
    [SerializeField] Transform playerPrefab;
    private PlayerGameplay player;

    public void SpawnNewGame()
    {
        player = SpawnPlayer();
        
        for (int i = 0; i < maxSpikes; i++)
        {
            SpawnEnemy();
        }

        for (int i = 0; i < maxCoins; i++)
        {
            SpawnCoin();
        }     

    }

    private PlayerGameplay SpawnPlayer()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerGameplay>();
    }

    private void SpawnCoin()
    {
        throw new NotImplementedException();
    }

    private void SpawnEnemy()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)));
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)));
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)));
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)));
    }
}

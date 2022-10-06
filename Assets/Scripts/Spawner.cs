using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] int maxSpikes;
    [SerializeField] int maxCoins;
    [SerializeField] Enemy spikePrefab;
    [SerializeField] Item coinPrefab;
    [SerializeField] Transform playerPrefab;
    [SerializeField] private int minDistance;
    [SerializeField] private TextMeshProUGUI gameText;
    [SerializeField] private Button startGameButton;
    [SerializeField] private TextMeshProUGUI coinText;
    private Player player;
    private List<Enemy> enemies;
    private List<Item> items;

    private float minX, maxX, minY, maxY;
    private const int maxIterations = 100;

    private void Awake()
    {
        enemies = new List<Enemy>();
        items = new List<Item>();

        Transform playerTransform = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
        player = playerTransform.GetComponent<Player>();
        player.gameObject.SetActive(false);
    }

    public void SpawnNewGame()
    {
        SetMinMaxXY();
        DeleteGameObjectsAndClearList(enemies);
        DeleteGameObjectsAndClearList(items);

        
        player.gameObject.SetActive(true);
        for (int i = 0; i < maxSpikes; i++)
        {
            enemies.Add(SpawnObject<Enemy>(spikePrefab.transform));
        }
        for (int i = 0; i < maxCoins; i++)
        {
            items.Add(SpawnObject<Item>(coinPrefab.transform));
        }
        maxCoins = items.Count;
        player.FirstInitialization(startGameButton, gameText, coinText, maxCoins);
    }

    private void DeleteGameObjectsAndClearList<T>(List<T> objectList) where T : MonoBehaviour
    {
        foreach (T mapItem in objectList)
        {
            Destroy(mapItem.gameObject);
        }
        objectList.Clear();
    }

    private void SetMinMaxXY()
    {
        Vector3 leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightUp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = leftDown.x;
        minY = leftDown.y;
        maxX = rightUp.x;
        maxY = rightUp.y;
    }

    private T SpawnObject<T>(Transform objectPrefab)
    {
        int i = 0;
        while (i < maxIterations)
        {
            float spawnX, spawnY;
            spawnX = Random.Range(minX, maxX);
            spawnY = Random.Range(minY, maxY);
            Vector2 spawnPoint = new Vector2(spawnX, spawnY);
            if (CheckSpawnPossibility(spawnPoint))
            {
                return Instantiate(objectPrefab, spawnPoint, Quaternion.identity).GetComponent<T>();
            }
            i++;
        }
        throw new Exception("NO_PLACE_FOR_SPAWN_EXCEPTION");
    }

    private bool CheckSpawnPossibility(Vector2 spawnPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint, minDistance);
        if (colliders.Length == 0)
        {
            return true;
        }
        return false;
    }
}

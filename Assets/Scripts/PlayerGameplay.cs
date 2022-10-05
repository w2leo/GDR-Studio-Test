using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameplay : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask itemsLayer;
    [SerializeField] private PlayerLine line;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI coinText;

    public bool GameIsActive { get; private set; }
    private int coins;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        coinText.text = coins.ToString();
    }

    public void StartGame()
    {
        GameIsActive = true;
        coins = 0;
        // Update UI text (coins)
        restartButton.gameObject.SetActive(false);
        endGameText.gameObject.SetActive(false);
        line.Initialize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        var collisionLayer = (1 << collision.gameObject.layer);

        if ((collisionLayer & itemsLayer) != 0)
        {
            CollectCoin(collision);

        }
        else if ((collisionLayer & enemyLayer) != 0)
        {
            TakeDamage();
        }

    }

    private void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        Destroy(coinCollider.gameObject);
        Debug.Log($"Coins = {coins}");
        // Update UI text (coins)

        if (coins == 6)
        {
            WinGame();
        }
    }

    private void TakeDamage()
    {
        Debug.Log("Game Over");
        LooseGame();
    }

    private void WinGame()
    {
        Debug.Log("Win Game");
        GameIsActive = false;
        GetComponent<PlayerMovement>().Initialize();
        line.Initialize();

        restartButton.gameObject.SetActive(true);
        endGameText.color = Color.green;
        endGameText.text = "YOU WIN!";
        endGameText.gameObject.SetActive(true);
    }

    private void LooseGame()
    {
        Debug.Log("Loose Game");
        GameIsActive = false;
        GetComponent<PlayerMovement>().Initialize();
        line.Initialize();

        restartButton.gameObject.SetActive(true);
        endGameText.color = Color.red;
        endGameText.text = "GAME OVER";
        endGameText.gameObject.SetActive(true);
    }
}

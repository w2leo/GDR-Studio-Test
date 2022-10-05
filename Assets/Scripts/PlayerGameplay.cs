using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplay : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask itemsLayer;

    private int coins;

    void StartGame(int maxHealth)
    {
        coins = 0;
        // Update UI text (coins)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        var collisionLayer = (1 << collision.gameObject.layer);

        if ((collisionLayer & itemsLayer) !=0)
        {
            Debug.Log("Item Triggered");
            CollectCoin(collision);
        }
        else if ((collisionLayer & enemyLayer) != 0)
        {
            Debug.Log("Enemy Triggered");
            TakeDamage();
        }
    }

    void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        Destroy(coinCollider.gameObject);
        Debug.Log($"Coins = {coins}");
        // Update UI text (coins)
    }

    void TakeDamage()
    {
        Debug.Log("Game Over");
        //game over animation
        // restart game
    }

}

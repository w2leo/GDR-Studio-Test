using UnityEngine;

public enum EndGameStatus
{
    Win, Loose
};

[RequireComponent(typeof(PlayerMovement))]
public class PlayerGameplay : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask itemsLayer;
    [SerializeField] private PlayerLine playerLine;
    private PlayerMovement playerMovement;
    [SerializeField] private Player player;

    public int Coins { get; private set; }

    public bool GameIsActive { get; private set; }

    private int maxCoins;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void StartGame(int maxCoins)
    {
        GameIsActive = true;
        Coins = 0;
        this.maxCoins = maxCoins;
        transform.position = Vector3.zero;
        playerLine.Initialize();
        playerMovement.Initialize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        Coins++;
        coinCollider.gameObject.SetActive(false);
        Debug.Log($"Coins = {Coins}");
        if (Coins == maxCoins)
        {
            player.EndGame(EndGameStatus.Win);
        }
    }

    private void TakeDamage()
    {
        player.EndGame(EndGameStatus.Loose);
    }

    private void EndGameplay(EndGameStatus status)
    {
        GameIsActive = false;
        playerLine.StopLine();
        playerMovement.Initialize();
        player.EndGame(EndGameStatus.Win);
    }
}

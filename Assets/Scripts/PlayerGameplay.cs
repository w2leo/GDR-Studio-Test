using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerGameplay : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask itemsLayer;
    [SerializeField] private PlayerLine playerLine;
    [SerializeField] private Player player;
    [SerializeField] Transform explosion;
    private PlayerMovement playerMovement;
    private int maxCoins;
    private ParticleSystem expl;

    public int Coins { get; private set; }

    public bool GameIsActive { get; private set; }

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
        expl = Instantiate(explosion, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();

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
        if (Coins == maxCoins)
        {
            EndGameplay(EndGameStatus.Win);
        }
    }

    private void TakeDamage()
    {
        EndGameplay(EndGameStatus.Loose);
    }

    private void EndGameplay(EndGameStatus status)
    {
        GameIsActive = false;
        PlayExplosion();
        playerLine.StopLine();
        playerMovement.Initialize();
        player.EndGame(status);
    }

    private void PlayExplosion()
    {
        expl.transform.position = transform.position;
        expl.Play();
    }
}

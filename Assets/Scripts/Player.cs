using System;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerGameplay playerGameplay;
    private TextMeshProUGUI gameText;
    private Button startGameButton;
    private TextMeshProUGUI coinText;
    private EndGameUIElements winGameData, looseGameData;
    private Dictionary<EndGameStatus, EndGameUIElements> endGameDataPairs;

    private void Start()
    {
        winGameData = new EndGameUIElements("YOU WIN", Color.green);
        looseGameData = new EndGameUIElements("YOU LOOSE. TRY AGAIN", Color.red);
        endGameDataPairs = new Dictionary<EndGameStatus, EndGameUIElements>();
        endGameDataPairs.Add(EndGameStatus.Win, winGameData);
        endGameDataPairs.Add(EndGameStatus.Loose, looseGameData);  
    }

    private void Update()
    {
        coinText.text = playerGameplay.Coins.ToString();
    }

    public void FirstInitialization(Button button, TextMeshProUGUI textUI, TextMeshProUGUI coinTextUI,int maxCoins)
    {
        startGameButton = button;
        gameText = textUI;
        coinText = coinTextUI; 
        GameInitialization(maxCoins);
    }

    private void GameInitialization(int maxCoins)
    {
        playerGameplay.StartGame(maxCoins);
        startGameButton.gameObject.SetActive(false);
        gameText.gameObject.SetActive(false);
        transform.gameObject.SetActive(true);
    }

    public void EndGame(EndGameStatus status)
    {
        transform.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(true);
        gameText.color = endGameDataPairs[status].endGameColor;
        gameText.text = endGameDataPairs[status].endGameMessage;
        gameText.gameObject.SetActive(true);
    }
}

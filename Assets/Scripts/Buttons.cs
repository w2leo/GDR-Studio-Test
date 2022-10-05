using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] PlayerGameplay player;
    private void ClickRestart()
    {
        player.StartGame();
    }
}

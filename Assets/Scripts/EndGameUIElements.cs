using UnityEngine;

public struct EndGameUIElements
{
    public string endGameMessage;
    public Color endGameColor;

    public EndGameUIElements(string endGameMessage, Color endGameColor)
    {
        this.endGameMessage = endGameMessage;
        this.endGameColor = endGameColor;
    }
}

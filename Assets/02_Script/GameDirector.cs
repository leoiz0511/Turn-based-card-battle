using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Player, AI }

public class GameDirector : MonoBehaviour
{
    public int playerWinStreak = 0;
    public int aiWinStreak = 0;
    public int turnCount = 1;

    public bool IsDoubleScoreTurn => (turnCount % 5 == 0);

    public void CompareCards(Card playerCard, Card aiCard)
    {
        int roundScore = IsDoubleScoreTurn ? 2 : 1;

        if (playerCard.RankValue > aiCard.RankValue)
        {
            HandleWin(Side.Player, roundScore);
        }
        else if (playerCard.RankValue < aiCard.RankValue)
        {
            HandleWin(Side.AI, roundScore);
        }
        else
        {
            HandleDraw();
        }

        turnCount++;
        CheckGameOver();
    }

    void HandleWin(Side winner, int baseScore)
    {
        if (winner == Side.Player)
        {
            playerWinStreak++;
            aiWinStreak = 0;

            int totalGain = baseScore + (playerWinStreak >= 2 ? 1 : 0);
            AddScore(Side.Player, totalGain);
        }
        else
        {
            aiWinStreak++;
            playerWinStreak = 0;

            int totalGain = baseScore + (aiWinStreak >= 2 ? 1 : 0);
            AddScore(Side.AI, totalGain);
        }
    }

    void HandleDraw()
    {
        playerWinStreak = 0;
        aiWinStreak = 0;
        Debug.Log("Draw!");
    }

    void AddScore(Side side, int amount)
    {
        // 실제 점수 관리 시스템 연결
    }

    void CheckGameOver()
    {
        // 덱 + 손패 체크
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image suitImage;
    public TMPro.TextMeshProUGUI rankText;

    private Card cardData;

    public void SetCard(Card card)
    {
        cardData = card;

        if (rankText != null)
            rankText.text = card.rank.ToString();

        int spriteIndex = GetSpriteIndex(card);

        Sprite loadedSprite = Resources.Load<Sprite>("Cards/Deck01_" + spriteIndex);

        if (loadedSprite != null)
        {
            suitImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("스프라이트 못 찾음: Deck01_" + spriteIndex);
        }
    }

    int GetSpriteIndex(Card card)
    {
        // 조커
        if (card.rank == Rank.Joker)
            return 56;

        int suitStart = 1;

        switch (card.suit)
        {
            case Suit.Heart: suitStart = 1; break;
            case Suit.Clover: suitStart = 15; break;
            case Suit.Diamond: suitStart = 28; break;
            case Suit.Spade: suitStart = 42; break;
        }

        int rankIndex = ConvertRank(card.rank);

        return suitStart + (rankIndex - 1);
    }

    int ConvertRank(Rank rank)
    {
        if (rank == Rank.Ace)
            return 1;

        return (int)rank; // R2=2, R3=3 ... K=13
    }

    public Card GetCard()
    {
        return cardData;
    }
}
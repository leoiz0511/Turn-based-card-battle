using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public enum Suit { Spade, Heart, Diamond, Clover, None }
public enum Rank
{
    R2 = 2, R3, R4, R5, R6, R7, R8, R9, R10,
    J, Q, K, Ace, Joker
}

[System.Serializable]
public class Card
{
    public Suit suit;
    public Rank rank;
    public int RankValue => (int)rank; //

    public Card(Suit s, Rank r)
    {
        suit = s;
        rank = r;
    }

    public override string ToString()
    {
        if (rank == Rank.Joker) return "Joker";
        return $"{suit} {rank}";
    }
}

// 실제 덱 로직 (기존 로직 유지)
public class Deck
{
    private List<Card> deck = new List<Card>();
    public int Count => deck.Count;

    public void CreateDeck()
    {
        deck.Clear();
        // 일반 카드 생성
        foreach (Suit suit in new[] { Suit.Spade, Suit.Heart, Suit.Diamond, Suit.Clover })
        {
            foreach (Rank rank in System.Enum.GetValues(typeof(Rank)))
            {
                if (rank == Rank.Joker) continue;
                deck.Add(new Card(suit, rank));
            }
        }
        // Joker 추가
        deck.Add(new Card(Suit.None, Rank.Joker));
        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(i, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (deck.Count == 0) return null;
        Card card = deck[0];
        deck.RemoveAt(0);
        return card;
    }
}

// 유니티 컴포넌트
public class DeckManager : MonoBehaviour
{
    public Deck deck = new Deck(); // 선언과 동시에 할당하여 Null 방지

    void Awake()
    {
        // GameManager보다 먼저 실행되도록 Awake에서 초기화
        deck.CreateDeck();
    }

    public Card DrawCard()
    {
        return deck.DrawCard();
    }

    public int GetRemainingCardCount()
    {
        return deck.Count;
    }
}
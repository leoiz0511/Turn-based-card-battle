using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public DeckManager deckManager;
    public Transform playerHandArea;
    public Transform enemyHandArea;

    [Header("Prefabs")]
    public GameObject playerCardPrefab;
    public GameObject enemyCardBackPrefab;

    private List<Card> playerHand = new List<Card>();
    private List<Card> enemyHand = new List<Card>();

    void Start()
    {
        // 1. 할당 체크 (가장 먼저 수행)
        if (!ValidateReferences()) return;

        // 2. 데이터 초기화
        deckManager.deck.CreateDeck();
        deckManager.deck.Shuffle();

        // 3. 게임 시작
        StartGame();
    }

    // 모든 필수 레퍼런스가 인스펙터에 연결되었는지 확인
    private bool ValidateReferences()
    {
        if (deckManager == null) { Debug.LogError("DeckManager가 연결되지 않았습니다!"); return false; }
        if (playerHandArea == null || enemyHandArea == null) { Debug.LogError("HandArea가 연결되지 않았습니다!"); return false; }
        if (playerCardPrefab == null || enemyCardBackPrefab == null) { Debug.LogError("Prefab이 연결되지 않았습니다!"); return false; }
        return true;
    }

    void StartGame()
    {
        DealInitialCards();
    }

    void DealInitialCards()
    {
        for (int i = 0; i < 5; i++)
        {
            DrawToPlayer();
            DrawToEnemy();
        }
    }

    public void DrawToPlayer()
    {
        // 덱 매니저를 통해 카드 데이터 가져오기 (덱 매니저 내 DrawCard가 deck.DrawCard()를 호출한다고 가정)
        Card card = deckManager.deck.DrawCard();

        if (card == null)
        {
            Debug.LogWarning("덱이 비어있어 카드를 뽑을 수 없습니다.");
            return;
        }

        playerHand.Add(card);

        // UI 생성
        GameObject cardObj = Instantiate(playerCardPrefab, playerHandArea);
        CardUI ui = cardObj.GetComponent<CardUI>();

        if (ui != null)
        {
            ui.SetCard(card);
        }
        else
        {
            Debug.LogError($"{playerCardPrefab.name}에 CardUI 스크립트가 없습니다!");
        }
    }

    public void DrawToEnemy()
    {
        Card card = deckManager.deck.DrawCard();
        if (card == null) return;

        enemyHand.Add(card);
        Instantiate(enemyCardBackPrefab, enemyHandArea);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class CardHandVisuals : MonoBehaviour
{
    public int cardCount;
    public List<Transform> cardSlots;
    public float slotSize;
    public float slotRotation;
    public CardVisuals cardPrefab;
    public List<CardVisuals> cards;
    public Transform spawnPoint;
    public Transform discardPoint;
    public float giveInterval;
    public SelectionLine selectionLine;

    private void Awake()
    {
        
        CreateSlots();
        MoveSlots();
        PopulateCards();
    }

    void CreateSlots()
    {
        for (int i = 0; i < cardCount; i++)
        {
            Transform newSlot = new GameObject("Card Slot " + i).transform;
            newSlot.parent = transform;
            cardSlots.Add(newSlot);
        }
    }

    async void PopulateCards()
    {
        foreach (Transform slot in cardSlots)
        {
             var card = Instantiate(cardPrefab,spawnPoint.position,spawnPoint.rotation);
             card.slot = slot;
             cards.Add(card);
             await new WaitForSeconds(giveInterval);
        }
    }

    public void RemoveCard(CardVisuals card)
    {
        var index = cards.IndexOf(card);
        cards.RemoveAt(index);
        cardSlots.RemoveAt(index);
        MoveSlots();
    }

    void MoveSlots()
    {
        int num = cardSlots.Count -1;
        if( num == 0 ) num = 1;
        for (int i = 0; i < cardSlots.Count; i++)
        {
            float t = (float)i / num;
            float posX = Mathf.Lerp( -0.5f, 0.5f, t) * slotSize * cardSlots.Count;
            cardSlots[i].localPosition = new Vector3(posX, 0, 0);
            float rotZ = Mathf.Lerp( -1,1, t)* -slotRotation * cardSlots.Count;
            cardSlots[i].localEulerAngles = new Vector3(0,0,rotZ);
        }
    }
    
    [ContextMenu("Discard Cards")]
    async void DiscardCards()
    {
        foreach (CardVisuals card in cards)
        {
            card.slot = discardPoint;
            card.target = discardPoint;
        }
    }
}

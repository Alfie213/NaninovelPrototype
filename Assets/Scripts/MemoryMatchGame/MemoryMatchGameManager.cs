using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryMatchGame
{
    public class MemoryMatchGameManager : MonoBehaviour
    {
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform gridParent;
        
        private readonly List<Card> cards = new();
        private Card firstCard, secondCard;

        private void Start()
        {
            GenerateCards();
            ShuffleCards();
            CreateCardObjects();
        }

        private void GenerateCards()
        {
            for (int i = 0; i < 9; i++)
            {
                Card card = Instantiate(cardPrefab);
                card.cardType = i % 5;
                cards.Add(card);
            }
        }

        private void ShuffleCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card temp = cards[i];
                int randomIndex = Random.Range(i, cards.Count);
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }

        private void CreateCardObjects()
        {
            foreach (Card card in cards)
            {
                card.transform.SetParent(gridParent);
                card.GetComponent<Button>().onClick.AddListener(card.OnCardClicked);
            }
        }

        public void CheckForMatch(Card clickedCard)
        {
            if (firstCard == null)
            {
                firstCard = clickedCard;
            }
            else if (secondCard == null)
            {
                secondCard = clickedCard;

                if (firstCard.cardID == secondCard.cardID)
                {
                    ResetCards();
                }
                else
                {
                    Invoke("UnflipCards", 1f);
                }
            }
        }

        private void UnflipCards()
        {
            firstCard.UnflipCard();
            secondCard.UnflipCard();
            ResetCards();
        }

        private void ResetCards()
        {
            firstCard = null;
            secondCard = null;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryMatchGame
{
    public class MemoryMatchGameManager : MonoBehaviour
    {
        public Card cardPrefab;
        public Transform gridParent;
        private List<Card> cards = new List<Card>();
        private Card firstCard, secondCard;

        void Start()
        {
            GenerateCards();
            ShuffleCards();
            CreateCardObjects();
        }

        void GenerateCards()
        {
            for (int i = 0; i < 9; i++)
            {
                Card card = Instantiate(cardPrefab);
                card.cardID = i % 5;
                cards.Add(card);
            }
        }

        void ShuffleCards()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card temp = cards[i];
                int randomIndex = Random.Range(i, cards.Count);
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }

        void CreateCardObjects()
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

        void UnflipCards()
        {
            firstCard.UnflipCard();
            secondCard.UnflipCard();
            ResetCards();
        }

        void ResetCards()
        {
            firstCard = null;
            secondCard = null;
        }
    }
}
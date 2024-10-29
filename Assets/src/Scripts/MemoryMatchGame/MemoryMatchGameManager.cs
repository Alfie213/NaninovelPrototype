using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MemoryMatchGame
{
    public class MemoryMatchGameManager : MonoBehaviour
    {
        public UnityEvent OnGameEnd;
        
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform gridParent;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 3;

        [Header("Card Sprites")]
        [SerializeField] private Sprite unknownSprite;
        [SerializeField] private Sprite backSprite;
        [SerializeField] private Sprite cowSprite;
        [SerializeField] private Sprite chickenSprite;
        [SerializeField] private Sprite bullSprite;

        private readonly List<Card> cards = new();
        private Card firstCard, secondCard;

        public void StartGame()
        {
            cards.Clear();
            firstCard = null;
            secondCard = null;
            
            foreach (Transform child in gridParent)
                Destroy(child.gameObject);
            
            GenerateAndShuffleCards();
            SetupGridLayout();
        }
        
        private void GenerateAndShuffleCards()
        {
            int totalCards = rows * columns;
            var cardTypes = new List<CardType>();

            // Create card pairs and one unique card if totalCards is odd
            for (int i = 0; i < totalCards / 2; i++)
            {
                CardType cardType = (CardType)(i % System.Enum.GetValues(typeof(CardType)).Length);
                cardTypes.Add(cardType);
                cardTypes.Add(cardType); // Add twice to create a pair
            }

            // If the number of cards is odd, add a unique card
            if (totalCards % 2 != 0)
            {
                CardType uniqueType = (CardType)((totalCards / 2) % System.Enum.GetValues(typeof(CardType)).Length);
                cardTypes.Add(uniqueType);
            }

            // Shuffle the card types
            cardTypes = cardTypes.OrderBy(c => Random.value).ToList();

            // Create cards with assigned types
            foreach (var cardType in cardTypes)
            {
                var card = Instantiate(cardPrefab, gridParent);
                
                card.GetComponent<Card>().Initialize(cardType, backSprite, GetSpriteForCardType(cardType));
                card.name = $"Card_{cardType}";
                cards.Add(card.GetComponent<Card>());

                // Subscribe to the card click event
                var button = card.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(() => OnCardClicked(card.GetComponent<Card>()));
            }
        }

        private Sprite GetSpriteForCardType(CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Cow:
                    return cowSprite;
                case CardType.Chicken:
                    return chickenSprite;
                case CardType.Bull:
                    return bullSprite;
                default:
                    return unknownSprite;
            }
        }

        private void SetupGridLayout()
        {
            var gridLayout = gridParent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayout.constraintCount = columns;

                RectTransform rectTransform = gridParent.GetComponent<RectTransform>();
                float cellWidth = rectTransform.rect.width / columns;
                float cellHeight = rectTransform.rect.height / rows;
                gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
            }
            
            gridLayout.gameObject.SetActive(true);
        }

        private void OnCardClicked(Card clickedCard)
        {
            if (firstCard == null)
            {
                firstCard = clickedCard;
                firstCard.FlipCard();
            }
            else if (secondCard == null && clickedCard != firstCard) // Prevent re-selecting the first card
            {
                secondCard = clickedCard;
                secondCard.FlipCard();

                if (firstCard.CardType == secondCard.CardType)
                {
                    Debug.Log($"Pair found: {firstCard.CardType}");
                    DisableCardButtons(firstCard);
                    DisableCardButtons(secondCard);
                    
                    ResetSelection();
                    CheckForCompletion();
                }
                else
                {
                    Debug.Log("No match, flipping cards back...");
                    Invoke("UnflipSelectedCards", 1f);
                }
            }
        }

        private void UnflipSelectedCards()
        {
            firstCard.UnflipCard();
            secondCard.UnflipCard();
            ResetSelection();
        }

        private void ResetSelection()
        {
            firstCard = null;
            secondCard = null;
        }

        private void DisableCardButtons(Card card)
        {
            var button = card.GetComponent<Button>();
            if (button != null) button.interactable = false;
        }

        private void CheckForCompletion()
        {
            // Check if all pairs are found or one card remains active (if odd number of cards)
            int activeCards = cards.Count(card => !card.IsFlipped);

            if (activeCards == 0 || (activeCards == 1 && cards.Count % 2 != 0))
            {
                Debug.Log("Congratulations, you've found all pairs!");
                gridParent.gameObject.SetActive(false);
                OnGameEnd?.Invoke();
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace MemoryMatchGame
{
    public class MemoryMatchGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform gridParent;
        [SerializeField] private int rows = 3;
        [SerializeField] private int columns = 3;
        [SerializeField] private TextMeshProUGUI resultText;

        [Header("Card Sprites")]
        [SerializeField] private Sprite unknownSprite;
        [SerializeField] private Sprite backSprite;
        [SerializeField] private Sprite cowSprite;
        [SerializeField] private Sprite chickenSprite;
        [SerializeField] private Sprite bullSprite;

        private readonly List<Card> cards = new();
        private Card firstCard, secondCard;

        private void Start()
        {
            GenerateAndShuffleCards();
            SetupGridLayout();
        }

        private void GenerateAndShuffleCards()
        {
            int totalCards = rows * columns;
            var cardTypes = new List<CardType>();

            // Создаем пары карт и одну уникальную карту, если totalCards нечетное
            for (int i = 0; i < totalCards / 2; i++)
            {
                CardType cardType = (CardType)(i % System.Enum.GetValues(typeof(CardType)).Length);
                cardTypes.Add(cardType);
                cardTypes.Add(cardType); // Добавляем дважды для пары
            }

            // Если нечетное количество карт, добавляем одну уникальную карту
            if (totalCards % 2 != 0)
            {
                CardType uniqueType = (CardType)((totalCards / 2) % System.Enum.GetValues(typeof(CardType)).Length);
                cardTypes.Add(uniqueType);
            }

            // Перемешиваем типы карт
            cardTypes = cardTypes.OrderBy(c => Random.value).ToList();

            // Создаем карты с назначенными типами
            foreach (var cardType in cardTypes)
            {
                var card = Instantiate(cardPrefab, gridParent);
                
                card.GetComponent<Card>().Initialize(cardType, backSprite, GetSpriteForCardType(cardType));
                card.name = $"Card_{cardType}";
                cards.Add(card.GetComponent<Card>());

                // Подписка на событие нажатия карты
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
        }

        private void OnCardClicked(Card clickedCard)
        {
            if (firstCard == null)
            {
                firstCard = clickedCard;
                firstCard.FlipCard();
            }
            else if (secondCard == null && clickedCard != firstCard) // Не допускаем повторного выбора первой карты
            {
                secondCard = clickedCard;
                secondCard.FlipCard();

                if (firstCard.CardType == secondCard.CardType)
                {
                    Debug.Log($"Пара найдена: {firstCard.CardType}");
                    DisableCardButtons(firstCard);
                    DisableCardButtons(secondCard);
                    
                    ResetSelection();
                    CheckForCompletion();
                }
                else
                {
                    Debug.Log("Не совпадает, закрываем карты...");
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
            // Проверка, что либо все пары найдены, либо осталась одна активная карта (если количество карт нечетное)
            int activeCards = cards.Count(card => !card.IsFlipped);

            if (activeCards == 0 || (activeCards == 1 && cards.Count % 2 != 0))
            {
                Debug.Log("Поздравляем, вы нашли все пары!");
                resultText.text = "Поздравляем, вы нашли все пары!";
            }
        }
    }
}

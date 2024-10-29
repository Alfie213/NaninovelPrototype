using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryMatchGame
{
    public enum CardType
    {
        Cow,
        Chicken,
        Bull
    }
    
    public class Card : MonoBehaviour
    {
        public CardType cardType;
        
        [SerializeField] private Sprite backSprite;
        [SerializeField] private Sprite frontSprite;

        public bool IsFlipped { get; private set; }

        private Image cardImage;
        
        private void Awake()
        {
            cardImage = GetComponent<Image>();
        }

        private void Start()
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = cardType.ToString();
        }

        public void OnCardClicked()
        {
            if (IsFlipped) return;

            FlipCard();
        }

        public void FlipCard()
        {
            IsFlipped = true;
            cardImage.sprite = frontSprite;
        }

        public void UnflipCard()
        {
            IsFlipped = false;
            cardImage.sprite = backSprite;
        }
    }
}
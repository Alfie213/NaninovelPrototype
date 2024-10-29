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
        public CardType CardType { get; private set; }
        
        private Sprite backSprite;
        private Sprite frontSprite;
        
        private Image cardImage;
        
        private bool isFlipped;

        public void Initialize(CardType cardType, Sprite backSprite, Sprite frontSprite)
        {
            this.CardType = cardType;
            this.backSprite = backSprite;
            this.frontSprite = frontSprite;
        }
        
        private void Awake()
        {
            cardImage = GetComponent<Image>();
        }

        public void FlipCard()
        {
            isFlipped = true;
            cardImage.sprite = backSprite;
        }

        public void UnflipCard()
        {
            isFlipped = false;
            cardImage.sprite = frontSprite;
        }
    }
}
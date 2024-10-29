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
        public bool IsFlipped { get; private set; }
        
        private Sprite backSprite;
        private Sprite frontSprite;
        
        private Image cardImage;
        

        public void Initialize(CardType cardType, Sprite backSprite, Sprite frontSprite)
        {
            this.CardType = cardType;
            this.backSprite = backSprite;
            this.frontSprite = frontSprite;

            cardImage = GetComponent<Image>();
            cardImage.sprite = this.backSprite;
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
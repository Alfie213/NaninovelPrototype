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

        private Image _cardImage;
        
        private bool isFlipped;

        private void Awake()
        {
            _cardImage = GetComponent<Image>();
        }

        private void Start()
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = cardType.ToString();
        }

        public void OnCardClicked()
        {
            if (isFlipped) return;

            FlipCard();
        }

        private void FlipCard()
        {
            isFlipped = true;
            _cardImage.sprite = frontSprite;
        }

        public void UnflipCard()
        {
            isFlipped = false;
            _cardImage.sprite = backSprite;
        }
    }
}
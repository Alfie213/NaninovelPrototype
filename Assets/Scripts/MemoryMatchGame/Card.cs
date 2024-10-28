using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MemoryMatchGame
{
    public class Card : MonoBehaviour
    {
        public Image cardImage;
        public int cardID;

        private bool isFlipped = false;

        private void Start()
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = cardID.ToString();
        }

        public void OnCardClicked()
        {
            if (isFlipped) return;

            FlipCard();
        }

        public void FlipCard()
        {
            isFlipped = true;
            cardImage.enabled = true;
        }

        public void UnflipCard()
        {
            isFlipped = false;
            cardImage.enabled = false;
        }
    }
}
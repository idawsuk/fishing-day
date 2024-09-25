using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class BaitUIController : MonoBehaviour
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private BaitItem prefab;
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI totalPriceDisplay;
        [SerializeField] private TextMeshProUGUI errorMessage;
        private List<Bait> baitToBuy = new List<Bait>();
        private List<BaitItem> baitItems = new List<BaitItem>();
        private int totalPrice = 0;

        [SerializeField] private Sprite redBait, blueBait, greenBait;

        // Start is called before the first frame update
        void Start()
        {
            Bait[] baits = gameplayManager.Game.Baits;
            for (int i = 0; i < baits.Length; i++)
            {
                Bait bait = baits[i];
                GameObject go = Instantiate(prefab.gameObject, prefab.transform.parent);
                BaitItem item = go.GetComponent<BaitItem>();
                item.SetData(bait.Color, bait.Color.ToString(), GetSprite(bait.Color));

                go.SetActive(true);

                item.OnAddBait += OnAddBait;
                item.OnRemoveBait += OnRemoveBait;

                baitItems.Add(item);
            }

            buyButton.onClick.AddListener(BuyBait);
        }

        public void Reset()
        {
            totalPrice = 0;
            baitToBuy.Clear();
            for (int i = 0; i < baitItems.Count; i++)
            {
                baitItems[i].ResetCount();
            }

            UpdatePriceDisplay();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnAddBait(Color color)
        {
            Bait bait = gameplayManager.Game.GetBait(color);
            totalPrice += bait.Price;
            baitToBuy.Add(bait);

            UpdatePriceDisplay();
        }

        private void OnRemoveBait(Color color)
        {
            Bait bait = gameplayManager.Game.GetBait(color);
            totalPrice -= bait.Price;
            baitToBuy.Remove(bait);

            UpdatePriceDisplay();
        }

        private Sprite GetSprite(Color color)
        {
            switch (color)
            {
                case Color.Red:
                    return redBait;
                case Color.Green:
                    return greenBait;
                case Color.Blue:
                    return blueBait;
            }

            return redBait;
        }

        private void BuyBait()
        {
            var result = gameplayManager.ConfirmBaitPurchase(baitToBuy);
            errorMessage.gameObject.SetActive(!result);
        }

        private void UpdatePriceDisplay()
        {
            totalPriceDisplay.text = totalPrice.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class FishingPoleUIController : MonoBehaviour
    {
        [SerializeField] private GameplayManager gameplayManager;
        [SerializeField] private PoleItem prefab;
        [SerializeField] private Sprite smallPole, mediumPole, bigPole;
        [SerializeField] private Button buyButton;
        [SerializeField] private RectTransform selectionTransform;

        private Size selectedPole;

        // Start is called before the first frame update
        void Start()
        {
            FishingPole[] poles = gameplayManager.Game.FishingPoles;
            for (int i = 0; i < poles.Length; i++)
            {
                FishingPole pole = poles[i];
                GameObject go = Instantiate(prefab.gameObject, prefab.transform.parent);
                PoleItem item = go.GetComponent<PoleItem>();
                item.SetData(pole.Size, pole.Size.ToString(), GetSprite(pole.Size));

                go.SetActive(true);

                item.SelectedEvent += (size) =>
                {
                    ChangeSelectedPole(size);
                    RectTransform rt = (RectTransform)item.transform;
                    selectionTransform.anchoredPosition = rt.anchoredPosition;
                    selectionTransform.gameObject.SetActive(true);
                };
            }

            buyButton.onClick.AddListener(BuyPole);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void ChangeSelectedPole(Size size)
        {
            selectedPole = size;
        }

        private void BuyPole()
        {
            gameplayManager.ConfirmPolePurchase(selectedPole);
        }

        private Sprite GetSprite(Size size)
        {
            switch (size)
            {
                case Size.Small:
                    return smallPole;
                case Size.Medium:
                    return mediumPole;
                case Size.Big:
                default:
                    return bigPole;
            }
        }
    }
}

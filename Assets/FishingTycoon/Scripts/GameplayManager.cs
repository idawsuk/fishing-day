using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishPlanner
{
    public class GameplayManager : MonoBehaviour
    {
        private Game game;

        public Game Game => game;

        [SerializeField] private FishingPoleUIController fishingPoleController;
        [SerializeField] private BaitUIController baitController;

        void Awake()
        {
            game = new Game();
            StartDay();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void StartDay()
        {
            game.StartDay();
            Game.FishForecast forecast = game.Forecast;
            fishingPoleController.gameObject.SetActive(true);
            baitController.gameObject.SetActive(false);
        }

        public void StartFishing()
        {
            var fish = game.StartFishing();

            for (int i = 0; i < fish.Count; i++)
            {
                Debug.Log($"fish: {fish[i].Color}, {fish[i].Size}, {fish[i].Reward}");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool ConfirmBaitPurchase(List<Bait> baitToBuy)
        {
            bool result = game.BuyBait(baitToBuy);
            if (result)
            {
                baitController.gameObject.SetActive(false);
                fishingPoleController.gameObject.SetActive(false);
                StartFishing();
            }
            else
            {
                Debug.Log("not enough gold");
            }

            return result;
        }

        public bool ConfirmPolePurchase(Size size)
        {
            bool result = game.BuyFishingPole(size);
            if(result)
            {
                baitController.gameObject.SetActive(true);
                fishingPoleController.gameObject.SetActive(false);
            } else
            {
                Debug.Log("not enough gold");
            }

            return result;
        }
    }
}
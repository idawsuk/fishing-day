using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FishPlanner
{
    public class GameplayManager : MonoBehaviour
    {
        private Game game;

        public Game Game => game;

        [SerializeField] private FishingPoleUIController fishingPoleController;
        [SerializeField] private BaitUIController baitController;
        [SerializeField] private PlayerInfoController playerInfoController;
        [SerializeField] private ForecastUIController forecastController;
        [SerializeField] private ResultController resultController;

        void Awake()
        {
            game = new Game();
            StartDay();
        }

        // Start is called before the first frame update
        void Start()
        {
            OnPlayerGoldUpdated(game.Player.Gold); //initial gold
            game.Player.OnGoldEventChanged += OnPlayerGoldUpdated;
            game.Player.OnPoleEventChanged += OnPlayerPoleUpdated;
            resultController.NextDayEvent += StartDay;
            resultController.RestartEvent += () =>
            {
                SceneManager.LoadScene("GameScene"); //restart scene
            };
        }

        private void StartDay()
        {
            game.StartDay();
            Game.FishForecast forecast = game.Forecast;
            forecastController.UpdateForecast(forecast, game.Day);
            fishingPoleController.gameObject.SetActive(true);
            fishingPoleController.ResetSelection();
            baitController.gameObject.SetActive(false);
            baitController.Reset();
            resultController.gameObject.SetActive(false);
            playerInfoController.UpdateBait(game.Player.Baits);
        }

        public void StartFishing()
        {
            var result = game.StartFishing();

            for (int i = 0; i < result.Fish.Count; i++)
            {
                Debug.Log($"fish: {result.Fish[i].Color}, {result.Fish[i].Size}, {result.Fish[i].Reward}");
            }

            Debug.Log($"gold earned: {result.GoldEarned}");
            Debug.Log($"total gold: {result.TotalGold}");
            Debug.Log($"can proceed to next day? {result.Survived}");

            fishingPoleController.gameObject.SetActive(false);
            baitController.gameObject.SetActive(false);
            resultController.gameObject.SetActive(true);
            resultController.PopulateResult(result);
            playerInfoController.UpdateBait(game.Player.Baits);
        }

        public void SkipDay()
        {
            StartDay();
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
                playerInfoController.UpdateFishingPole(size.ToString());
            } else
            {
                Debug.Log("not enough gold");
            }

            return result;
        }

        private void OnPlayerGoldUpdated(int gold)
        {
            playerInfoController.UpdateGold(gold);
        }

        private void OnPlayerPoleUpdated(FishingPole pole)
        {
            string name = "-";
            if(pole != null)
            {
                name = pole.Size.ToString();
            }

            playerInfoController.UpdateFishingPole(name);
        }
    }
}
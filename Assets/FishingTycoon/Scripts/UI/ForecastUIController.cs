using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static FishPlanner.Game;

namespace FishPlanner
{
    public class ForecastUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fishSize;
        [SerializeField] private TextMeshProUGUI fishColor;
        [SerializeField] private TextMeshProUGUI day;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateForecast(FishForecast fishForecast, int day)
        {
            string[] sizeContent = new string[fishForecast.FishSize.Count];
            int count = 0;
            foreach(var size in fishForecast.FishSize)
            {
                sizeContent[count] = $"{size.Value} {size.Key.ToString().ToLower()} fish";
                count++;
            }
            fishSize.text = $"Today, we're seeing {string.Join(", ", sizeContent)}.";

            string[] chanceContent = new string[fishForecast.FishColor.Count];
            count = 0;
            foreach(var chance in fishForecast.FishColor)
            {
                if (count == 0)
                {
                    chanceContent[count] = $"{(chance.Value * 100).ToString("00")}% of the fish are {chance.Key.ToString().ToLower()}";
                } else
                {
                    chanceContent[count] = $"{(chance.Value * 100).ToString("00")}% are {chance.Key.ToString().ToLower()}";
                }
                count++;
            }
            fishColor.text = $"{string.Join(", ", chanceContent)}.";

            this.day.text = day.ToString();
        }
    }
}

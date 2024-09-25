using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class FishItem : MonoBehaviour
    {
        [SerializeField] private Image bgColor;
        [SerializeField] private TextMeshProUGUI size;
        [SerializeField] private TextMeshProUGUI price;

        [SerializeField] private UnityEngine.Color red;
        [SerializeField] private UnityEngine.Color green;
        [SerializeField] private UnityEngine.Color blue;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetData(Fish fish)
        {
            switch (fish.Color)
            {
                case Color.Red:
                    bgColor.color = red;
                    break;
                case Color.Green:
                    bgColor.color = green;
                    break;
                case Color.Blue:
                    bgColor.color = blue;
                    break;
            }

            size.text = fish.Size.ToString();
            price.text = fish.Reward.ToString();
        }
    }
}

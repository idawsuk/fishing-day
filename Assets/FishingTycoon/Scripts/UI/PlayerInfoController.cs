using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class PlayerInfoController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gold;
        [SerializeField] private TextMeshProUGUI fishingPole;
        [SerializeField] private BaitInfo redBait;
        [SerializeField] private BaitInfo blueBait;
        [SerializeField] private BaitInfo greenBait;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void UpdateFishingPole(string name)
        {
            fishingPole.text = name;
        }

        public void UpdateGold(int gold)
        {
            this.gold.text = gold.ToString();
        }

        public void UpdateBait(Dictionary<Color, List<Bait>> baits)
        {
            foreach(var bait in baits)
            {
                switch (bait.Key)
                {
                    case Color.Red:
                        redBait.UpdateBaitCount(bait.Value.Count);
                        break;
                    case Color.Green:
                        greenBait.UpdateBaitCount(bait.Value.Count);
                        break;
                    case Color.Blue:
                        blueBait.UpdateBaitCount(bait.Value.Count);
                        break;
                }
            }
        }
    }
}

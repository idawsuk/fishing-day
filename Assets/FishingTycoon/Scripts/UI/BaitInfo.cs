using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class BaitInfo : MonoBehaviour
    {
        [SerializeField] private Image thumbnail;
        [SerializeField] private TextMeshProUGUI baitName;
        [SerializeField] private TextMeshProUGUI baitCount;

        private Color color;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetData(Color color, string baitName, Sprite sprite)
        {
            thumbnail.sprite = sprite;
            this.baitName.text = baitName;
            this.color = color;
        }

        public void UpdateBaitCount(int count)
        {
            baitCount.text = count.ToString();
        }
    }
}

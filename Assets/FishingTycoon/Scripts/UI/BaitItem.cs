using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class BaitItem : MonoBehaviour
    {
        [SerializeField] private Image thumbnail;
        [SerializeField] private TextMeshProUGUI baitName;
        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private TextMeshProUGUI baitCount;
        private int count;
        private Color color;

        public delegate void BaitItemEvent(Color color);
        public BaitItemEvent OnAddBait;
        public BaitItemEvent OnRemoveBait;

        // Start is called before the first frame update
        void Start()
        {
            addButton.onClick.AddListener(AddBait);
            removeButton.onClick.AddListener(RemoveBait);
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

        public void ResetCount()
        {
            count = 0;
            baitCount.text = count.ToString();
        }

        private void AddBait()
        {
            count++;
            baitCount.text = count.ToString();
            OnAddBait?.Invoke(color);
        }

        private void RemoveBait()
        {
            count--;
            if(count < 0)
            {
                count = 0;
            }

            baitCount.text = count.ToString();
            OnRemoveBait?.Invoke(color);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class PoleItem : MonoBehaviour
    {
        [SerializeField] private Button selectButton;
        [SerializeField] private Image thumbnail;
        [SerializeField] private TextMeshProUGUI poleName;

        private Size size;

        public delegate void PoleItemEvent(Size size);
        public PoleItemEvent SelectedEvent;

        // Start is called before the first frame update
        void Start()
        {
            selectButton.onClick.AddListener(SelectItem);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SetData(Size size, string poleName, Sprite sprite)
        {
            this.size = size;
            thumbnail.sprite = sprite;
            this.poleName.text = poleName;
        }

        private void SelectItem()
        {
            SelectedEvent?.Invoke(size);
        }
    }
}

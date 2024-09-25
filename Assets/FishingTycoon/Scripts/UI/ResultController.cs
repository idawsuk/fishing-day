using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishPlanner
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] private FishItem prefab;
        [SerializeField] private List<FishItem> items = new List<FishItem>();
        [SerializeField] private Button nextDayButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI resultMessage;

        public delegate void InteractEvent();
        public InteractEvent NextDayEvent;
        public InteractEvent RestartEvent;

        // Start is called before the first frame update
        void Start()
        {
            nextDayButton.onClick.AddListener(() => NextDayEvent?.Invoke());
            restartButton.onClick.AddListener(() => RestartEvent?.Invoke());
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void PopulateResult(Game.FishingResults result)
        {
            HideItems();
            if(items.Count > result.Fish.Count)
            {
                for (int i = 0; i < result.Fish.Count; i++)
                {
                    items[i].SetData(result.Fish[i]);
                    items[i].gameObject.SetActive(true);
                }

                for (int i = result.Fish.Count; i < items.Count; i++)
                {
                    items[i].gameObject.SetActive(false);
                }
            } else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].SetData(result.Fish[i]);
                    items[i].gameObject.SetActive(true);
                }

                for (int i = items.Count; i < result.Fish.Count; i++)
                {
                    GameObject go = Instantiate(prefab.gameObject, prefab.transform.parent);
                    FishItem item = go.GetComponent<FishItem>();
                    item.SetData(result.Fish[i]);
                    items.Add(item);
                    go.SetActive(true);
                }
            }

            nextDayButton.gameObject.SetActive(result.Survived);
            restartButton.gameObject.SetActive(!result.Survived);

            if(result.Survived)
            {
                resultMessage.color = UnityEngine.Color.green;
                resultMessage.text = "You have enough gold to continue!";
            } else
            {
                resultMessage.color = UnityEngine.Color.red;
                resultMessage.text = "You don't have enough gold, try again next time.";
            }
        }

        private void HideItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].gameObject.SetActive(false);
            }
        }
    }
}

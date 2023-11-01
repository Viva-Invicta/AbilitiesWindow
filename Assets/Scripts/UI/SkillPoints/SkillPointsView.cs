using System;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitiesWindow
{
    public class SkillPointsView : View
    {
        public event Action IncreaseButtonClicked;

        [SerializeField] private Text pointsCountText;
        [SerializeField] private Button increasePointsButton;

        protected override void InitializeInternal()
        {
            increasePointsButton.onClick.AddListener(() => IncreaseButtonClicked?.Invoke());
        }

        public void SetCount(int count)
        {
            pointsCountText.text = count.ToString();
        }
    }
}
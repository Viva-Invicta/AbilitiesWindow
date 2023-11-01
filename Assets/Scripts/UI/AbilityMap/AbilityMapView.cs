using System;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitiesWindow.AbilityMap
{
    public class AbilityMapView : View
    {
        [SerializeField] private Button learnButton;
        [SerializeField] private Button forgetButton;
        [SerializeField] private Button forgetAllButton;
        [SerializeField] private Text costText;

        public event Action LearnButtonPressed;
        public event Action ForgetButtonPressed;
        public event Action ForgetAllButtonPressed;

        protected override void InitializeInternal()
        {
            learnButton.onClick.AddListener(() => LearnButtonPressed?.Invoke());   
            forgetButton.onClick.AddListener(() => ForgetButtonPressed?.Invoke());
            forgetAllButton.onClick.AddListener(() => ForgetAllButtonPressed?.Invoke());
        }

        public void SetLearnButtonActive(bool isActive)
        {
            learnButton.interactable = isActive;
        }

        public void SetForgetButtonActive(bool isActive)
        {
            forgetButton.interactable = isActive;
        }

        public void SetCost(int cost) 
        {
            costText.text = cost == 0 ? "-" : cost.ToString();
        }
    }
}